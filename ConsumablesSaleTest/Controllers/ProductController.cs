using ConsumablesSaleTest.Data;
using ConsumablesSaleTest.Models;
using ConsumablesSaleTest.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using static System.Net.WebRequestMethods;

namespace ConsumablesSaleTest.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _context.Products.Include(x => x.Type).Include(x => x.Developer);
            //foreach (var item in productList)
            //{
            //    item.Type = _context.Types.FirstOrDefault(x => x.Id == item.TypeId);
            //    item.Developer = _context.Developers.FirstOrDefault(x => x.Id == item.DeveloperId);
            //}
            return View(productList);
        }

        //Upsert
        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> TypeDropDown = _context.Types.Select(x => new SelectListItem
            //{
            //    Text = x.Name,
            //    Value = x.Id.ToString()
            //});
            ////ViewBag.TypeDropDown = TypeDropDown;
            //ViewData["TypeDropDown"] = TypeDropDown;
            //Product product = new Product();
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                TypeSelectList = _context.Types.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }),
                DeveloperSelectList = _context.Developers.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
            };
            if (id == null)
                return View(productVM);
            else
            {
                productVM.Product = _context.Products.FirstOrDefault(x => x.Id == id);
                if (productVM.Product == null)
                    return NotFound();
                return View(productVM);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var files = HttpContext.Request.Form.Files;
                string webRootPath = _webHostEnvironment.WebRootPath;
                if (productVM.Product.Id == 0)
                {
                    string upload = webRootPath + Constant.ImagePath;
                    string fileName = Guid.NewGuid().ToString();
                    string extension = Path.GetExtension(files[0].FileName);
                    using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStream);
                    }
                    productVM.Product.Img = fileName + extension;
                    _context.Products.Add(productVM.Product);
                }
                else
                {
                    var prodFromDb = _context.Products.AsNoTracking().FirstOrDefault(x => x.Id == productVM.Product.Id);
                    if (files.Count > 0)
                    {
                        string upload = webRootPath + Constant.ImagePath;
                        string fileName = Guid.NewGuid().ToString();
                        string extension = Path.GetExtension(files[0].FileName);
                        var fileToDelete = Path.Combine(upload, prodFromDb.Img);
                        if (System.IO.File.Exists(fileToDelete))
                            System.IO.File.Delete(fileToDelete);
                        using (var fileStream = new FileStream(Path.Combine(upload, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productVM.Product.Img = fileName + extension;
                    }
                    else
                    {
                        productVM.Product.Img = prodFromDb.Img;
                    }
                    _context.Update(productVM.Product);
                }
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            productVM.TypeSelectList = _context.Types.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            productVM.DeveloperSelectList = _context.Developers.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            });
            return View(productVM);
        }

        //Delete
        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
                return NotFound();
            var product = _context.Products.Include(x => x.Type).Include(x => x.Developer).FirstOrDefault(x => x.Id == id);
            if (product == null)
                return NotFound();
            return View(product);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var prodFromDb = _context.Products.FirstOrDefault(x => x.Id == id);
            if (prodFromDb == null)
                return NotFound();
            string upload = _webHostEnvironment.WebRootPath + Constant.ImagePath;
            var fileToDelete = Path.Combine(upload, prodFromDb.Img);
            if (System.IO.File.Exists(fileToDelete))
                System.IO.File.Delete(fileToDelete);
            _context.Products.Remove(prodFromDb);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
