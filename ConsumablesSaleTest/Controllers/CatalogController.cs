using ConsumablesSaleTest.Data;
using ConsumablesSaleTest.Models;
using ConsumablesSaleTest.Models.ViewModels;
using ConsumablesSaleTest.Sevices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ConsumablesSaleTest.Controllers
{
    public class CatalogController : Controller
    {
        private readonly AppDbContext _context;

        public CatalogController(AppDbContext context)
        {
            
            _context = context;
        }

        public IActionResult Index(int? developer, int? typeProd, string search, Original original)
        {
            IQueryable<Product> products = _context.Products.Include(x => x.Developer).Include(x => x.TypeProd);
            if (developer != null && developer != 0)
                products = products.Where(x => x.DeveloperId == developer);
            if (typeProd != null && typeProd != 0)
                products = products.Where(x => x.TypeProdId == typeProd);
            if (original != Original.All)
                products = products.Where(x => x.Original == original);
            if (!String.IsNullOrEmpty(search))
                products = products.Where(p => p.Name.Contains(search));
            List<Developer> developers = _context.Developers.ToList();
            List<TypeProd> types = _context.Types.ToList();
            developers.Insert(0, new Developer { Name = "Все", Id = 0 });
            types.Insert(0, new TypeProd { Name = "Все", Id = 0 });
            CatalogVM catalogVM = new CatalogVM()
            {
                Products = products.ToList(),
                Developers = new SelectList(developers, "Id", "Name"),
                Types = new SelectList(types, "Id", "Name"),
                Original = original,
                Name = search,
                DevId = developer,
                TypId= typeProd
            };
            return View(catalogVM);


            //CatalogVM catalogVM = new CatalogVM()
            //{
            //    Products = _context.Products.Include(x => x.Type).Include(x => x.Developer),
            //    Types = _context.Types,
            //    Developers = _context.Developers
            //};
            //return View(catalogVM);
        }

        public IActionResult Details(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(Constant.SessionCartKey) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(Constant.SessionCartKey).Count() > 0)
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(Constant.SessionCartKey);
            DetailsVM detailsVM = new DetailsVM()
            {
                Product = _context.Products.Include(x => x.Developer).Include(x => x.TypeProd)
                .Where(x => x.Id == id).FirstOrDefault(),
                InCard = false
            };

            foreach (var prod in shoppingCartList)
            {
                if (prod.ProductId == id) 
                { 
                    detailsVM.InCard = true;
                }
            }

            return View(detailsVM);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public IActionResult DetailsPost(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(Constant.SessionCartKey) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(Constant.SessionCartKey).Count() > 0)
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(Constant.SessionCartKey);
            shoppingCartList.Add(new ShoppingCart { ProductId= id });
            HttpContext.Session.Set(Constant.SessionCartKey, shoppingCartList);
            return RedirectToAction(nameof(Details));
        }

        public IActionResult RemoveFromCard(int id)
        {
            List<ShoppingCart> shoppingCartList = new List<ShoppingCart>();
            if (HttpContext.Session.Get<IEnumerable<ShoppingCart>>(Constant.SessionCartKey) != null &&
                HttpContext.Session.Get<IEnumerable<ShoppingCart>>(Constant.SessionCartKey).Count() > 0)
                shoppingCartList = HttpContext.Session.Get<List<ShoppingCart>>(Constant.SessionCartKey);
            var itemRemove = shoppingCartList.SingleOrDefault(x => x.ProductId == id);
            if (itemRemove != null)
                shoppingCartList.Remove(itemRemove);
            HttpContext.Session.Set(Constant.SessionCartKey, shoppingCartList);
            return RedirectToAction(nameof(Index));
        }
    }
}
