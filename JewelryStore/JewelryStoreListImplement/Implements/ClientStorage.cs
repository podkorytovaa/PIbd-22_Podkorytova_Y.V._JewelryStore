using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreListImplement.Models;
using System;
using System.Collections.Generic;

namespace JewelryStoreListImplement.Implements
{
    public class ClientStorage : IClientStorage
    {
        private readonly DataListSingleton source;

        public ClientStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<ClientViewModel> GetFullList()
        {
            var result = new List<ClientViewModel>();
            foreach (var client in source.Clients)
            {
                result.Add(CreateModel(client));
            }
            return result;
        }

        public List<ClientViewModel> GetFilteredList(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<ClientViewModel>();
            foreach (var client in source.Clients)
            {
                if (client.Login == model.Login && client.Password == model.Password)
                {
                    result.Add(CreateModel(client));
                }
            }
            return result;
        }

        public ClientViewModel GetElement(ClientBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var client in source.Clients)
            {
                if (client.Login == model.Login || client.Id == model.Id)
                {
                    return CreateModel(client);
                }
            }
            return null;
        }

        public void Insert(ClientBindingModel model)
        {
            var tempClient = new Client { Id = 1 };
            foreach (var client in source.Clients)
            {
                if (client.Id >= tempClient.Id)
                {
                    tempClient.Id = client.Id + 1;
                }
            }
            source.Clients.Add(CreateModel(model, tempClient));
        }

        public void Update(ClientBindingModel model)
        {
            Client tempClient = null;
            foreach (var client in source.Clients)
            {
                if (model.Id == client.Id)
                {
                    tempClient = client;
                }
            }
            if (tempClient == null)
            {
                throw new Exception("Клиент не найден");
            }
            CreateModel(model, tempClient);
        }

        public void Delete(ClientBindingModel model)
        {
            for (int i = 0; i < source.Clients.Count; ++i)
            {
                if (source.Clients[i].Id == model.Id.Value)
                {
                    source.Clients.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Клиент не найден");
        }

        public static Client CreateModel(ClientBindingModel model, Client client)
        {
            client.ClientFIO = model.ClientFIO;
            client.Login = model.Login;
            client.Password = model.Password;
            return client;
        }

        public ClientViewModel CreateModel(Client client)
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
