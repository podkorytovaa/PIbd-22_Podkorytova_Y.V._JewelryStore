﻿namespace JewelryStoreContracts.BindingModels
{
    // Данные для смены статуса заказа
    public class ChangeStatusBindingModel
    {
        public int OrderId { get; set; }

        public int? ImplementerId { get; set; }
    }
}
