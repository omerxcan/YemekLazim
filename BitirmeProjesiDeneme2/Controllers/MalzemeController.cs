using BitirmeProjesiDeneme2.Models;
using BitirmeProjesiDeneme2.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BitirmeProjesiDeneme2.Controllers
{
    public class MalzemeController : Controller
    {
        private readonly UygulamaDbContext _context;

        public MalzemeController(UygulamaDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Tüm malzemeleri veritabanından çekip view'a gönderiyoruz.
            var malzemeler = _context.Malzemeler.ToList();
            return View(malzemeler);
        }
        [HttpPost]
        public IActionResult FindRecipes(int[] selectedIngredients)
        {
            if (selectedIngredients == null || selectedIngredients.Length == 0)
            {
                ViewBag.EksikMalzemeBilgisi = "Lütfen en az bir malzeme seçin.";
                return View("FindRecipes", new List<(Tarif tarif, List<string> eksikler)>());
            }

            // Seçilen malzemelerle uyumlu tarifleri buluyoruz.
            var tarifler = _context.Tarifler.Include(t => t.TarifMalzemeler).ToList();
            var uygunTarifler = new List<(Tarif tarif, List<string> eksikler)>();

            foreach (var tarif in tarifler)
            {
                var tarifMalzemeleri = tarif.TarifMalzemeler.Select(tm => tm.MalzemeId).ToList();

                // Seçilen malzemelerin tarifteki malzemelerle tam olarak eşleşip eşleşmediğini kontrol et
                var eksikler = tarifMalzemeleri.Except(selectedIngredients).ToList();

                // Eğer eksik malzeme yoksa, tarifin kendisini göster
                if (!eksikler.Any() && tarifMalzemeleri.All(m => selectedIngredients.Contains(m)))
                {
                    uygunTarifler.Add((tarif, null)); // Tam eşleşme, eksik yok
                }
                else if (eksikler.Any() && selectedIngredients.Any(si => tarifMalzemeleri.Contains(si)))
                {
                    // Eksik malzemeleri ekle
                    var eksikMalzemeAdları = eksikler.Select(em => _context.Malzemeler.Find(em)?.Ad).ToList();
                    uygunTarifler.Add((tarif, eksikMalzemeAdları)); // Eksik malzemeler ile birlikte ekle
                }
            }

            return View("FindRecipes", uygunTarifler);
        }









    }
}