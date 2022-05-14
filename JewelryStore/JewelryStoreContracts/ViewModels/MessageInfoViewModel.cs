using System;
using System.ComponentModel;


namespace JewelryStoreContracts.ViewModels
{
    // Сообщения, приходящие на почту
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }

        [DisplayName("Отправитель")]
        public string SenderName { get; set; }

        [DisplayName("Дата письма")]
        public DateTime DateDelivery { get; set; }

        [DisplayName("Заголовок")]
        public string Subject { get; set; }

        [DisplayName("Текст")]
        public string Body { get; set; }

        [DisplayName("Прочитано")]
        public bool Checked { get; set; }

        [DisplayName("Ответ")]
        public string ReplyText { get; set; }
    }
}
