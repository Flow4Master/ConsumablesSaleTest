using ConsumablesSaleTest.Data;
using ConsumablesSaleTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ConsumablesSaleTest.Controllers
{
    public class TypeController : Controller
    {
        private readonly AppDbContext _context;

        public TypeController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Type> typeList = _context.Types;
            return View(typeList);
        }

        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Type type)
        {
            if (ModelState.IsValid)
            {
                _context.Types.Add(type);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        //Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var type = _context.Types.FirstOrDefault(x => x.Id == id);
            if (type == null)
                return NotFound();
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Type type)
        {
            if (ModelState.IsValid)
            {
                _context.Types.Update(type);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(type);
        }

        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var type = _context.Types.FirstOrDefault(x => x.Id == id);
            if (type == null)
                return NotFound();
            return View(type);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var type = _context.Types.FirstOrDefault(x => x.Id == id);
            if (type == null)
                return NotFound(id);
            _context.Types.Remove(type);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
