﻿using System;
using System.ComponentModel;
using JewelryStoreContracts.Enums;

namespace JewelryStoreContracts.ViewModels
{
    // Заказ
    public class OrderViewModel
    {
        public int Id { get; set; }

        public int JewelId { get; set; }

        [DisplayName("Драгоценность")]
        public string JewelName { get; set; }

        [DisplayName("Количество")]
        public int Count { get; set; }

        [DisplayName("Сумма")]
        public decimal Sum { get; set; }

        [DisplayName("Статус")]
        public OrderStatus Status { get; set; }

        [DisplayName("Дата создания")]
        public DateTime DateCreate { get; set; }

        [DisplayName("Дата выполнения")]
        public DateTime? DateImplement { get; set; }
    }
}
