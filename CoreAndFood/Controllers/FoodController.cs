using CoreAndFood.Data;
using CoreAndFood.Data.Models;
using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using X.PagedList;

namespace CoreAndFood.Controllers
{
    public class FoodController : Controller
    {
        FoodRepository foodRepository = new FoodRepository();
        Context db = new Context();

        public IActionResult Index(int page = 1)
        {
            return View(foodRepository.TList().ToPagedList(page, 3));
        }

        [HttpGet]
        public IActionResult AddFood()
        {
            List<SelectListItem> categoryList = (from x in db.Categories.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = x.CategoryName,
                                                     Value = x.CategoryID.ToString()
                                                 }).ToList();
            ViewBag.categoriesList = categoryList;
            return View();
        }

        [HttpPost]
        public IActionResult AddFood(UrunEkle food)
        {
            Food f = new Food();
            if (food.ImageURL != null)
            {
                var extension = Path.GetExtension(food.ImageURL.FileName);
                var newimagename = Guid.NewGuid() + extension;
                var location = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/resimler/", newimagename);
                var stream = new FileStream(location, FileMode.Create);
                food.ImageURL.CopyTo(stream);
                f.ImageURL = newimagename;
            }
            
            f.FoodName = food.FoodName;
            f.Price = food.Price;
            f.Stock = food.Stock;
            f.CategoryID = food.CategoryID;
            f.Description = food.Description;

            foodRepository.TAdd(f);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteFood(int id)
        {
            foodRepository.TDelete(new Food { FoodID = id});
            return RedirectToAction("Index");
        }

        public IActionResult GetFood(int id)
        {
            var x = foodRepository.TGet(id);

            List<SelectListItem> categoryList = (from y in db.Categories.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Text = y.CategoryName,
                                                     Value = y.CategoryID.ToString()
                                                 }).ToList();
            ViewBag.categoriesList = categoryList;

            Food f = new Food()
            {
                CategoryID = x.CategoryID,
                FoodName = x.FoodName,
                Price = x.Price,
                Stock = x.Stock,
                Description = x.Description,
                ImageURL = x.ImageURL
            };

            return View(f);
        }

        [HttpPost]
        public IActionResult FoodUpdate(Food p)
        {
            var x = foodRepository.TGet(p.FoodID);

            x.FoodName = p.FoodName;
            x.Stock = p.Stock;
            x.Price = p.Price;
            x.ImageURL = p.ImageURL;
            x.Description = p.Description;
            x.CategoryID = p.CategoryID;

            foodRepository.TUpdate(x);

            return RedirectToAction("Index");
        }
    }
}
