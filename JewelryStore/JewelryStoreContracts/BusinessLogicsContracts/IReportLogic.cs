using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportJewelComponentViewModel> GetJewelComponent();

        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);
        
        void SaveJewelsToWordFile(ReportBindingModel model);

        void SaveJewelComponentToExcelFile(ReportBindingModel model);
        
        void SaveOrdersToPdfFile(ReportBindingModel model);
    }
}
