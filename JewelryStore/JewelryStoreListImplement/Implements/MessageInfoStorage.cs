using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreListImplement.Models;
using System;
using System.Collections.Generic;

namespace JewelryStoreListImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly DataListSingleton source;

        public MessageInfoStorage()
        {
            source = DataListSingleton.GetInstance();
        }
        
        public List<MessageInfoViewModel> GetFullList()
        {
            var result = new List<MessageInfoViewModel>();
            foreach (var messageInfo in source.MessagesInfo)
            {
                result.Add(CreateModel(messageInfo));
            }
            return result;
        }
        
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            int toSkip = model.ToSkip ?? 0;
            int toTake = model.ToTake ?? source.MessagesInfo.Count;
            var result = new List<MessageInfoViewModel>();
            if (model.ToSkip.HasValue && model.ToTake.HasValue && !model.ClientId.HasValue)
            {
                foreach (var messageInfo in source.MessagesInfo)
                {
                    if (toSkip > 0) 
                    { 
                        toSkip--; 
                        continue; 
                    }
                    if (toTake > 0)
                    {
                        result.Add(CreateModel(messageInfo));
                        toTake--;
                    }
                }
                return result;
            }
            foreach (var messageInfo in source.MessagesInfo)
            {
                if ((model.ClientId.HasValue && messageInfo.ClientId == model.ClientId) || (!model.ClientId.HasValue && messageInfo.DateDelivery.Date == model.DateDelivery.Date))
                {
                    if (toSkip > 0) 
                    { 
                        toSkip--; 
                        continue; 
                    }
                    if (toTake > 0)
                    {
                        result.Add(CreateModel(messageInfo));
                        toTake--;
                    }
                }
            }
            return result;
        }

        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var message in source.MessagesInfo)
            {
                if (message.MessageId == model.MessageId)
                {
                    return CreateModel(message);
                }
            }
            return null;
        }

        public void Insert(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return;
            }
            source.MessagesInfo.Add(CreateModel(model, new MessageInfo()));
        }

        public void Update(MessageInfoBindingModel model)
        {
            MessageInfo element = null;
            foreach (var message in source.MessagesInfo)
            {
                if (message.MessageId == model.MessageId)
                {
                    element = message;
                    break;
                }
            }
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        private MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo messageInfo)
        {
            string clientFIO = string.Empty;
            foreach (var client in source.Clients)
            {
                if (client.Id == model.ClientId)
                {
                    clientFIO = client.ClientFIO;
                    break;
                }
            }
            messageInfo.MessageId = model.MessageId;
            messageInfo.ClientId = model.ClientId;
            messageInfo.SenderName = clientFIO;
            messageInfo.DateDelivery = model.DateDelivery;
            messageInfo.Subject = model.Subject;
            messageInfo.Body = model.Body;
            messageInfo.Checked = model.Checked;
            messageInfo.ReplyText = model.ReplyText;
            return messageInfo;
        }

        private MessageInfoViewModel CreateModel(MessageInfo messageInfo)
        {
            return new MessageInfoViewModel
            {
                MessageId = messageInfo.MessageId,
                SenderName = messageInfo.SenderName,
                DateDelivery = messageInfo.DateDelivery,
                Subject = messageInfo.Subject,
                Body = messageInfo.Body,
                Checked = messageInfo.Checked,
                ReplyText = messageInfo.ReplyText
            };
        }
    }
}
