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
    public class MainController : ControllerBase
    {
        private readonly IOrderLogic _order;

        private readonly IJewelLogic _jewel;

        public MainController(IOrderLogic order, IJewelLogic jewel)
        {
            _order = order;
            _jewel = jewel;
        }

        [HttpGet]
        public List<JewelViewModel> GetJewelList() => _jewel.Read(null)?.ToList();

        [HttpGet]
        public JewelViewModel GetJewel(int jewelId) => _jewel.Read(new JewelBindingModel { Id = jewelId })?[0];

        [HttpGet]
        public List<OrderViewModel> GetOrders(int clientId) => _order.Read(new OrderBindingModel { ClientId = clientId });

        [HttpPost]
        public void CreateOrder(CreateOrderBindingModel model) => _order.CreateOrder(model);
    }
}
