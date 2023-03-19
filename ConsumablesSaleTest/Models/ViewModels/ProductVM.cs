using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ConsumablesSaleTest.Models.ViewModels
{
    public class ProductVM
    {
        public Product Product { get; set; }
        public IEnumerable<SelectListItem> TypeSelectList { get;set; }
        public IEnumerable<SelectListItem> DeveloperSelectList { get; set; }
    }
}
