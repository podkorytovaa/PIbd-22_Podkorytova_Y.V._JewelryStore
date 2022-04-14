using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace JewelryStoreDatabaseImplement.Models
{
    public class Implementer
    {
        public int Id { get; set; }

        [Required]
        public string ImplementerFIO { get; set; }

        [Required]
        public int WorkingTime { get; set; }

        [Required]
        public int PauseTime { get; set; }

        [ForeignKey("ImplementerId")]
        public virtual List<Order> Orders { get; set; }
    }
}
