using System;
using System.ComponentModel.DataAnnotations;

namespace Hostel
{
    public class MessDishes
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string DishName { get; set; }
        [Required]
        public decimal Price { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }
    }
}