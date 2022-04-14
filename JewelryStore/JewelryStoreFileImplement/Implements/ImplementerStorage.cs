using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreFileImplement.Implements
{
    public class ImplementerStorage : IImplementerStorage
    {
        private readonly FileDataListSingleton source;

        public ImplementerStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<ImplementerViewModel> GetFullList()
        {
            return source.Implementers.Select(CreateModel).ToList();
        }

        public List<ImplementerViewModel> GetFilteredList(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Implementers.Where(rec => rec.ImplementerFIO == model.ImplementerFIO).Select(CreateModel).ToList();
        }

        public ImplementerViewModel GetElement(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var implementer = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            return implementer != null ? CreateModel(implementer) : null;
        }

        public void Insert(ImplementerBindingModel model)
        {
            int maxId = source.Implementers.Count > 0 ? source.Implementers.Max(rec => rec.Id) : 0;
            var implementer = new Implementer { Id = maxId + 1 };
            source.Implementers.Add(CreateModel(model, implementer));
        }

        public void Update(ImplementerBindingModel model)
        {
            var implementer = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (implementer == null)
            {
                throw new Exception("Исполнитель не найден");
            }
            CreateModel(model, implementer);
        }
        
        public void Delete(ImplementerBindingModel model)
        {
            Implementer implementer = source.Implementers.FirstOrDefault(rec => rec.Id == model.Id);
            if (implementer != null)
            {
                source.Implementers.Remove(implementer);
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
