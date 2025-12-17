using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class AIController : Controller
    {

        public IActionResult CreateRecipeWithOpenAI()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipeWithOpenAI(string promt)
        {
            var apiKey = "";

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

            var requestData = new
            {
                model = "gpt-3.5-turbo",
                messages = new[]
                {
                    new{
                        role="system",
                        content="Sen bir restoran için yemek önerileri yapan bir yapay zeka aracısın," +
                        " amacımız kullanıcı tarafından girilen malzemelere göre yemek tarifi önerisinde bulunmak."},
                    new{
                        role="user",
                        content=promt},

                },

                temperature=0.5

            };

            var response = await client.PostAsJsonAsync("https://api.openai.com/V1/chat/completions", requestData);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<OpenAIResponse>();

                if (result?.choices != null && result.choices.Count > 0 && result.choices[0]?.message != null)
                {
                    var content = result.choices[0].message.content;
                    ViewBag.recipe = content ?? "Yanıt alınamadı";
                }
                else
                {
                    ViewBag.recipe = "Yanıt formatı beklenmedik";
                }
            }

            else 
            {
                ViewBag.recipe = "Bir hata oluştu"+response.StatusCode;
            }


            return View();
        }

        public class OpenAIResponse
        {
            public List<Choice> choices { get; set; }

        }

        public class Choice 
        {
            public Message message { get; set; }
        }

        public class Message
        {
            public string role { get; set; }

            public string content { get; set; }
        }
    }
}
