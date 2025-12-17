using ApiProjeKampi.WebUI.Dtos.MessageDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using static ApiProjeKampi.WebUI.Controllers.AIController;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class MessageController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private const string ApiBaseUrl = "https://localhost:7020/api";

        public MessageController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> MessageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/Messages");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultMessageDto>>(jsonData);
                return View(values);
            }

            return View(new List<ResultMessageDto>());
        }

        [HttpGet]
        public IActionResult CreateMessage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(CreateMessageDto createMessageDto)
        {
            createMessageDto.SendDate = DateTime.Now;
            createMessageDto.IsRead = false;

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var responseMessage = await client.PostAsync($"{ApiBaseUrl}/Messages", content);

            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }

            return View();
        }

        public async Task<IActionResult> DeleteMessage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            await client.DeleteAsync($"{ApiBaseUrl}/Messages?id={id}");
            return RedirectToAction("MessageList");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateMessage(int id)
        {
            try
            {
                var client = _httpClientFactory.CreateClient();
                var responseMessage = await client.GetAsync($"{ApiBaseUrl}/Messages/GetMessage?id={id}");

                if (responseMessage.IsSuccessStatusCode)
                {
                    var jsonData = await responseMessage.Content.ReadAsStringAsync();
                    var value = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);

                    if (value != null)
                    {
                        return View(value);
                    }
                }

                return RedirectToAction("MessageList");
            }
            catch (Exception)
            {
                return RedirectToAction("MessageList");
            }
        }

        [HttpPost]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            try
            {
                if (updateMessageDto == null || updateMessageDto.MessageId <= 0)
                {
                    return RedirectToAction("MessageList");
                }

                var client = _httpClientFactory.CreateClient();
                var jsonData = JsonConvert.SerializeObject(updateMessageDto);
                StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
                var responseMessage = await client.PutAsync($"{ApiBaseUrl}/Messages", stringContent);

                if (responseMessage.IsSuccessStatusCode)
                {
                    return RedirectToAction("MessageList");
                }

                // Hata durumunda mevcut mesajı tekrar getir
                var getMessage = await client.GetAsync($"{ApiBaseUrl}/Messages/GetMessage?id={updateMessageDto.MessageId}");
                if (getMessage.IsSuccessStatusCode)
                {
                    var existingData = await getMessage.Content.ReadAsStringAsync();
                    var model = JsonConvert.DeserializeObject<GetMessageByIdDto>(existingData);

                    if (model != null)
                    {
                        return View(model);
                    }
                }

                return RedirectToAction("MessageList");
            }
            catch (Exception)
            {
                return RedirectToAction("MessageList");
            }


        }

        [HttpGet]
        public async Task<IActionResult> AnswerMessageWithOpenAI(int id,string promt)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync($"{ApiBaseUrl}/Messages/GetMessage?id={id}");

            var jsonData = await responseMessage.Content.ReadAsStringAsync();
            var value = JsonConvert.DeserializeObject<GetMessageByIdDto>(jsonData);

            if (value != null && !string.IsNullOrEmpty(value.MessageDetails))
            {
                promt = value.MessageDetails;
            }
            else
            {
                return RedirectToAction("MessageList");
            }
            var apiKey = "";

            using var client2 = new HttpClient();
            client2.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",apiKey);

            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new{
                        role="system",
                        content="Sen bir restoran için kullanıcıların göndermiş oldukları mesajları detaylı ve olabildiğince olumlu, müşteri memnuniyeti gözeten cevaplar veren bir yapay zeka aracısın." +
                        "Amacımız kullanıcı tarafından gönderdilen mesajlara en olumlu ve en mantıklı cevapları sunabilmek"},
                    new{
                        role="user",
                        content=promt},

                },

                temperature = 0.5

            };

            var response = await client2.PostAsJsonAsync("https://api.openai.com/V1/chat/completions", requestData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();

                if (result?.choices != null && result.choices.Count > 0 && result.choices[0]?.message != null)
                {
                    var content = result.choices[0].message.content;
                    ViewBag.answerAI = content ?? "Yanıt alınamadı";
                }
                else
                {
                    ViewBag.answerAI = "Yanıt formatı beklenmedik";
                }
            }

            else
            {
                ViewBag.answerAI = "Bir hata oluştu" + response.StatusCode;
            }


            return View(value);

        }

        

        public PartialViewResult SendMessage()
        {
            return PartialView();
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(CreateMessageDto createMessageDto)
        {

            var client = new HttpClient();
            var apiKey = "";
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);
            try
            {
                var translateRequestBody = new
                {
                    inputs = createMessageDto.MessageDetails
                };
                var translateJson = System.Text.Json.JsonSerializer.Serialize(translateRequestBody);
                var translateContent = new StringContent(translateJson, Encoding.UTF8, "application/json");

                var translateResponse = await client.PostAsync("https://api-inference.huggingface.co/models/Helsinki-NLP/opus-mt-tr-en", translateContent);
                var translateResponseString = await translateResponse.Content.ReadAsStringAsync();

                string englishText = createMessageDto.MessageDetails;
                if (translateResponseString.TrimStart().StartsWith("["))
                {
                    var translateDoc = JsonDocument.Parse(translateResponseString);
                    englishText = translateDoc.RootElement[0].GetProperty("translation_text").GetString();
                    //ViewBag.v = englishText;
                }

                var toxicRequestBody = new
                {
                    inputs = englishText
                };

                var toxicJson = System.Text.Json.JsonSerializer.Serialize(toxicRequestBody);
                var toxicContent = new StringContent(toxicJson, Encoding.UTF8, "application/json");
                var toxicResponse = await client.PostAsync("https://api-inference.huggingface.co/models/unitary/toxic-bert", toxicContent);
                var toxicResponseString = await toxicResponse.Content.ReadAsStringAsync();

                if (toxicResponseString.TrimStart().StartsWith("["))
                {
                    var toxicDoc = JsonDocument.Parse(toxicResponseString);
                    foreach (var item in toxicDoc.RootElement[0].EnumerateArray())
                    {
                        string label = item.GetProperty("label").GetString();
                        double score = item.GetProperty("score").GetDouble();

                        if (score > 0.5)
                        {
                            createMessageDto.Status = "Toksik Mesaj";
                            break;
                        }
                    }
                }
                if (string.IsNullOrEmpty(createMessageDto.Status))
                {
                    createMessageDto.Status = "Mesaj Alındı";
                }
            }
            catch (Exception)
            {
                createMessageDto.Status = "Onay Bekliyor";
            }


            var client2 = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createMessageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client2.PostAsync("https://localhost:7020/api/Messages", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("MessageList");
            }
            return View();
        }

    }
}
