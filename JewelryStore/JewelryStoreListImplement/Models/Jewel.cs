using System.Collections.Generic;

namespace JewelryStoreListImplement.Models
{
    // Изделие (драгоценность), изготавливаемое в магазине
    public class Jewel
    {
        public int Id { get; set; }
        public string JewelName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, int> JewelComponents { get; set; }
    }
}
