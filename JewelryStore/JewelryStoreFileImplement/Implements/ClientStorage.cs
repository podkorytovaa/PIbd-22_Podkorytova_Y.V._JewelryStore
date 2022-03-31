using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreFileImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreFileImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly FileDataListSingleton source;

        public ClientStorage()
        {
            source = FileDataListSingleton.GetInstance();
        }

        public List<ClientViewModel> GetFullList()
        {
            return source.Clients.Select(CreateModel).ToList();
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return source.Clients.Where(rec => rec.Login == model.Login && rec.Password == model.Password).Select(CreateModel).ToList();
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var client = source.Clients.FirstOrDefault(rec => rec.Login == model.Login || rec.Id == model.Id);
            return client != null ? CreateModel(client) : null;
        }

        public void Insert(ClientBindingModel model)
        {
            int maxId = source.Clients.Count > 0 ? source.Clients.Max(rec => rec.Id) : 0;
            var client = new Client { Id = maxId + 1 };
            source.Clients.Add(CreateModel(model, client));
        }

        public void Update(ClientBindingModel model)
        {
            var client = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (client == null)
            {
                throw new Exception("Клиент не найден");
            }
            CreateModel(model, client);
        }

        public void Delete(ClientBindingModel model)
        {
            Client client = source.Clients.FirstOrDefault(rec => rec.Id == model.Id);
            if (client != null)
            {
                source.Clients.Remove(client);
            }
            else
            {
                throw new Exception("Клиент не найден");
            }
        }

        private static Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFIO = model.ClientFIO;
            client.Login = model.Login;
            client.Password = model.Password;
            return client;
        }

        private ClientViewModel CreateModel(Client client)
        {
            return new ClientViewModel
            {
                Id = client.Id,
                ClientFIO = client.ClientFIO,
                Login = client.Login,
                Password = client.Password
            };
        }
    }
}
