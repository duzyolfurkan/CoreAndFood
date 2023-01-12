using CoreAndFood.Data;
using CoreAndFood.Data.Models;
using CoreAndFood.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreAndFood.Controllers
{
    public class CategoryController : Controller
    {
        //Veritabanı bağlantısı
        Context db = new Context();
        //Category Repository'e erişim sağlama
        CategoryRepository categoryRepository = new CategoryRepository();

        //[Authorize]
        public IActionResult Index(string p)
        {
            if (!string.IsNullOrEmpty(p))
            {
                return View(categoryRepository.List(x => x.CategoryName == p));
            }
            return View(categoryRepository.TList());
        }

        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            //Eğer kategori ekleme içerisinde model valid olmazsa aşağıdaki işlemler yapılacaktır.
            //Model'in valid omlası için required olan tüm elemanların girişi yapılmış olmalıdır.
            if (!ModelState.IsValid)
            {
                return View("AddCategory");
            }

            categoryRepository.TAdd(category);
            return RedirectToAction("Index");
        }

        public IActionResult GetCategory(int id)
        {
            var x = categoryRepository.TGet(id);
            Category ct = new Category()
            {
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription,
                CategoryID = x.CategoryID
            };
            return View(ct);
        }

        [HttpPost]
        public IActionResult CategoryUpdate(Category category)
        {
            var x = categoryRepository.TGet(category.CategoryID);
            x.CategoryName = category.CategoryName;
            x.CategoryDescription = category.CategoryDescription;
            x.Status = true;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }

        public IActionResult DeleteCategory(int id)
        {
            var x = categoryRepository.TGet(id);
            x.Status = false;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }

    }
}
