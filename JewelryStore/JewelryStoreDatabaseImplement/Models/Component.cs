﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace JewelryStoreDatabaseImplement.Models
{
    // Компонент, требуемый для изготовления изделия
    public class Component
    {
        public int Id { get; set; }

        [Required]
        public string ComponentName { get; set; }

        [ForeignKey("ComponentId")]
        public virtual List<JewelComponent> JewelComponents { get; set; }
    }
}
