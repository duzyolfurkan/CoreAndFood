namespace CoreAndFood.Data.Models
{
    public class Food
    {
        public int FoodID { get; set; }
        public string FoodName { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageURL { get; set; }
        public int Stock { get; set; }

        //Tablo bağlantıları
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}
