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
    public class JewelStorage : IJewelStorage
    {
        public List<JewelViewModel> GetFullList()
        {
            using var context = new JewelryStoreDatabase();
            return context.Jewels
                .Include(rec => rec.JewelComponents)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<JewelViewModel> GetFilteredList(JewelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new JewelryStoreDatabase();
            return context.Jewels
                .Include(rec => rec.JewelComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.JewelName.Contains(model.JewelName))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public JewelViewModel GetElement(JewelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new JewelryStoreDatabase();
            var jewel = context.Jewels
                .Include(rec => rec.JewelComponents)
                .ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.JewelName == model.JewelName || rec.Id == model.Id);
            return jewel != null ? CreateModel(jewel) : null;
        }

        public void Insert(JewelBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Jewel jewel = new Jewel()
                {
                    JewelName = model.JewelName,
                    Price = model.Price
                };
                context.Jewels.Add(jewel); 
                context.SaveChanges();
                CreateModel(model, jewel, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(JewelBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Jewels.FirstOrDefault(rec => rec.Id == model.Id);
                if (element == null)
                {
                    throw new Exception("Элемент не найден");
                }
                CreateModel(model, element, context);
                context.SaveChanges();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Delete(JewelBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            Jewel element = context.Jewels.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Jewels.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private static Jewel CreateModel(JewelBindingModel model, Jewel jewel, JewelryStoreDatabase context)
        {
            jewel.JewelName = model.JewelName;
            jewel.Price = model.Price;

            if (model.Id.HasValue)
            {
                var jewelComponents = context.JewelComponents.Where(rec => rec.JewelId == model.Id.Value).ToList();
                // удалили те, которых нет в модели
                context.JewelComponents.RemoveRange(jewelComponents.Where(rec => !model.JewelComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                // обновили количество у существующих записей
                foreach (var updateComponent in jewelComponents)
                {
                    updateComponent.Count = model.JewelComponents[updateComponent.ComponentId].Item2;
                    model.JewelComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }
            // добавили новые
            foreach (var jc in model.JewelComponents)
            {
                context.JewelComponents.Add(new JewelComponent
                {
                    JewelId = jewel.Id,
                    ComponentId = jc.Key,
                    Count = jc.Value.Item2
                });
                context.SaveChanges();
            }
            return jewel;
        }

        private static JewelViewModel CreateModel(Jewel jewel)
        {
            return new JewelViewModel
            {
                Id = jewel.Id,
                JewelName = jewel.JewelName,
                Price = jewel.Price,
                JewelComponents = jewel.JewelComponents.ToDictionary(recJC => recJC.ComponentId, recJC => (recJC.Component?.ComponentName, recJC.Count))
            };
        }
    }
}
