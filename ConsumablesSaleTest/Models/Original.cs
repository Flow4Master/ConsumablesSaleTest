using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumablesSaleTest.Models
{
    public enum Original
    {
        [Display(Name = "Все")]
        All,
        [Display(Name = "Оригинальный")]
        Original,
        [Display(Name = "Совместимый")]
        Generic
        
    }
}
