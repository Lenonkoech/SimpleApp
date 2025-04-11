using System.ComponentModel.DataAnnotations;

namespace ADO.Net_App.Models
{
    public class Product
    {
        [Key]
        public int Id { set; get; }
        [Required(ErrorMessage ="Name is required")]
        public string? Name { set; get; }
        [Required(ErrorMessage = "Quantity is required")]

        public int Quantity { set; get; }
        [Required(ErrorMessage = "Price is required")]

        public double Price { set; get; }
        [Required(ErrorMessage = "Stock is required")]

        public int Stock { set;get; }
        [Required(ErrorMessage = "Description is required")]

        public string? Description { set;get; }
    }
}
