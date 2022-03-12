using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreFileImplement.Implements
{
    public class WarehouseStorage : IWarehouseStorage
    {
        private readonly FileDataListSingleton source;

        public WarehouseStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<WarehouseViewModel> GetFullList()
        {
            return source.Warehouses.Select(CreateModel).ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Warehouses.Where(recW => recW.WarehouseName.Contains(model.WarehouseName)).Select(CreateModel).ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var warehouse = source.Warehouses.FirstOrDefault(recW => recW.WarehouseName == model.WarehouseName || recW.Id == model.Id);
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            int maxId = source.Warehouses.Count > 0 ? source.Warehouses.Max(rec => rec.Id) : 0;
            var element = new Warehouse { Id = maxId + 1, WarehouseComponents = new Dictionary<int, int>(), DateCreate = DateTime.Now };
            source.Warehouses.Add(CreateModel(model, element));
        }

        public void Update(WarehouseBindingModel model)
        {
            var element = source.Warehouses.FirstOrDefault(recW => recW.Id == model.Id);
            if (element == null)
            {
                throw new Exception("Элемент не найден");
            }

            CreateModel(model, element);
        }

        public void Delete(WarehouseBindingModel model)
        {
            Warehouse element = source.Warehouses.FirstOrDefault(recW => recW.Id == model.Id);
            if (element != null)
            {
                source.Warehouses.Remove(element);
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsibleFullName = model.ResponsibleFullName;

            foreach (var key in warehouse.WarehouseComponents.Keys.ToList())
            {
                if (!model.WarehouseComponents.ContainsKey(key))
                {
                    warehouse.WarehouseComponents.Remove(key);
                }
            }

            foreach (var component in model.WarehouseComponents)
            {
                if (warehouse.WarehouseComponents.ContainsKey(component.Key))
                {
                    warehouse.WarehouseComponents[component.Key] = model.WarehouseComponents[component.Key].Item2;
                }
                else
                {
                    warehouse.WarehouseComponents.Add(component.Key, model.WarehouseComponents[component.Key].Item2);
                }
            }
            return warehouse;
        }

        private WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsibleFullName = warehouse.ResponsibleFullName,
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouse.WarehouseComponents.ToDictionary(recWC => recWC.Key, recWC => (source.Components.FirstOrDefault(recC => recC.Id == recWC.Key)?.ComponentName, recWC.Value))
            };
        }

        public bool CheckAndWriteOff(Dictionary<int, (string, int)> warehouseComponents, int jewelsCount)
        {
            foreach (var warehouseComponent in warehouseComponents)
            {
                int count = source.Warehouses.Where(component => component.WarehouseComponents.ContainsKey(warehouseComponent.Key)).Sum(component => component.WarehouseComponents[warehouseComponent.Key]);

                if (count < warehouseComponent.Value.Item2 * jewelsCount)
                {
                    return false;
                }
            }

            foreach (var warehouseComponent in warehouseComponents)
            {
                int count = warehouseComponent.Value.Item2 * jewelsCount;
               
                IEnumerable<Warehouse> warehouses = source.Warehouses.Where(component => component.WarehouseComponents.ContainsKey(warehouseComponent.Key));

                foreach (Warehouse warehouse in warehouses)
                {
                    if (warehouse.WarehouseComponents[warehouseComponent.Key] <= count)
                    {
                        count -= warehouse.WarehouseComponents[warehouseComponent.Key];
                        warehouse.WarehouseComponents.Remove(warehouseComponent.Key);
                    }
                    else
                    {
                        warehouse.WarehouseComponents[warehouseComponent.Key] -= count;
                        count = 0;
                    }

                    if (count == 0)
                    {
                        break;
                    }
                }
            }
            return true;
        }
    }
}
