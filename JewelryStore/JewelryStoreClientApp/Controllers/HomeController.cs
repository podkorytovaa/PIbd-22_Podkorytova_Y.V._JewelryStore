using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.ViewModels;
using JewelryStoreClientApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace JewelryStoreClientApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return
            View(APIClient.GetRequest<List<OrderViewModel>>($"api/main/getorders?clientId={Program.Client.Id}"));
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(Program.Client);
        }

        [HttpPost]
        public void Privacy(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(fio))
            {
                APIClient.PostRequest("api/client/updatedata", new ClientBindingModel
                {
                    Id = Program.Client.Id,
                    ClientFIO = fio,
                    Login = login,
                    Password = password
                });
                Program.Client.ClientFIO = fio;
                Program.Client.Login = login;
                Program.Client.Password = password;
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
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
        public void Enter(string login, string password)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password))
            {
                Program.Client = APIClient.GetRequest<ClientViewModel>($"api/client/login?login={login}&password={password}");
                if (Program.Client == null)
                {
                    throw new Exception("Неверный логин/пароль");
                }
                Response.Redirect("Index");
                return;
            }
            throw new Exception("Введите логин, пароль");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public void Register(string login, string password, string fio)
        {
            if (!string.IsNullOrEmpty(login) && !string.IsNullOrEmpty(password) && !string.IsNullOrEmpty(fio))
            {
                APIClient.PostRequest("api/client/register", new ClientBindingModel
                {
                    ClientFIO = fio,
                    Login = login,
                    Password = password
                });
                Response.Redirect("Enter");
                return;
            }
            throw new Exception("Введите логин, пароль и ФИО");
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Jewels = APIClient.GetRequest<List<JewelViewModel>>("api/main/getjewellist");
            return View();
        }

        [HttpPost]
        public void Create(int jewel, int count, decimal sum)
        {
            if (count == 0 || sum == 0)
            {
                return;
            }
            // запрос
            APIClient.PostRequest("api/main/createorder", new CreateOrderBindingModel
            {
                ClientId = (int)Program.Client.Id,
                JewelId = jewel,
                Count = count,
                Sum = sum
            });
            Response.Redirect("Index");
        }

        [HttpPost]
        public decimal Calc(decimal count, int jewel)
        {
            JewelViewModel jew = APIClient.GetRequest<JewelViewModel>($"api/main/getjewel?jewelId={jewel}");
            return count * jew.Price;
        }

        [HttpGet]
        public IActionResult MessagesInfo()
        {
            if (Program.Client == null)
            {
                return Redirect("~/Home/Enter");
            }
            return View(APIClient.GetRequest<List<MessageInfoViewModel>> ($"api/client/GetMessagesInfo?clientId={Program.Client.Id}"));
        }
    }
}
