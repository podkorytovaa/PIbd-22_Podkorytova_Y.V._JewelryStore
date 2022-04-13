using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreBusinessLogic.OfficePackage.HelperModels
{
    public class WordInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<JewelViewModel> Jewels { get; set; }
        public List<WarehouseViewModel> Warehouses { get; set; }
    }
}
