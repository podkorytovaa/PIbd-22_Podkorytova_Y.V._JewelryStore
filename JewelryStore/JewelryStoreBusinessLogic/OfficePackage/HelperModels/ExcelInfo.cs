using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreBusinessLogic.OfficePackage.HelperModels
{
    public class ExcelInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public List<ReportJewelComponentViewModel> JewelComponents { get; set; }
        public List<ReportWarehouseComponentViewModel> WarehouseComponents { get; set; }
    }
}
