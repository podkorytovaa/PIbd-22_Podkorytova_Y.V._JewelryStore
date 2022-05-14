using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreDatabaseImplement.Implements
{
    public class MessageInfoStorage : IMessageInfoStorage
    {
        public List<MessageInfoViewModel> GetFullList()
        {
            using var context = new JewelryStoreDatabase();
            return context.MessagesInfo
                .Select(CreateModel)
                .ToList();
        }
        
        public List<MessageInfoViewModel> GetFilteredList(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new JewelryStoreDatabase();
            if (model.ToSkip.HasValue && model.ToTake.HasValue && !model.ClientId.HasValue)
            {
                return context.MessagesInfo
                    .Skip((int)model.ToSkip)
                    .Take((int)model.ToTake)
                    .Select(CreateModel)
                    .ToList();
            }
            return context.MessagesInfo
                .Where(rec => (model.ClientId.HasValue && rec.ClientId == model.ClientId) || (!model.ClientId.HasValue && rec.DateDelivery.Date == model.DateDelivery.Date))
                .Skip(model.ToSkip ?? 0)
                .Take(model.ToTake ?? context.MessagesInfo.Count())
                .Select(CreateModel)
                .ToList();
        }

        public MessageInfoViewModel GetElement(MessageInfoBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new JewelryStoreDatabase();
            var message = context.MessagesInfo.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            return message != null ? CreateModel(message) : null;
        }

        public void Insert(MessageInfoBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            MessageInfo element = context.MessagesInfo.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element != null)
            {
                throw new Exception("Уже есть письмо с таким идентификатором");
            }
            context.MessagesInfo.Add(CreateModel(model, new MessageInfo()));
            context.SaveChanges();
        }

        public void Update(MessageInfoBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            MessageInfo element = context.MessagesInfo.FirstOrDefault(rec => rec.MessageId == model.MessageId);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
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
