using JewelryStoreContracts.Attributes;
using System.Collections.Generic;

namespace JewelryStoreContracts.ViewModels
{
    // Изделие (драгоценность), изготавливаемое в магазине
    public class JewelViewModel
    {
        [Column(title: "Номер", width: 60)]
        public int Id { get; set; }

        [Column(title: "Драгоценность", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string JewelName { get; set; }

        [Column(title: "Цена", width: 120)]
        public decimal Price { get; set; }

        public Dictionary<int, (string, int)> JewelComponents { get; set; }
    }
}
