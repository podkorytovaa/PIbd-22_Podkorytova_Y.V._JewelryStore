using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreRestApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseLogic _warehouse;
        private readonly IComponentLogic _component;

        public WarehouseController(IWarehouseLogic warehouse, IComponentLogic component)
        {
            _warehouse = warehouse;
            _component = component;
        }

        [HttpGet]
        public List<WarehouseViewModel> GetWarehouseList() => _warehouse.Read(null)?.ToList();

        [HttpGet]
        public WarehouseViewModel GetWarehouse(int warehouseId) => _warehouse.Read(new WarehouseBindingModel { Id = warehouseId })?[0];

        [HttpGet]
        public List<ComponentViewModel> GetComponentsList() => _component.Read(null)?.ToList();

        [HttpPost]
        public void CreateOrUpdateWarehouse(WarehouseBindingModel model) => _warehouse.CreateOrUpdate(model);
        
        [HttpPost]
        public void DeleteWarehouse(WarehouseBindingModel model) => _warehouse.Delete(model);
        
        [HttpPost]
        public void AddComponentWarehouse(WarehouseComponentsBindingModel model) => _warehouse.AddComponent(new WarehouseBindingModel { Id = model.WarehouseId }, model.ComponentId, model.Count);
    }
}
