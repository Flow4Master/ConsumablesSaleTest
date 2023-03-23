using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections;
using System.Collections.Generic;

namespace ConsumablesSaleTest.Models.ViewModels
{
    public class CatalogVM
    {
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Developers { get; set; }
        public  Original Original { get; set; }
        public string Name { get; set; }
        public int? DevId { get; set; }
        public int? TypId { get; set; }
    }
}
