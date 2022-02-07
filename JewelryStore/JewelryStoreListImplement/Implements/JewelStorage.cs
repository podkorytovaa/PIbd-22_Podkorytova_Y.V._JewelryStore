﻿using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using JewelryStoreListImplement.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreListImplement.Implements
{
    public class JewelStorage : IJewelStorage
    {
        private readonly DataListSingleton source;

        public JewelStorage()
        {
            source = DataListSingleton.GetInstance();
        }

        public List<JewelViewModel> GetFullList()
        {
            var result = new List<JewelViewModel>();
            foreach (var component in source.Jewels)
            {
                result.Add(CreateModel(component));
            }
            return result;
        }

        public List<JewelViewModel> GetFilteredList(JewelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            var result = new List<JewelViewModel>();
            foreach (var jewel in source.Jewels)
            {
                if (jewel.JewelName.Contains(model.JewelName))
                {
                    result.Add(CreateModel(jewel));
                }
            }
            return result;
        }

        public JewelViewModel GetElement(JewelBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            foreach (var jewel in source.Jewels)
            {
                if (jewel.Id == model.Id || jewel.JewelName == model.JewelName)
                {
                    return CreateModel(jewel);
                }
            }
            return null;
        }

        public void Insert(JewelBindingModel model)
        {
            var tempJewel = new Jewel { Id = 1, JewelComponents = new Dictionary<int, int>() };
            foreach (var jewel in source.Jewels)
            {
                if (jewel.Id >= tempJewel.Id)
                {
                    tempJewel.Id = jewel.Id + 1;
                }
            }
            source.Jewels.Add(CreateModel(model, tempJewel));
        }

        public void Update(JewelBindingModel model)
        {
            Jewel tempJewel = null;
            foreach (var jewel in source.Jewels)
            {
                if (jewel.Id == model.Id)
                {
                    tempJewel = jewel;
                }
            }
            if (tempJewel == null)
            {
                throw new Exception("Элемент не найден");
            }
            CreateModel(model, tempJewel);
        }

        public void Delete(JewelBindingModel model)
        {
            for (int i = 0; i < source.Jewels.Count; ++i)
            {
                if (source.Jewels[i].Id == model.Id)
                {
                    source.Jewels.RemoveAt(i);
                    return;
                }
            }
            throw new Exception("Элемент не найден");
        }

        private static Jewel CreateModel(JewelBindingModel model, Jewel jewel)
        {
            jewel.JewelName = model.JewelName;
            jewel.Price = model.Price;

            // удаляем убранные
            foreach (var key in jewel.JewelComponents.Keys.ToList())
            {
                if (!model.JewelComponents.ContainsKey(key))
                {
                    jewel.JewelComponents.Remove(key);
                }
            }

            // обновляем существуюущие и добавляем новые
            foreach (var component in model.JewelComponents)
            {
                if (jewel.JewelComponents.ContainsKey(component.Key))
                {
                    jewel.JewelComponents[component.Key] = model.JewelComponents[component.Key].Item2;
                }
                else
                {
                    jewel.JewelComponents.Add(component.Key, model.JewelComponents[component.Key].Item2);
                }
            }
            return jewel;
        }

        private JewelViewModel CreateModel(Jewel jewel)
        {
            // требуется дополнительно получить список компонентов для изделия с названиями и их количество
            var jewelComponents = new Dictionary<int, (string, int)>();
            foreach (var jc in jewel.JewelComponents)
            {
                string componentName = string.Empty;
                foreach (var component in source.Components)
                {
                    if (jc.Key == component.Id)
                    {
                        componentName = component.ComponentName;
                        break;
                    }
                }
                jewelComponents.Add(jc.Key, (componentName, jc.Value));
            }
            return new JewelViewModel
            {
                Id = jewel.Id,
                JewelName = jewel.JewelName,
                Price = jewel.Price,
                JewelComponents = jewelComponents
            };
        }
    }
}
