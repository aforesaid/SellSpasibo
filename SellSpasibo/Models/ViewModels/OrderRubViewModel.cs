using System;
using System.ComponentModel.DataAnnotations;

namespace SellSpasibo.Models.ViewModels
{
    public class OrderRubViewModel
    {
        [Required]
        public string Number { get; set; }
        [Required]
        public string Cost { get; set; }
        [Required]
        public DateTime Date { get; set; }
    }
}
