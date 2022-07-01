﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using WizLib_DataAccess.Data;
using WizLib_Model.Models;

namespace WizLib.Controllers
{
    public class PublisherController : Controller
    {
        private readonly ApplicationDbContext _db;

        public PublisherController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<Publisher> objPublisher = _db.Publishers.ToList();
            return View(objPublisher);
        }

        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            Publisher publisher = new Publisher(); 
            if (id == null) return View(publisher); 

            publisher = _db.Publishers.FirstOrDefault(p => p.Publisher_Id == id);
            if (publisher == null) return NotFound();

            return View(publisher);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Publisher obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.Publisher_Id == 0)
                    _db.Publishers.Add(obj);
                else
                    _db.Publishers.Update(obj);

                _db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(obj);
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            Publisher publisher = _db.Publishers.FirstOrDefault(p => p.Publisher_Id == id);
            _db.Publishers.Remove(publisher);
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
