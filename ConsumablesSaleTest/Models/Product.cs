using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConsumablesSaleTest.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Необходимо указать название расходника")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Необходимо указать артикул расходника")]
        public string ArticleNumber { get; set; }
        [Required(ErrorMessage = "Необходимо указать ресурс расходника")]
        public int Resource { get; set; }
        [Required(ErrorMessage = "Необходимо указать цвет расходника")]
        public string Color { get; set; }
        public string Compatibility { get; set; }
        public string Description { get; set; }
        [Range(1, int.MaxValue)]
        public double Price { get; set; }
        public string Img { get; set; }
        [Required(ErrorMessage = "Необходимо выбрать тип расходника")]
        public virtual Original Original { get; set; }
        [Display(Name= "Type type")]
        public int TypeId { get; set; }
        [ForeignKey("TypeId")]
        public virtual Type Type { get; set; }
        [Display(Name = "Developer type")]
        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public virtual Developer Developer { get; set; }
    }
}
