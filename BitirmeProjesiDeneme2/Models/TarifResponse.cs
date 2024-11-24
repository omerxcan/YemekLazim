namespace BitirmeProjesiDeneme2.Models
{
    public class TarifResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string image { get; set; } // Tarifin fotoğrafı
        public int readyInMinutes { get; set; } // Hazırlama süresi
        public int servings { get; set; } // Kaç kişilik
    }
}
