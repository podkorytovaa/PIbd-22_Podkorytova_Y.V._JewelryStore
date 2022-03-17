﻿using JewelryStoreBusinessLogic.OfficePackage;
using JewelryStoreBusinessLogic.OfficePackage.HelperModels;
using JewelryStoreContracts.BindingModels;
using JewelryStoreContracts.BusinessLogicsContracts;
using JewelryStoreContracts.StoragesContracts;
using JewelryStoreContracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JewelryStoreBusinessLogic.BusinessLogics
{
    public class ReportLogic : IReportLogic
    {
        private readonly IComponentStorage _componentStorage;
        private readonly IJewelStorage _jewelStorage;
        private readonly IOrderStorage _orderStorage;
        private readonly AbstractSaveToExcel _saveToExcel;
        private readonly AbstractSaveToWord _saveToWord;
        private readonly AbstractSaveToPdf _saveToPdf;

        public ReportLogic(IJewelStorage productStorage, IComponentStorage componentStorage, IOrderStorage orderStorage, AbstractSaveToExcel saveToExcel, AbstractSaveToWord saveToWord, AbstractSaveToPdf saveToPdf)
        {
            _jewelStorage = productStorage;
            _componentStorage = componentStorage;
            _orderStorage = orderStorage;
            _saveToExcel = saveToExcel;
            _saveToWord = saveToWord;
            _saveToPdf = saveToPdf;
        }

        // Получение списка компонент с указанием, в каких изделиях используются
        public List<ReportJewelComponentViewModel> GetJewelComponent()
        {
            var components = _componentStorage.GetFullList();
            var jewels = _jewelStorage.GetFullList();
            var list = new List<ReportJewelComponentViewModel>();
            foreach (var jewel in jewels)
            {
                var record = new ReportJewelComponentViewModel
                {
                    JewelName = jewel.JewelName,
                    Components = new List<Tuple<string, int>>(),
                    TotalCount = 0
                };
                foreach (var component in jewel.JewelComponents)
                {
                    record.Components.Add(new Tuple<string, int>(component.Value.Item1, component.Value.Item2));
                    record.TotalCount += component.Value.Item2;
                }

                list.Add(record);
            }

            return list;
        }
        
        // Получение списка заказов за определенный период
        public List<ReportOrdersViewModel> GetOrders(ReportBindingModel model)
        {
            return _orderStorage.GetFilteredList(new OrderBindingModel
            {
                DateFrom = model.DateFrom,
                DateTo = model.DateTo
            })
            .Select(x => new ReportOrdersViewModel
            {
                DateCreate = x.DateCreate,
                JewelName = x.JewelName,
                Count = x.Count,
                Sum = x.Sum,
                Status = x.Status.ToString()
            })
           .ToList();
        }
       
        // Сохранение компонент в файл-Word
        public void SaveJewelsToWordFile(ReportBindingModel model)
        {
            _saveToWord.CreateDoc(new WordInfo
            {
                FileName = model.FileName,
                Title = "Список драгоценностей",
                Jewels = _jewelStorage.GetFullList()
            });
        }
        
        // Сохранение компонент с указаеним продуктов в файл-Excel
        public void SaveJewelComponentToExcelFile(ReportBindingModel model)
        {
            _saveToExcel.CreateReport(new ExcelInfo
            {
                FileName = model.FileName,
                Title = "Список компонент",
                JewelComponents = GetJewelComponent()
            });
        }
       
        // Сохранение заказов в файл-Pdf
        public void SaveOrdersToPdfFile(ReportBindingModel model)
        {
            _saveToPdf.CreateDoc(new PdfInfo
            {
                FileName = model.FileName,
                Title = "Список заказов",
                DateFrom = model.DateFrom.Value,
                DateTo = model.DateTo.Value,
                Orders = GetOrders(model)
            });
        }
    }
}
