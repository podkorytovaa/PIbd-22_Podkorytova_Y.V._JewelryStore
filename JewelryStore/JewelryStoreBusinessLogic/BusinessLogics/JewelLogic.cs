using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace JewelryStoreBusinessLogic.BusinessLogics
{
    public class JewelLogic : IJewelLogic
    {
        private readonly IJewelStorage _jewelStorage;

        public JewelLogic(IJewelStorage jewelStorage)
        {
            _jewelStorage = jewelStorage;
        }

        public List<JewelViewModel> Read(JewelBindingModel model)
        {
            if (model == null)
            {
                return _jewelStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<JewelViewModel> { _jewelStorage.GetElement(model) };
            }
            return _jewelStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(JewelBindingModel model)
        {
            var element = _jewelStorage.GetElement(new JewelBindingModel
            {
                JewelName = model.JewelName,
                Price = model.Price,
                JewelComponents = model.JewelComponents,
            });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть драгоценность с таким названием");
            }
            if (model.Id.HasValue)
            {
                _jewelStorage.Update(model);
            }
            else
            {
                _jewelStorage.Insert(model);
            }
        }

        public void Delete(JewelBindingModel model)
        {
            var jewel = _jewelStorage.GetElement(new JewelBindingModel { Id = model.Id });
            if (jewel == null)
            {
                throw new Exception("Драгоценность не найдена");
            }
            _jewelStorage.Delete(model);
        }
    }
}
