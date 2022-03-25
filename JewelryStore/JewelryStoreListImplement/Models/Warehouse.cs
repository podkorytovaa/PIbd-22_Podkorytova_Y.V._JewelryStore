using System;
using System.Collections.Generic;

namespace JewelryStoreListImplement.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string WarehouseName { get; set; }
        public string ResponsibleFullName { get; set; }
        public DateTime DateCreate { get; set; }
        public Dictionary<int, int> WarehouseComponents { get; set; }
    }
}
