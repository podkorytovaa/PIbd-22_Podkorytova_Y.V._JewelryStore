using JewelryStoreBusinessLogic.OfficePackage.HelperEnums;
using JewelryStoreBusinessLogic.OfficePackage.HelperModels;

namespace JewelryStoreBusinessLogic.OfficePackage
{
    public abstract class AbstractSaveToExcel
    {
        // Создание отчета
        public void CreateReport(ExcelInfo info)
        {
            CreateExcel(info);

            InsertCellInWorksheet(new ExcelCellParameters
            {
                ColumnName = "A",
                RowIndex = 1,
                Text = info.Title,
                StyleInfo = ExcelStyleInfoType.Title
            });

            MergeCells(new ExcelMergeParameters
            {
                CellFromName = "A1",
                CellToName = "C1"
            });

            uint rowIndex = 2;
            foreach (var jc in info.JewelComponents)
            {
                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "A",
                    RowIndex = rowIndex,
                    Text = jc.JewelName,
                    StyleInfo = ExcelStyleInfoType.Text
                });
                rowIndex++;

                foreach (var jewel in jc.Components)
                {
                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "B",
                        RowIndex = rowIndex,
                        Text = jewel.Item1,
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });

                    InsertCellInWorksheet(new ExcelCellParameters
                    {
                        ColumnName = "C",
                        RowIndex = rowIndex,
                        Text = jewel.Item2.ToString(),
                        StyleInfo = ExcelStyleInfoType.TextWithBroder
                    });

                    rowIndex++;
                }

                InsertCellInWorksheet(new ExcelCellParameters
                {
                    ColumnName = "C",
                    RowIndex = rowIndex,
                    Text = jc.TotalCount.ToString(),
                    StyleInfo = ExcelStyleInfoType.Text
                });

                rowIndex++;
            }

            SaveExcel(info);
        }
        
        // Создание excel-файла
        protected abstract void CreateExcel(ExcelInfo info);
        
        // Добавляем новую ячейку в лист
        protected abstract void InsertCellInWorksheet(ExcelCellParameters  excelParams);
       
        // Объединение ячеек
        protected abstract void MergeCells(ExcelMergeParameters excelParams);
        
        // Сохранение файла
        protected abstract void SaveExcel(ExcelInfo info);
    }
}
