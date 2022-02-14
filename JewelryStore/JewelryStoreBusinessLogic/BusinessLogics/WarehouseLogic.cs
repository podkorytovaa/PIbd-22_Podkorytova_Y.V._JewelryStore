﻿using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using System;
using System.Collections.Generic;

namespace JewelryStoreBusinessLogic.BusinessLogics
{
    public class WarehouseLogic : IWarehouseLogic
    {
        private readonly IWarehouseStorage _warehouseStorage;
        private readonly IComponentStorage _componentStorage;

        public WarehouseLogic(IWarehouseStorage warehouseStorage, IComponentStorage componentStorage)
        {
            _warehouseStorage = warehouseStorage;
            _componentStorage = componentStorage;
        }

        public List<WarehouseViewModel> Read(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return _warehouseStorage.GetFullList();
            }
            if (model.Id.HasValue)
            {
                return new List<WarehouseViewModel> { _warehouseStorage.GetElement(model) };
            }
            return _warehouseStorage.GetFilteredList(model);
        }

        public void CreateOrUpdate(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel { WarehouseName = model.WarehouseName });
            if (element != null && element.Id != model.Id)
            {
                throw new Exception("Уже есть склад с таким названием");
            }
            if (model.Id.HasValue)
            {
                _warehouseStorage.Update(model);
            }
            else
            {
                _warehouseStorage.Insert(model);
            }
        }

        public void Delete(WarehouseBindingModel model)
        {
            var element = _warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.Id });

            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }
            _warehouseStorage.Delete(model);
        }
        
        public void AddComponent(WarehouseBindingModel model, int componentId, int count)
        {
            var warehouse = _warehouseStorage.GetElement(new WarehouseBindingModel { Id = model.Id });

            var component = _componentStorage.GetElement(new ComponentBindingModel { Id = componentId });

            if (warehouse == null)
            {
                throw new Exception("Склад не найден");
            }

            if (component == null)
            {
                throw new Exception("Компонент не найден");
            }

            if (warehouse.WarehouseComponents.ContainsKey(componentId))
            {
                int oldCount = warehouse.WarehouseComponents[componentId].Item2;
                warehouse.WarehouseComponents[componentId] = (component.ComponentName, oldCount + count);
            }
            else
            {
                warehouse.WarehouseComponents.Add(componentId, (component.ComponentName, count));
            }

            _warehouseStorage.Update(new WarehouseBindingModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsibleFullName = warehouse.ResponsibleFullName,
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouse.WarehouseComponents
            });
        }
    }
}
