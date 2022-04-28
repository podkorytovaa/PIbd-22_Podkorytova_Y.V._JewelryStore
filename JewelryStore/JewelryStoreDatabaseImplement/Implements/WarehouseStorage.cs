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
    public class WarehouseStorage : IWarehouseStorage
    {
        public List<WarehouseViewModel> GetFullList()
        {
            using var context = new JewelryStoreDatabase();
            return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public List<WarehouseViewModel> GetFilteredList(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new JewelryStoreDatabase();
            return context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .Where(rec => rec.WarehouseName.Contains(model.WarehouseName))
                .ToList()
                .Select(CreateModel)
                .ToList();
        }

        public WarehouseViewModel GetElement(WarehouseBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            using var context = new JewelryStoreDatabase();
            var warehouse = context.Warehouses
                .Include(rec => rec.WarehouseComponents)
                .ThenInclude(rec => rec.Component)
                .FirstOrDefault(rec => rec.WarehouseName == model.WarehouseName || rec.Id == model.Id); 
            return warehouse != null ? CreateModel(warehouse) : null;
        }

        public void Insert(WarehouseBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                Warehouse warehouse = new Warehouse()
                {
                    WarehouseName = model.WarehouseName,
                    ResponsibleFullName = model.ResponsibleFullName,
                    DateCreate = DateTime.Now
                };
                context.Warehouses.Add(warehouse);
                context.SaveChanges();
                CreateModel(model, warehouse, context);
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public void Update(WarehouseBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                var element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
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

        public void Delete(WarehouseBindingModel model)
        {
            using var context = new JewelryStoreDatabase();
            Warehouse element = context.Warehouses.FirstOrDefault(rec => rec.Id == model.Id);
            if (element != null)
            {
                context.Warehouses.Remove(element);
                context.SaveChanges();
            }
            else
            {
                throw new Exception("Элемент не найден");
            }
        }

        private Warehouse CreateModel(WarehouseBindingModel model, Warehouse warehouse, JewelryStoreDatabase context)
        {
            warehouse.WarehouseName = model.WarehouseName;
            warehouse.ResponsibleFullName = model.ResponsibleFullName;

            if (model.Id.HasValue)
            {
                var warehouseComponents = context.WarehouseComponents.Where(rec => rec.WarehouseId == model.Id.Value).ToList();
               
                context.WarehouseComponents.RemoveRange(warehouseComponents.Where(rec => !model.WarehouseComponents.ContainsKey(rec.ComponentId)).ToList());
                context.SaveChanges();
                
                foreach (var updateComponent in warehouseComponents)
                {
                    updateComponent.Count = model.WarehouseComponents[updateComponent.ComponentId].Item2;
                    model.WarehouseComponents.Remove(updateComponent.ComponentId);
                }
                context.SaveChanges();
            }

            foreach (var wc in model.WarehouseComponents)
            {
                context.WarehouseComponents.Add(new WarehouseComponent
                {
                    WarehouseId = warehouse.Id,
                    ComponentId = wc.Key,
                    Count = wc.Value.Item2,
                });
                context.SaveChanges();
            }
            return warehouse;
        }

        private static WarehouseViewModel CreateModel(Warehouse warehouse)
        {
            return new WarehouseViewModel
            {
                Id = warehouse.Id,
                WarehouseName = warehouse.WarehouseName,
                ResponsibleFullName = warehouse.ResponsibleFullName,
                DateCreate = warehouse.DateCreate,
                WarehouseComponents = warehouse.WarehouseComponents.ToDictionary(rec => rec.ComponentId, rec => (rec.Component?.ComponentName, rec.Count))
            };
        }

        public bool CheckAndWriteOff(Dictionary<int, (string, int)> warehouseComponents, int jewelsCount)
        {
            using var context = new JewelryStoreDatabase();
            using var transaction = context.Database.BeginTransaction();
            try
            {
                foreach (var warehouseComponent in warehouseComponents)
                {
                    int requiredCompCount = warehouseComponent.Value.Item2 * jewelsCount;

                    var warehouses = context.WarehouseComponents.Where(warehouse => warehouse.ComponentId == warehouseComponent.Key);

                    foreach (WarehouseComponent component in warehouses)
                    {
                        if (component.Count <= requiredCompCount)
                        {
                            requiredCompCount -= component.Count;
                            context.WarehouseComponents.Remove(component);
                        }
                        else
                        {
                            component.Count -= requiredCompCount;
                            requiredCompCount = 0;
                            break;
                        }
                    }
                    if (requiredCompCount != 0)
                    {
                        throw new Exception("На складе недостаточно компонент");
                    }
                }
                context.SaveChanges();
                transaction.Commit();
                return true;
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }
}
