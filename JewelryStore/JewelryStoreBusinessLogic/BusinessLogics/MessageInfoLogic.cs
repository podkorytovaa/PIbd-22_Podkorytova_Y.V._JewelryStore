using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using System.Collections.Generic;

namespace JewelryStoreBusinessLogic.BusinessLogics
{
    public class MessageInfoLogic : IMessageInfoLogic
    {
        private readonly IMessageInfoStorage _messageInfoStorage;

        public MessageInfoLogic(IMessageInfoStorage messageInfoStorage)
        {
            _messageInfoStorage = messageInfoStorage;
        }

        public List<MessageInfoViewModel> Read(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return _messageInfoStorage.GetFullList();
            }
            return _messageInfoStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(MessageInfoBindingModel model)
        {
            _messageInfoStorage.Insert(model);
        }
    }
}
