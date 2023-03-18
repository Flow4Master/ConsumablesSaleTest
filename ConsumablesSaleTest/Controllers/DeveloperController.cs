using ConsumablesSaleTest.Data;
using ConsumablesSaleTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ConsumablesSaleTest.Controllers
{
    public class DeveloperController : Controller
    {
        private readonly AppDbContext _context;

        public DeveloperController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Developer> developerList = _context.Developers;
            return View(developerList);
        }
        //Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Developer developer)
        {
            if (ModelState.IsValid) 
            { 
                _context.Developers.Add(developer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(developer);
        }

        //Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var develop = _context.Developers.FirstOrDefault(x=>x.Id == id);
            if (develop == null)
                return NotFound();
            return View(develop);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Developer developer)
        {
            if (ModelState.IsValid)
            {
                _context.Developers.Update(developer);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(developer);
        }

        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var develop = _context.Developers.FirstOrDefault(x => x.Id == id);
            if (develop == null)
                return NotFound();
            return View(develop);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var develop = _context.Developers.FirstOrDefault(x => x.Id == id);
            if (develop == null)
                return NotFound();
            _context.Developers.Remove(develop);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
