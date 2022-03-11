using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreDatabaseImplement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreDatabaseImplement.Implements
{
    public class OrderStorage : IOrderStorage
    {
        public List<OrderViewModel> GetFullList()
        {
            using var context = new JewelryStoreDatabase();
            return context.Orders
                .Include(rec => rec.Jewel)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<OrderViewModel> GetFilteredList(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new JewelryStoreDatabase();
            return context.Orders
                .Include(rec => rec.Jewel)
                .Where(rec => rec.JewelId == model.JewelId)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public OrderViewModel GetElement(OrderBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new JewelryStoreDatabase();
            var order = context.Orders.Include(rec => rec.Jewel).FirstOrDefault(rec => rec.Id == model.Id);
            return order != null ? CreateModel(order) : null;
        }

        public void Insert(OrderBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            context.Orders.Add(CreateModel(model, new Order()));
            context.SaveChanges();
        }

        public void Update(OrderBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            var element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, element);
            context.SaveChanges();
        }

        public void Delete(OrderBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            Order element = context.Orders.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Orders.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Order CreateModel(OrderBindingModel model, Order order)
        {
            order.JewelId = model.JewelId;
            order.Count = model.Count;
            order.Sum = model.Sum;
            order.Status = model.Status;
            order.DateCreate = model.DateCreate;
            order.DateImplement = model.DateImplement;
            return order;
        }

        private OrderViewModel CreateModel(Order order)
        {
            return new OrderViewModel
            {
                Id = order.Id,
                JewelId = order.JewelId,
                JewelName = order.Jewel.JewelName,
                Count = order.Count,
                Sum = order.Sum,
                Status = order.Status,
                DateCreate = order.DateCreate,
                DateImplement = order.DateImplement
            };
        }
    }
}
