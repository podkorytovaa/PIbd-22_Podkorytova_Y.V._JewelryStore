﻿using JewelryStoreListImplement.Models;
using System.Collections.Generic;

namespace JewelryStoreListImplement
{
    public class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Component> Components { get; set; }
        public List<Order> Orders { get; set; }
        public List<Jewel> Jewels { get; set; }
        public List<Warehouse> Warehouses { get; set; }

        private DataListSingleton()
        {
            Components = new List<Component>();
            Orders = new List<Order>();
            Jewels = new List<Jewel>();
            Warehouses = new List<Warehouse>();
        }

        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
