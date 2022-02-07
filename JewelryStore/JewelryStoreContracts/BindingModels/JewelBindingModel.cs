using System.Collections.Generic;

namespace JewelryStoreContracts.BindingModels
{
    // Изделие (драгоценность), изготавливаемое в магазине
    public class JewelBindingModel
    {
        public int? Id { get; set; }
        public string JewelName { get; set; }
        public decimal Price { get; set; }
        public Dictionary<int, (string, int)> JewelComponents { get; set; }
    }
}
