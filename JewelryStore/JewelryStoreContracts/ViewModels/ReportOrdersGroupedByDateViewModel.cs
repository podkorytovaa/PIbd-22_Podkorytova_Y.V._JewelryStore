using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JewelryStoreContracts.ViewModels
{
    public class ReportOrdersGroupedByDateViewModel
    {
        public DateTime DateCreate { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
