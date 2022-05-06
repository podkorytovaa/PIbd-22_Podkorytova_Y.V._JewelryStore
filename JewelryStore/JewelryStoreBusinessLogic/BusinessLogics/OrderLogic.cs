using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreContracts.Enums;
using System;
using System.Collections.Generic;

namespace JewelryStoreBusinessLogic.BusinessLogics
{
    public class OrderLogic : IOrderLogic
    {
        private readonly IOrderStorage _orderStorage;
        private readonly IJewelStorage _jewelStorage;
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly object locker = new object();

        public OrderLogic(IOrderStorage orderStorage, IJewelStorage jewelStorage, IWarehouseStorage warehouseStorage)
        {
            _orderStorage = orderStorage;
            _jewelStorage = jewelStorage;
            _warehouseStorage = warehouseStorage;
        }

        public List<OrderViewModel> Read(OrderBindingModel model)
        {
            if (model == null)
            {
                return _orderStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<OrderViewModel> { _orderStorage.GetElement(model) };
            }
            return _orderStorage.GetFilteredList(model);
        }

        public void CreateOrder(CreateOrderBindingModel model)
        {
            _orderStorage.Insert(new OrderBindingModel
            {
                ClientId = model.ClientId,
                JewelId = model.JewelId,
                Count = model.Count,
                Sum = model.Sum,
                Status = OrderStatus.Принят,
                DateCreate = DateTime.Now
            });
        }

        public void TakeOrderInWork(ChangeStatusBindingModel model)
        {
            lock (locker)
            {
                var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
                if (order == null)
                {
                    throw new Exception("Не найден заказ");
                }
                if (order.Status != OrderStatus.Принят && order.Status != OrderStatus.ТребуютсяМатериалы)
                {
                    throw new Exception("Заказ не в статусе \"Принят\" или \"Требуются материалы\"");
                }

                var jewel = _jewelStorage.GetElement(new JewelBindingModel { Id = order.JewelId });
                var tempOrder = new OrderBindingModel
                {
                    Id = order.Id,
                    ClientId = order.ClientId,
                    JewelId = order.JewelId,
                    ImplementerId = model.ImplementerId,
                    Count = order.Count,
                    Sum = order.Sum,
                    DateCreate = order.DateCreate
                };

                if (_warehouseStorage.CheckAndWriteOff(jewel.JewelComponents, tempOrder.Count))
                {
                    tempOrder.Status = OrderStatus.Выполняется;
                    tempOrder.DateImplement = DateTime.Now;
                    _orderStorage.Update(tempOrder);
                }
                else
                {
                    tempOrder.Status = OrderStatus.ТребуютсяМатериалы;
                    _orderStorage.Update(tempOrder);
                }
            }
        }

        public void FinishOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Выполняется)
            {
                throw new Exception("Заказ не в статусе \"Выполняется\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                JewelId = order.JewelId,
                ImplementerId = model.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Готов
            });
        }

        public void DeliveryOrder(ChangeStatusBindingModel model)
        {
            var order = _orderStorage.GetElement(new OrderBindingModel { Id = model.OrderId });
            if (order == null)
            {
                throw new Exception("Не найден заказ");
            }
            if (order.Status != OrderStatus.Готов)
            {
                throw new Exception("Заказ не в статусе \"Готов\"");
            }
            _orderStorage.Update(new OrderBindingModel
            {
                Id = order.Id,
                ClientId = order.ClientId,
                JewelId = order.JewelId,
                ImplementerId = order.ImplementerId,
                Count = order.Count,
                Sum = order.Sum,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement,
                Status = OrderStatus.Выдан
            });
        }
    }
}
