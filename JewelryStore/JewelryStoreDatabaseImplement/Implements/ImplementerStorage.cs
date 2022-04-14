using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreDatabaseImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreDatabaseImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        public List<ImplementerViewModel> GetFullList()
        {
            using var context = new JewelryStoreDatabase();
            return context.Implementers
                .Select(CreateModel)
                .ToList();
        }
        
        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new JewelryStoreDatabase();
            return context.Implementers
                .Where(rec => rec.ImplementerFIO == model.ImplementerFIO)
                .Select(CreateModel)
                .ToList();
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }

            using var context = new JewelryStoreDatabase();
            var implementer = context.Implementers.FirstOrDefault(rec => rec.ImplementerFIO == model.ImplementerFIO || rec.Id == model.Id);
            return implementer != null ? CreateModel(implementer) : null;
        }

        public void Insert(ImplementerBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            context.Implementers.Add(CreateModel(model, new Implementer()));
            context.SaveChanges();
        }

        public void Update(ImplementerBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            var implementer = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (implementer == null)
            {
                throw new Exception("Исполнитель не найден");
            }
            CreateModel(model, implementer);
            context.SaveChanges();
        }

        public void Delete(ImplementerBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            Implementer implementer = context.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (implementer != null)
            {
                context.Implementers.Remove(implementer);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Исполнитель не найден");
            }
        }

        private static Implementer CreateModel(ImplementerBindingModel model, Implementer implementer)
        {
            implementer.ImplementerFIO = model.ImplementerFIO;
            implementer.WorkingTime = model.WorkingTime;
            implementer.PauseTime = model.PauseTime;
            return implementer;
        }

        private static ImplementerViewModel CreateModel(Implementer implementer)
        {
            return new ImplementerViewModel
            {
                Id = implementer.Id,
                ImplementerFIO = implementer.ImplementerFIO,
                WorkingTime = implementer.WorkingTime,
                PauseTime = implementer.PauseTime
            };
        }
    }
}
