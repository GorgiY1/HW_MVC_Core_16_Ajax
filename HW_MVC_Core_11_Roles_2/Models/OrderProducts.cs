namespace HW_MVC_Core_11_Roles_2.Models
{
    using Shop_app.Models;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using System;
    using System.Text.Json.Serialization;

    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required(ErrorMessage = "Order Date is required")]
        public DateTime OrderDate { get; set; }

        [JsonIgnore] // Prevent serialization of the navigation property
        // Навігаційна властивість: один замовлення має багато продуктів
        public virtual ICollection<OrderProducts> OrderProducts { get; set; } = new List<OrderProducts>();
    }

    public class OrderProducts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid OrderId { get; set; } // Зовнішній ключ до замовлення

        [Required]
        public Guid ProductId { get; set; } // Зовнішній ключ до продукту

        [Required]
        [Range(1, 10000, ErrorMessage = "Quantity must be between 1 and 10,000")]
        public int Quantity { get; set; } // Кількість продуктів

        // Навігаційна властивість: зв'язок з `Order`
        [ForeignKey(nameof(OrderId))]
        [JsonIgnore] // Prevent serialization of the navigation property
        public virtual Order Order { get; set; }

        // Навігаційна властивість: зв'язок з `Product`
        [ForeignKey(nameof(ProductId))]
        [JsonIgnore] // Prevent serialization of the navigation property
        public virtual Product Product { get; set; }
    }
}
