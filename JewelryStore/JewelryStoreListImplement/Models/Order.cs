﻿using JewelryStoreContracts.Enums;
using System;

namespace JewelryStoreListImplement.Models
{
    // Заказ
    public class Order
    {
        public int Id { get; set; }
        public int JewelId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
    }
}
