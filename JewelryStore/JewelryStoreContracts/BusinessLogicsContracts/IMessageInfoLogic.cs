using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreContracts.BusinessLogicsContracts
{
    public interface IMessageInfoLogic
    {
        List<MessageInfoViewModel> Read(MessageInfoBindingModel model);

        void CreateOrUpdate(MessageInfoBindingModel model);
    }
}
