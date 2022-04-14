using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace JewelryStoreBusinessLogic.BusinessLogics
{
    public class ImplementerLogic : IImplementerLogic
    {
        private readonly IImplementerStorage _implementerStorage;

        public ImplementerLogic(IImplementerStorage implementerStorage)
        {
            _implementerStorage = implementerStorage;
        }

        public List<ImplementerViewModel> Read(ImplementerBindingModel model)
        {
            if (model == null)
            {
                return _implementerStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<ImplementerViewModel> { _implementerStorage.GetElement(model) };
            }
            return _implementerStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(ImplementerBindingModel model)
        {
            var element = _implementerStorage.GetElement(new ImplementerBindingModel { ImplementerFIO = model.ImplementerFIO });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть исполнитель с таким ФИО");
            }
            if (model.Id.HasValue)
            {
                _implementerStorage.Update(model);
            }
            else
            {
                _implementerStorage.Insert(model);
            }
        }

        public void Delete(ImplementerBindingModel model)
        {
            var element = _implementerStorage.GetElement(new ImplementerBindingModel { Id = model.Id });
            if (element == null)
            {
                throw new Exception("Исполнитель не найден");
            }
            _implementerStorage.Delete(model);
        }
    }
}
