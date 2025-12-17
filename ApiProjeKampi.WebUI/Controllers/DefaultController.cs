using ApiProjeKampi.WebUI.Dtos.ReservationDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Collections.Generic;
using System.Net.Http;

namespace ApiProjeKampi.WebUI.Controllers
{
    [Route("Default")]
    public class DefaultController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DefaultController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }


        [HttpGet]
        [Route("Index")]
        [Route("")]
        public IActionResult Index()
        {
            System.Diagnostics.Debug.WriteLine("=== DEFAULT CONTROLLER GET ACTION ÇAĞRILDI ===");
            return View(new CreateReservationDto());
        }

        [HttpPost]
        [Route("Index")]
        public async Task<IActionResult> Index(CreateReservationDto createReservationDto)
        {
            // DEBUG: Controller'a geldi mi kontrol et
            System.Diagnostics.Debug.WriteLine("=== DEFAULT CONTROLLER POST ACTION ÇAĞRILDI ===");
            System.Diagnostics.Debug.WriteLine($"createReservationDto null mu? {createReservationDto == null}");
            
            // Debug: Gelen veriyi kontrol et
            if (createReservationDto == null)
            {
                System.Diagnostics.Debug.WriteLine("createReservationDto NULL!");
                TempData["ErrorMessage"] = "Form verisi alınamadı. Lütfen tekrar deneyin.";
                return View(new CreateReservationDto());
            }
            
            // DEBUG: Gelen veriyi logla
            System.Diagnostics.Debug.WriteLine($"NameSurname: {createReservationDto.NameSurname}");
            System.Diagnostics.Debug.WriteLine($"Email: {createReservationDto.Email}");
            System.Diagnostics.Debug.WriteLine($"PhoneNumber: {createReservationDto.PhoneNumber}");
            System.Diagnostics.Debug.WriteLine($"ReservationDate: {createReservationDto.ReservationDate}");
            System.Diagnostics.Debug.WriteLine($"ReservationTime: {createReservationDto.ReservationTime}");
            System.Diagnostics.Debug.WriteLine($"CountofPeople: {createReservationDto.CountofPeople}");
            System.Diagnostics.Debug.WriteLine($"Message: {createReservationDto.Message ?? "null"}");

            // DEBUG: ModelState kontrolü
            System.Diagnostics.Debug.WriteLine($"=== MODELSTATE KONTROLÜ ===");
            System.Diagnostics.Debug.WriteLine($"ModelState.IsValid: {ModelState.IsValid}");
            System.Diagnostics.Debug.WriteLine($"ModelState.ErrorCount: {ModelState.ErrorCount}");

            // Debug: ModelState kontrolü - Tüm hataları göster
            if (!ModelState.IsValid)
            {
                var errors = new List<string>();
                foreach (var key in ModelState.Keys)
                {
                    var state = ModelState[key];
                    foreach (var error in state.Errors)
                    {
                        var errorMsg = $"{key}: {error.ErrorMessage}";
                        errors.Add(errorMsg);
                        System.Diagnostics.Debug.WriteLine($"ModelState Error: {errorMsg}");
                    }
                }
                var errorList = string.Join(" | ", errors);
                System.Diagnostics.Debug.WriteLine($"=== VALIDATION HATALARI: {errorList} ===");
                TempData["ErrorMessage"] = $"Validation hataları: {errorList}";
                return View(createReservationDto);
            }
            
            createReservationDto.ReservationStatus = "Onay Bekliyor";

            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createReservationDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PostAsync("https://localhost:7020/api/Reservations", stringContent);
            
            if (responseMessage.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "Rezervasyon talebiniz başarıyla oluşturuldu!";
                return RedirectToAction("Index");
            }
            
            TempData["ErrorMessage"] = "Rezervasyon oluşturulurken bir hata oluştu. Lütfen tekrar deneyin.";
            return View(createReservationDto);
        }
    }
}
