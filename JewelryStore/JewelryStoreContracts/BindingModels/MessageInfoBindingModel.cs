using System;

namespace JewelryStoreContracts.BindingModels
{
    // Сообщения, приходящие на почту
    public class MessageInfoBindingModel
    {
        public int? ClientId { get; set; }
        public string MessageId { get; set; }
        public string FromMailAddress { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public DateTime DateDelivery { get; set; }
        public bool Checked { get; set; }
        public string ReplyText { get; set; }
        public int? ToSkip { get; set; }
        public int? ToTake { get; set; }
    }
}
