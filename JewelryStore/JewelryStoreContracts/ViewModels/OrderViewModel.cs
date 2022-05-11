using System;
using JewelryStoreContracts.Attributes;
using JewelryStoreContracts.Enums;

namespace JewelryStoreContracts.ViewModels
{
    // Заказ
    public class OrderViewModel
    {
        [Column(title: "Номер", width: 60)]
        public int Id { get; set; }

        public int ClientId { get; set; }

        [Column(title: "Клиент", width: 260)]
        public string ClientFIO { get; set; }

        public int JewelId { get; set; }

        [Column(title: "Драгоценность", gridViewAutoSize: GridViewAutoSize.Fill)]
        public string JewelName { get; set; }

        public int? ImplementerId { get; set; }

        [Column(title: "Исполнитель", width: 210)]
        public string ImplementerFIO { get; set; }

        [Column(title: "Количество", width: 100)]
        public int Count { get; set; }

        [Column(title: "Сумма", width: 90)]
        public decimal Sum { get; set; }

        [Column(title: "Статус", width: 100)]
        public OrderStatus Status { get; set; }

        [Column(title: "Дата создания", width: 140)]
        public DateTime DateCreate { get; set; }

        [Column(title: "Дата выполнения", width: 140)]
        public DateTime? DateImplement { get; set; }
    }
}
