using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shop_app.Models
{
    public class Product
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required(ErrorMessage = "Id is required")]
        public Guid Id { get; set; } = Guid.NewGuid(); // Задаем `Guid` для `Id`

        [Required(ErrorMessage = "Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "min: 2 max: 100")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 1000000.00, ErrorMessage = "min: 0.01 max: 1000000.00")]
        [Column(TypeName = "decimal(18,2)")]
        //[RegularExpression(@"^\d+([.,]\d{1,2})?$", ErrorMessage = "Price must have up to 2 decimal places and use a dot or comma as the decimal separator.")]
        public decimal Price { get; set; } = decimal.Zero;

        [Required(ErrorMessage = "Description is required")]
        [StringLength(1024, MinimumLength = 2, ErrorMessage = "min: 2 max: 1024")]
        public string Description { get; set; } = string.Empty;
    }
}
