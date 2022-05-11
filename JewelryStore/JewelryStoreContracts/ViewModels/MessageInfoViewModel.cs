using JewelryStoreContracts.Attributes;
using System;


namespace JewelryStoreContracts.ViewModels
{
    // Сообщения, приходящие на почту
    public class MessageInfoViewModel
    {
        public string MessageId { get; set; }

        [Column(title: "Отправитель", width: 250)]
        public string SenderName { get; set; }

        [Column(title: "Дата письма", width: 140)]
        public DateTime DateDelivery { get; set; }

        [Column(title: "Заголовок", width: 250)]
        public string Subject { get; set; }

        [Column(title: "Текст", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string Body { get; set; }
    }
}
