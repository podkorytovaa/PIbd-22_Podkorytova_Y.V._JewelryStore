using System.Collections.Generic;
using System.ComponentModel;

namespace JewelryStoreContracts.ViewModels
{
    // Изделие (драгоценность), изготавливаемое в магазине
    public class JewelViewModel
    {
        public int Id { get; set; }

        [DisplayName("Название драгоценности")]
        public string JewelName { get; set; }

        [DisplayName("Цена")]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> JewelComponents { get; set; }
    }
}
