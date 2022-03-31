using System;
using System.ComponentModel;
using JewelryStoreContracts.Enums;
using System.Runtime.Serialization;

namespace JewelryStoreContracts.ViewModels
{
    // Заказ
    [DataContract]
    public class OrderViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int ClientId { get; set; }

        [DataMember]
        [DisplayName("ФИО клиента")]
        public string ClientFIO { get; set; }

        [DataMember]
        public int JewelId { get; set; }

        [DataMember]
        [DisplayName("Драгоценность")]
        public string JewelName { get; set; }

        [DataMember]
        [DisplayName("Количество")]
        public int Count { get; set; }

        [DataMember]
        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DataMember]
        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DataMember]
        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DataMember]
        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
