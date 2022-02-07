using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreContracts.StoragesContracts
{
    public interface IJewelStorage
    {
        List<JewelViewModel> GetFullList();
        List<JewelViewModel> GetFilteredList(JewelBindingModel model);
        JewelViewModel GetElement(JewelBindingModel model);
        void Insert(JewelBindingModel model);
        void Update(JewelBindingModel model);
        void Delete(JewelBindingModel model);
    }
}
