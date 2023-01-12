using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoreAndFood.Data.Models
{
    public class Category
    {
        //Tablo propertyleri
        public int CategoryID { get; set; }
        
        [Required(ErrorMessage = "Category name cannot null")]
        [StringLength(20, MinimumLength =3, ErrorMessage = "Character name can be 3-20 characters")]
        public string CategoryName { get; set; }

        public string CategoryDescription { get; set; }
       
        public bool Status { get; set; }

        //Tablo bağlantıları
        public List<Food> Foods { get; set; }
    }
}
