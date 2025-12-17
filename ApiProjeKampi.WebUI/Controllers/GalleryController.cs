using ApiProjeKampi.WebUI.Dtos.ImageDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace ApiProjeKampi.WebUI.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public GalleryController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> ImageList()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/Images");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultImageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        public async Task<IActionResult> ImageListWithEdit()
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/Images");
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<ResultImageDto>>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateImage()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateImage(CreateImageDto createImageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createImageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7020/api/Images", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ImageListWithEdit");
            }
            return View();
        }

        public async Task<IActionResult> DeleteImage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7020/api/Images?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ImageListWithEdit");
            }
            return RedirectToAction("ImageListWithEdit");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateImage(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7020/api/Images/GetImage?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var getValue = JsonConvert.DeserializeObject<GetImageByIdDto>(jsonData);
                if (getValue != null)
                {
                    var updateValue = new UpdateImageDto
                    {
                        ImageId = getValue.ImageId,
                        Title = getValue.Title,
                        ImageUrl = getValue.ImageUrl
                    };
                    return View(updateValue);
                }
            }
            return RedirectToAction("ImageListWithEdit");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateImage(UpdateImageDto updateImageDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateImageDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7020/api/Images", stringContent);
            if (responseMessage.IsSuccessStatusCode)
            {
                return RedirectToAction("ImageListWithEdit");
            }
            return View();
        }
    }
}

