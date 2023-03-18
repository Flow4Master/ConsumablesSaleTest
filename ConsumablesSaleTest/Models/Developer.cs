using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ConsumablesSaleTest.Models
{
    public class Developer
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("Производитель")]
        public string Name { get; set; }
    }
}
