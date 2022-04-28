﻿using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.Enums;
using JewelryStoreContracts.ViewModels;
using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;


namespace JewelryStoreBusinessLogic.BusinessLogics
{
    public class WorkModeling : IWorkProcess
    {
        private IOrderLogic _orderLogic;

        private readonly Random rnd;

        public WorkModeling()
        {
            rnd = new Random(1000);
        }

        // Запуск работ
        public void DoWork(IImplementerLogic implementerLogic, IOrderLogic orderLogic)
        {
            _orderLogic = orderLogic;
            var implementers = implementerLogic.Read(null);
            ConcurrentBag<OrderViewModel> orders = new(_orderLogic.Read(new OrderBindingModel { SearchStatus = OrderStatus.Принят }));
            foreach (var implementer in implementers)
            {
                Task.Run(async () => await WorkerWorkAsync(implementer, orders));
            }
        }

        // Иммитация работы исполнителя
        private async Task WorkerWorkAsync(ImplementerViewModel implementer, ConcurrentBag<OrderViewModel> orders)
        {
            // ищем заказы, которые уже в работе (вдруг исполнителя прервали)
            var runOrders = await Task.Run(() => _orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.Выполняется
            }));
            foreach (var order in runOrders)
            {
                // делаем работу заново
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                _orderLogic.FinishOrder(new ChangeStatusBindingModel { OrderId = order.Id });
                // отдыхаем
                Thread.Sleep(implementer.PauseTime);
            }

            var requiredOrders = await Task.Run(() => _orderLogic.Read(new OrderBindingModel
            {
                ImplementerId = implementer.Id,
                Status = OrderStatus.ТребуютсяМатериалы
            }));
            foreach (var order in requiredOrders)
            {
                _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel
                {
                    OrderId = order.Id,
                    ImplementerId = implementer.Id
                });
                OrderViewModel tempOrder = _orderLogic.Read(new OrderBindingModel { Id = order.Id })?[0];
                if (tempOrder.Status == OrderStatus.ТребуютсяМатериалы)
                {
                    continue;
                }
                Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                _orderLogic.FinishOrder(new ChangeStatusBindingModel
                {
                    OrderId = order.Id,
                    ImplementerId = implementer.Id
                });
                Thread.Sleep(implementer.PauseTime);
            }

            await Task.Run(() =>
            {
                while (!orders.IsEmpty)
                {
                    if (orders.TryTake(out OrderViewModel order))
                    {
                        // пытаемся назначить заказ на исполнителя
                        _orderLogic.TakeOrderInWork(new ChangeStatusBindingModel 
                        { 
                            OrderId = order.Id, 
                            ImplementerId = implementer.Id 
                        });

                        OrderViewModel tempOrder = _orderLogic.Read(new OrderBindingModel { Id = order.Id })?[0];
                        if (tempOrder.Status == OrderStatus.ТребуютсяМатериалы)
                        {
                            continue;
                        }

                        Thread.Sleep(implementer.WorkingTime * rnd.Next(1, 5) * order.Count);
                        _orderLogic.FinishOrder(new ChangeStatusBindingModel 
                        { 
                            OrderId = order.Id,
                            ImplementerId = implementer.Id
                        });
                        // отдыхаем
                        Thread.Sleep(implementer.PauseTime);
                    }
                }
            });
        }
    }
}
