using JewelryStoreContracts.ViewModels;
using JewelryStoreContracts.BindingModels;
using System.Collections.Generic;

namespace JewelryStoreContracts.BusinessLogicsContracts
{
    public interface IClientLogic
    {
        List<ClientViewModel> Read(ClientBindingModel model);

        void CreateOrUpdate(ClientBindingModel model);

        void Delete(ClientBindingModel model);
    }
}
