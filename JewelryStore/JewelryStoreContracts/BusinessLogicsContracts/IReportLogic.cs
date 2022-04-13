using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreContracts.BusinessLogicsContracts
{
    public interface IReportLogic
    {
        List<ReportJewelComponentViewModel> GetJewelComponent();

        List<ReportWarehouseComponentViewModel> GetWarehouseComponent();

        List<ReportOrdersViewModel> GetOrders(ReportBindingModel model);

        List<ReportOrdersGroupedByDateViewModel> GetOrdersGroupedByDate();

        void SaveJewelsToWordFile(ReportBindingModel model);

        void SaveWarehousesToWordFile(ReportBindingModel model);

        void SaveJewelComponentToExcelFile(ReportBindingModel model);

        void SaveWarehouseComponentToExcelFile(ReportBindingModel model);

        void SaveOrdersToPdfFile(ReportBindingModel model);

        void SaveOrdersGroupedByDateToPdfFile(ReportBindingModel model);
    }
}
