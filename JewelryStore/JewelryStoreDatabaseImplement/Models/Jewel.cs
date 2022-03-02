using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JewelryStoreDatabaseImplement.Models
{
    public class Jewel
    {
        public int Id { get; set; }

        [Required]
        public string JewelName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("JewelId")]
        public virtual List<JewelComponent> JewelComponents { get; set; }

        [ForeignKey("JewelId")]
        public virtual List<Order> Orders { get; set; }
    }
}
