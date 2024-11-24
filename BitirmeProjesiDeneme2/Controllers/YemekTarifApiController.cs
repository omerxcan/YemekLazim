using BitirmeProjesiDeneme2.Models;
using Microsoft.AspNetCore.Mvc;

namespace BitirmeProjesiDeneme2.Controllers
{
    public class YemekTarifApiController : Controller
    {
        private readonly HttpClient _httpClient;

        public YemekTarifApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IActionResult> Index()
        {
            // Spoonacular API URL'si
            string apiKey = "0d781411fce24c08a9bcd77b278e5d25"; // Buraya API anahtarını ekle
            var yemekTarifleri = await _httpClient.GetFromJsonAsync<SpoonacularResponse>(
                $"https://api.spoonacular.com/recipes/complexSearch?apiKey={apiKey}&number=10");

            return View(yemekTarifleri.Results);
        }
    }

    public class SpoonacularResponse
    {
        public List<Tarif> Results { get; set; }
    }
}

