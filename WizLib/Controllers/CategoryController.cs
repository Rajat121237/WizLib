using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Category> objList = _db.Categories.ToList();
            return View(objList);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Category obj = new Category();
            if (id == null) return View(obj);

            obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            if (obj == null) return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Category obj)
        {
            if (ModelState.IsValid)
            {
                if(obj.Id == 0)
                    _db.Categories.Add(obj);
                else
                    _db.Categories.Update(obj);

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Category obj = _db.Categories.FirstOrDefault(u => u.Id == id);
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateMultiple2()
        {
            List<Category> objList = new List<Category>();
            for (int i=1; i<=2; i++)
            {
                //_db.Categories.Add(new Category() { Name = Guid.NewGuid().ToString() });
                objList.Add(new Category() { Name = Guid.NewGuid().ToString() });
            }
            _db.AddRange(objList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult CreateMultiple5()
        {
            List<Category> objList = new List<Category>();
            for (int i=1; i<=5; i++)
            {
                //_db.Categories.Add(new Category() { Name = Guid.NewGuid().ToString() });
                objList.Add(new Category() { Name = Guid.NewGuid().ToString() });
            }
            _db.AddRange(objList);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RemoveMultiple2()
        {
            IEnumerable<Category> objCategory = _db.Categories.OrderByDescending(c => c.Id).Take(2);
            _db.Categories.RemoveRange(objCategory);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult RemoveMultiple5()
        {
            IEnumerable<Category> objCategory = _db.Categories.OrderByDescending(c => c.Id).Take(5);
            _db.Categories.RemoveRange(objCategory);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
