﻿using JewelryStoreContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace JewelryStoreBusinessLogic.OfficePackage.HelperModels
{
    public class PdfInfo
    {
        public string FileName { get; set; }
        public string Title { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public List<ReportOrdersViewModel> Orders { get; set; }
        public List<ReportOrdersGroupedByDateViewModel> OrdersGroupedByDate { get; set; }
    }
}
