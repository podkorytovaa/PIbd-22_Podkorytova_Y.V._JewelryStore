using System;
using System.Collections.Generic;

namespace JewelryStoreContracts.ViewModels
{
    public class ReportJewelComponentViewModel
    {
        public string JewelName { get; set; }
        public int TotalCount { get; set; }
        public List<Tuple<string, int>> Components { get; set; }
    }
}
