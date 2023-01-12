using CoreAndFood.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;

namespace CoreAndFood.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VisualizeProductResult()
        {
            return Json(ProList());
        }

        //Statik liste tanımlandı
        public List<Class1> ProList()
        {
            List<Class1> cs = new List<Class1>();
            cs.Add(new Class1()
            {
                proname = "computer",
                stock = 150
            });
            cs.Add(new Class1()
            {
                proname = "Lcd",
                stock = 75
            });
            cs.Add(new Class1()
            {
                proname = "Usb Disk",
                stock = 220
            });

            return cs;
        }

        public IActionResult Index2()
        { 
            return View();
        }

        public IActionResult Index3()
        {
            return View();

        }

        public IActionResult VisualizeProductResult2()
        {
            return Json(FoodList());
        }

        //Veritabanı ile bağlantılı dinamik liste tanımlandı
        Context db = new Context();
        public List<Class2> FoodList()
        {
            List<Class2> cs2 = new List<Class2>();
            using (var v = new Context())
            {
                cs2 = db.Foods.Select(x => new Class2
                {
                    foodname = x.FoodName,
                    stock = x.Stock
                }).ToList();
            }
            return cs2;
        }

        public IActionResult Statistics()
        {
            var totalFood = db.Foods.Count();
            ViewBag.totalFood = totalFood;

            var totalCategory = db.Categories.Count();
            ViewBag.totalCategory = totalCategory;

            int fruitCategoryID = db.Categories.Where(x => x.CategoryName == "Fruit").FirstOrDefault().CategoryID;
            var fruitCount = db.Foods.Where(x => x.CategoryID == fruitCategoryID).Count();
            ViewBag.fruitCount = fruitCount;

            int vegetableCategoryID = db.Categories.Where(x => x.CategoryName == "Vegetables").FirstOrDefault().CategoryID;
            var vegetableCount = db.Foods.Where(x => x.CategoryID == vegetableCategoryID).Count();
            ViewBag.vegetableCount = vegetableCount;

            var sumFood = db.Foods.Sum(x => x.Stock);
            ViewBag.sumFood = sumFood;

            int legumeCategoryID = db.Categories.Where(x => x.CategoryName == "Legumes").FirstOrDefault().CategoryID;
            var legumeCount = db.Foods.Where(x => x.CategoryID == legumeCategoryID).Count();
            ViewBag.legumeCount = legumeCount;

            var maxStokUrun = db.Foods.OrderByDescending(x => x.Stock).Select(y => y.FoodName).FirstOrDefault();
            ViewBag.maxStock = maxStokUrun;

            var minUrunStok = db.Foods.OrderBy(x => x.Stock).Select(y => y.FoodName).FirstOrDefault();
            ViewBag.minStock = minUrunStok;

            var averageFoodPrice = db.Foods.Average(x => x.Price).ToString();
            ViewBag.averageFoodPrice = averageFoodPrice;

            var totalFruitsCountID = db.Categories.Where(x => x.CategoryName == "Fruit").FirstOrDefault().CategoryID;
            var totalFruitStock = db.Foods.Where(x=>x.CategoryID == totalFruitsCountID).Sum(x=>x.Stock).ToString();
            ViewBag.totalFruitStock = totalFruitStock;

            var totalVegetablesCountID = db.Categories.Where(x => x.CategoryName == "Vegetables").FirstOrDefault().CategoryID;
            var totalVegetableStock = db.Foods.Where(x => x.CategoryID == totalVegetablesCountID).Sum(x => x.Stock).ToString();
            ViewBag.totalVegetableStock = totalVegetableStock;

            var highestPriceProduct = db.Foods.OrderByDescending(x => x.Price).FirstOrDefault();
            ViewBag.highestPriceProductName = highestPriceProduct.FoodName;

            return View();
        }
    }
}
