using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.ViewModels;
using JewelryStoreWarehouseApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreWarehouseApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<WarehouseViewModel>>($"api/Warehouse/GetWarehouseList"));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult Enter()
        {
            return View();
        }

        [HttpPost]
        public void Enter(string password)
        {
            if (!string.IsNullOrEmpty(password))
            {
                if (_configuration["Password"] != password)
                {
                    throw new Exception("Неверный пароль");
                }
                Program.Autorized = true;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите пароль");
        }

        [HttpGet]
        public IActionResult Create()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            return View();
        }

        [HttpPost]
        public void Create(string warehouseName, string responsibleFullName)
        {
            if (String.IsNullOrEmpty(warehouseName) || String.IsNullOrEmpty(responsibleFullName))
            {
                return;
            }
            APIClient.PostRequest("api/Warehouse/CreateOrUpdateWarehouse", new WarehouseBindingModel
            {
                WarehouseName = warehouseName,
                ResponsibleFullName = responsibleFullName,
                DateCreate = DateTime.Now,
                WarehouseComponents = new Dictionary<int, (string, int)>()
            });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Adding()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouses = APIClient.GetRequest<List<WarehouseViewModel>>("api/Warehouse/GetWarehouseList");
            ViewBag.Components = APIClient.GetRequest<List<ComponentViewModel>>("api/Warehouse/GetComponentsList");
            return View();
        }

        [HttpPost]
        public void Adding(int warehouse, int component, int count)
        {
            APIClient.PostRequest("api/Warehouse/AddComponentWarehouse", new WarehouseComponentsBindingModel
            {
                WarehouseId = warehouse,
                ComponentId = component,
                Count = count
            });
            Response.Redirect("Adding");
        }

        [HttpGet]
        public IActionResult Delete()
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            ViewBag.Warehouses = APIClient.GetRequest<List<WarehouseViewModel>>("api/Warehouse/GetWarehouseList");
            return View();
        }

        [HttpPost]
        public void Delete(int warehouse)
        {
            APIClient.PostRequest("api/Warehouse/DeleteWarehouse", new WarehouseBindingModel { Id = warehouse });
            Response.Redirect("Index");
        }

        [HttpGet]
        public IActionResult Update(int warehouseId)
        {
            if (Program.Autorized == false)
            {
                return Redirect("~/Home/Enter");
            }
            WarehouseViewModel warehouse = APIClient.GetRequest<WarehouseViewModel>($"api/Warehouse/GetWarehouse?warehouseId={warehouseId}");
            ViewBag.WarehouseName = warehouse.WarehouseName;
            ViewBag.ResponsibleFullName = warehouse.ResponsibleFullName;
            ViewBag.WarehouseComponents = warehouse.WarehouseComponents;
            return View();
        }

        [HttpPost]
        public void Update(int warehouseId, string warehouseName, string responsibleFullName)
        {
            if (String.IsNullOrEmpty(warehouseName) || String.IsNullOrEmpty(responsibleFullName))
            {
                return;
            }
            WarehouseViewModel warehouse = APIClient.GetRequest<WarehouseViewModel>($"api/Warehouse/GetWarehouse?warehouseId={warehouseId}");
            APIClient.PostRequest("api/Warehouse/CreateOrUpdateWarehouse", new WarehouseBindingModel
            {
                Id = warehouseId,
                WarehouseName = warehouseName,
                ResponsibleFullName = responsibleFullName,
                DateCreate = DateTime.Now,
                WarehouseComponents = warehouse.WarehouseComponents
            });
            Response.Redirect("Index");
        }
    }
}
