using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreFileImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        private readonly FileDataListSingleton source;

        public MessageInfoStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<MessageInfoViewModel> GetFullList()
        {
            return source.MessagesInfo
                .Select(CreateModel)
                .ToList();
        }

        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            if (model.ToSkip.HasValue && model.ToTake.HasValue && !model.ClientId.HasValue)
            {
                return source.MessagesInfo
                    .Skip((int)model.ToSkip)
                    .Take((int)model.ToTake)
                    .Select(CreateModel)
                    .ToList();
            }
            return source.MessagesInfo.Where(rec => (model.ClientId.HasValue && rec.ClientId == model.ClientId) || (!model.ClientId.HasValue && rec.DateDelivery.Date == model.DateDelivery.Date))
                .Skip(model.ToSkip ?? 0)
                .Take(model.ToTake ?? source.MessagesInfo.Count())
                .Select(CreateModel)
                .ToList();
        }

        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            var message = source.MessagesInfo.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            return message != null ? CreateModel(message) : null;
        }

        public void Insert(MessageInfoBindingModel model)
        {
            MessageInfo element = source.MessagesInfo.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким идентификатором");
            }
            source.MessagesInfo.Add(CreateModel(model, new MessageInfo()));
        }

        public void Update(MessageInfoBindingModel model)
        {
            MessageInfo element = source.MessagesInfo.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
        }

        private MessageInfoViewModel CreateModel(MessageInfo model)
        {
            return new MessageInfoViewModel
            {
                MessageId = model.MessageId,
                SenderName = model.SenderName,
                DateDelivery = model.DateDelivery,
                Subject = model.Subject,
                Body = model.Body,
                Checked = model.Checked,
                ReplyText = model.ReplyText
            };
        }

        private static MessageInfo CreateModel(MessageInfoBindingModel model, MessageInfo message)
        {
            message.MessageId = model.MessageId;
            message.ClientId = model.ClientId;
            message.SenderName = model.FromMailAddress;
            message.DateDelivery = model.DateDelivery;
            message.Subject = model.Subject;
            message.Body = model.Body;
            message.Checked = model.Checked;
            message.ReplyText = model.ReplyText;
            return message;
        }
    }
}
