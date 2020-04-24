using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;

namespace GivosCalc
{
    class ExcelHandler
    {
        public void SaveAndOpenExcel(string path,string outputPath, List<Item> _itemsOnSecondTab)
        {
            Application excel = new Application();                      
            Workbook wb = excel.Workbooks.Open(System.AppDomain.CurrentDomain.BaseDirectory + path);
            wb.SaveAs(outputPath);
            wb.Close(0);


            excel.WindowState = XlWindowState.xlMaximized; //maximize Excel
            excel.WindowState = XlWindowState.xlMaximized; //maximize the workbook in Excel
            excel.Visible = true;
            Workbook workingWb = excel.Workbooks.Open(outputPath);


            Worksheet sheet = workingWb.Worksheets["Sheet1"];
            Range colRange = sheet.Range["A1:A60"];

            WriteValuesInExcel(excel, sheet, colRange, _itemsOnSecondTab);

        }

        public void WriteValuesInExcel(Application excel, Worksheet sheet, Range range, List<Item> _itemsOnSecondTab)
        {
            List<Range> listOfCells = range.Cells.Cast<Range>().ToList<Range>();

            ExcelNames names = new ExcelNames(_itemsOnSecondTab);
            var dictionary = names.dictCalc();
            foreach(KeyValuePair<string,float> entry in dictionary)
            {
                for(int i = 0; i < 60; i++)
                { if (listOfCells[i].Value2 != null)
                    {
                        if (entry.Key == listOfCells[i].Value2)
                        {
                            var cell = sheet.Cells[listOfCells[i].Row, listOfCells[i].Column + 2];
                            cell.Value2 = entry.Value;
                        }
                    }
                }
            }

          
        }
    }

    
}
