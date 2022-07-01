using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class AuthorController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthorController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Author> objAuthors = _db.Authors.ToList();
            return View(objAuthors);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Author obj = new Author();
            if (id == null) return View(obj);

            obj = _db.Authors.FirstOrDefault(x => x.Author_Id == id);
            if (obj == null) return NotFound();

            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Author obj)
        {
            if(ModelState.IsValid)
            {
                if (obj.Author_Id == 0)
                    _db.Authors.Add(obj);
                else
                    _db.Authors.Update(obj);

                _db.SaveChanges();

                return RedirectToAction(nameof(Index));
            }

            return View(obj);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            Author author = _db.Authors.FirstOrDefault(x => x.Author_Id == id);
            _db.Authors.Remove(author);
            _db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

    }
}
