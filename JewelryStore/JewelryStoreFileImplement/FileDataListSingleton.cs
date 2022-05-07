using JewelryStoreContracts.Enums;
using JewelryStoreFileImplement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace JewelryStoreFileImplement
{
    public class FileDataListSingleton
    {
        private static FileDataListSingleton instance;
        private readonly string ComponentFileName = "Component.xml";
        private readonly string OrderFileName = "Order.xml";
        private readonly string JewelFileName = "Jewel.xml";
        private readonly string WarehouseFileName = "Warehouse.xml";
        private readonly string ClientFileName = "Client.xml";
        private readonly string ImplementerFileName = "Implementer.xml";
        private readonly string MessageInfoFileName = "MessageInfo.xml";
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Jewel> Jewels { get; set; }
        public List<Warehouse> Warehouses { get; set; }
        public List<Client> Clients { get; set; }
        public List<Implementer> Implementers { get; set; }
        public List<MessageInfo> MessagesInfo { get; set; }

        private FileDataListSingleton()
        {
            Components = LoadComponents();
            Orders = LoadOrders();
            Jewels = LoadJewels();
            Warehouses = LoadWarehouses();
            Clients = LoadClients();
            Implementers = LoadImplementers();
            MessagesInfo = LoadMessagesInfo();
        }

        public static FileDataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new FileDataListSingleton();
            }
            return instance;
        }

        public static void Save()
        {
            instance.SaveComponents();
            instance.SaveOrders();
            instance.SaveJewels();
            instance.SaveWarehouses();
            instance.SaveClients();
            instance.SaveImplementers();
            instance.SaveMessagesInfo();
        }

        private List<Component> LoadComponents()
        {
            var list = new List<Component>();
            if (File.Exists(ComponentFileName))
            {
                var xDocument = XDocument.Load(ComponentFileName);
                var xElements = xDocument.Root.Elements("Component").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Component
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ComponentName = elem.Element("ComponentName").Value
                    });
                }
            }
            return list;
        }

        private List<Order> LoadOrders()
        {
            var list = new List<Order>();
            if (File.Exists(OrderFileName))
            {
                var xDocument = XDocument.Load(OrderFileName);
                var xElements = xDocument.Root.Elements("Order").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Order
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientId = Convert.ToInt32(elem.Element("ClientId").Value),
                        JewelId = Convert.ToInt32(elem.Element("JewelId").Value),
                        ImplementerId = Convert.ToInt32(elem.Element("ImplementerId").Value),
                        Count = Convert.ToInt32(elem.Element("Count").Value),
                        Sum = Convert.ToDecimal(elem.Element("Sum").Value),
                        Status = (OrderStatus)Enum.Parse(typeof(OrderStatus), elem.Element("Status").Value),
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        DateImplement = string.IsNullOrEmpty(elem.Element("DateImplement").Value) ? (DateTime?)null : Convert.ToDateTime(elem.Element("DateImplement").Value)
                    });
                }
            }
            return list;
        }

        private List<Jewel> LoadJewels()
        {
            var list = new List<Jewel>();
            if (File.Exists(JewelFileName))
            {
                var xDocument = XDocument.Load(JewelFileName);
                var xElements = xDocument.Root.Elements("Jewel").ToList();
                foreach (var elem in xElements)
                {
                    var jewComp = new Dictionary<int, int>();
                    foreach (var component in elem.Element("JewelComponents").Elements("JewelComponent").ToList())
                    {
                        jewComp.Add(Convert.ToInt32(component.Element("Key").Value), Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Jewel
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        JewelName = elem.Element("JewelName").Value,
                        Price = Convert.ToDecimal(elem.Element("Price").Value),
                        JewelComponents = jewComp
                    });
                }
            }
            return list;
        }

        private List<Warehouse> LoadWarehouses()
        {
            var list = new List<Warehouse>();
            if (File.Exists(WarehouseFileName))
            {
                var xDocument = XDocument.Load(WarehouseFileName);
                var xElements = xDocument.Root.Elements("Warehouse").ToList();
                foreach (var elem in xElements)
                {
                    var warehouseComp = new Dictionary<int, int>();
                    foreach (var component in elem.Element("WarehouseComponents").Elements("WarehouseComponent").ToList())
                    {
                        warehouseComp.Add(Convert.ToInt32(component.Element("Key").Value), Convert.ToInt32(component.Element("Value").Value));
                    }
                    list.Add(new Warehouse
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        WarehouseName = elem.Element("WarehouseName").Value,
                        ResponsibleFullName = elem.Element("ResponsibleFullName").Value,
                        DateCreate = Convert.ToDateTime(elem.Element("DateCreate").Value),
                        WarehouseComponents = warehouseComp
                    });
                }
            }
            return list;
        }

        private List<Client> LoadClients()
        {
            var list = new List<Client>();
            if (File.Exists(ClientFileName))
            {
                var xDocument = XDocument.Load(ClientFileName);
                var xElements = xDocument.Root.Elements("Client").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Client
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ClientFIO = elem.Element("ClientFIO").Value,
                        Login = elem.Element("Login").Value,
                        Password = elem.Element("Password").Value
                    });
                }
            }
            return list;
        }

        private List<Implementer> LoadImplementers()
        {
            var list = new List<Implementer>();
            if (File.Exists(ImplementerFileName))
            {
                XDocument xDocument = XDocument.Load(ImplementerFileName);
                var xElements = xDocument.Root.Elements("Implementer").ToList();
                foreach (var elem in xElements)
                {
                    list.Add(new Implementer
                    {
                        Id = Convert.ToInt32(elem.Attribute("Id").Value),
                        ImplementerFIO = elem.Element("ImplementerFIO").Value,
                        WorkingTime = Convert.ToInt32(elem.Attribute("WorkingTime").Value),
                        PauseTime = Convert.ToInt32(elem.Attribute("PauseTime").Value),
                    });
                }
            }
            return list;
        }

        private List<MessageInfo> LoadMessagesInfo()
        {
            var list = new List<MessageInfo>();
            if (File.Exists(MessageInfoFileName))
            {
                var xDocument = XDocument.Load(MessageInfoFileName);
                var xElements = xDocument.Root.Elements("Message").ToList();
                int? clientId;
                foreach (var elem in xElements)
                {
                    clientId = null;
                    if (elem.Element("ClientId").Value != "")
                    {
                        clientId = Convert.ToInt32(elem.Element("ClientId").Value);
                    }
                    list.Add(new MessageInfo
                    {
                        MessageId = elem.Attribute("MessageId").Value,
                        ClientId = clientId,
                        SenderName = elem.Element("SenderName").Value,
                        DateDelivery = DateTime.Parse(elem.Element("DateDelivery").Value),
                        Subject = elem.Element("Subject").Value,
                        Body = elem.Element("Body").Value
                    });
                }
            }
            return list;
        }

        private void SaveComponents()
        {
            if (Components != null)
            {
                var xElement = new XElement("Components");
                foreach (var component in Components)
                {
                    xElement.Add(new XElement("Component",
                        new XAttribute("Id", component.Id),
                        new XElement("ComponentName", component.ComponentName)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ComponentFileName);
            }
        }

        private void SaveOrders()
        {
            if (Orders != null)
            {
                var xElement = new XElement("Orders");
                foreach (var order in Orders)
                {
                    xElement.Add(new XElement("Order",
                        new XAttribute("Id", order.Id),
                        new XElement("ClientId", order.ClientId),
                        new XElement("JewelId", order.JewelId),
                        new XElement("ImplementerId", order.ImplementerId),
                        new XElement("Count", order.Count),
                        new XElement("Sum", order.Sum),
                        new XElement("Status", order.Status),
                        new XElement("DateCreate", order.DateCreate),
                        new XElement("DateImplement", order.DateImplement)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }

        private void SaveJewels()
        {
            if (Jewels != null)
            {
                var xElement = new XElement("Jewels");
                foreach (var jewel in Jewels)
                {
                    var compElement = new XElement("JewelComponents");
                    foreach (var component in jewel.JewelComponents)
                    {
                        compElement.Add(new XElement("JewelComponent",
                            new XElement("Key", component.Key),
                            new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Jewel",
                        new XAttribute("Id", jewel.Id),
                        new XElement("JewelName", jewel.JewelName),
                        new XElement("Price", jewel.Price),
                        compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(JewelFileName);
            }
        }

        private void SaveWarehouses()
        {
            if (Warehouses != null)
            {
                var xElement = new XElement("Warehouses");
                foreach (var warehouse in Warehouses)
                {
                    var compElement = new XElement("WarehouseComponents");
                    foreach (var component in warehouse.WarehouseComponents)
                    {
                        compElement.Add(new XElement("WarehouseComponent",
                            new XElement("Key", component.Key),
                            new XElement("Value", component.Value)));
                    }
                    xElement.Add(new XElement("Warehouse",
                        new XAttribute("Id", warehouse.Id),
                        new XElement("WarehouseName", warehouse.WarehouseName),
                        new XElement("ResponsibleFullName", warehouse.ResponsibleFullName),
                        new XElement("DateCreate", warehouse.DateCreate),
                        compElement));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(WarehouseFileName);
            }
        }

        private void SaveClients()
        {
            if (Clients != null)
            {
                var xElement = new XElement("Clients");
                foreach (var client in Clients)
                {
                    xElement.Add(new XElement("Client",
                        new XAttribute("Id", client.Id),
                        new XElement("ClientFIO", client.ClientFIO),
                        new XElement("Login", client.Login),
                        new XElement("Password", client.Password)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(ClientFileName);
            }
        }

        private void SaveImplementers()
        {
            if (Implementers != null)
            {
                var xElement = new XElement("Implementers");
                foreach (var implementer in Implementers)
                {
                    xElement.Add(new XElement("Implementer",
                    new XAttribute("Id", implementer.Id),
                    new XElement("ImplementerFIO", implementer.ImplementerFIO),
                    new XElement("WorkingTime", implementer.WorkingTime),
                    new XElement("PauseTime", implementer.PauseTime)));
                }
                XDocument xDocument = new XDocument(xElement);
                xDocument.Save(ImplementerFileName);
            }
        }

        private void SaveMessagesInfo()
        {
            if (MessagesInfo != null)
            {
                var xElement = new XElement("MessagesInfo");
                foreach (var messageInfo in MessagesInfo)
                {
                    xElement.Add(new XElement("MessageInfo",
                        new XAttribute("MessageId", messageInfo.MessageId),
                        new XElement("ClientId", messageInfo.ClientId),
                        new XElement("SenderName", messageInfo.SenderName),
                        new XElement("Subject", messageInfo.Subject),
                        new XElement("Body", messageInfo.Body),
                        new XElement("DateDelivery", messageInfo.DateDelivery)));
                }
                var xDocument = new XDocument(xElement);
                xDocument.Save(OrderFileName);
            }
        }
    }
}
