using JewelryStoreContracts.ViewModels;
using JewelryStoreContracts.BindingModels;
using System.Collections.Generic;

namespace JewelryStoreContracts.BusinessLogicsContracts
{
    public interface IImplementerLogic
    {
        List<ImplementerViewModel> Read(ImplementerBindingModel model);

        void CreateOrUpdate(ImplementerBindingModel model);

        void Delete(ImplementerBindingModel model);
    }
}
