using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreContracts.StoragesContracts
{
    public interface IMessageInfoStorage
    {
        List<MessageInfoViewModel> GetFullList();

        List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model);

        MessageInfoViewModel GetElement(MessageInfoBindingModel model);

        void Insert(MessageInfoBindingModel model);

        void Update(MessageInfoBindingModel model);
    }
}
