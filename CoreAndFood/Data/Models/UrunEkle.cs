using Microsoft.AspNetCore.Http;

namespace CoreAndFood.Data.Models
{
    public class UrunEkle
    {
        public string FoodName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public IFormFile ImageURL { get; set; }
        public int Stock { get; set; }

        //Tablo bağlantıları
        public int CategoryID { get; set; }
    }
}
