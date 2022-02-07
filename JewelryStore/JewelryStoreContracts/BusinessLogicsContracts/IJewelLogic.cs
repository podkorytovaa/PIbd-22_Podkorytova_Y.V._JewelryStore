using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreContracts.BusinessLogicsContracts
{
    public interface IJewelLogic
    {
        List<JewelViewModel> Read(JewelBindingModel model);
        void CreateOrUpdate(JewelBindingModel model);
        void Delete(JewelBindingModel model);
    }
}
