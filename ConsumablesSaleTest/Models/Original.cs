using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumablesSaleTest.Models
{
    public enum Original
    {
        [Display(Name = "Оригинал")]
        Original,
        [Display(Name = "Совместимый")]
        Generic
    }
}
