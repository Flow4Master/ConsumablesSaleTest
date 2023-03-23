using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumablesSaleTest.Models
{
    public class TypeProd
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Тип печати")]
        public string Name { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage ="Введите порядок отображения")]
        [DisplayName("Порядок отображения")]
        public int DisplayOrder { get; set; }
    }
}
