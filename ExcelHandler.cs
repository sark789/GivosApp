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

            //z montažo in brez montaže sheet
            Worksheet sheet1 = workingWb.Worksheets["Z montažo"];
            Worksheet sheet2 = workingWb.Worksheets["Brez montaže"];
            Range colRange1 = sheet1.Range["A1:A60"];
            Range colRange2 = sheet2.Range["A1:A60"];
            WriteValuesInExcel(excel, sheet1, colRange1,sheet2, colRange2, _itemsOnSecondTab);

            workingWb.Save();
        }

        public void WriteValuesInExcel(Application excel, Worksheet sheet, Range range, Worksheet sheet2, Range range2, List<Item> _itemsOnSecondTab)
        {
            List<Range> listOfCells = range.Cells.Cast<Range>().ToList<Range>();
            List<Range> listOfCells2 = range2.Cells.Cast<Range>().ToList<Range>();

            ExcelNames names = new ExcelNames(_itemsOnSecondTab);
            var dictionary = names.dictCalc();
            foreach(KeyValuePair<string,dynamic> entry in dictionary)
            {
                for(int i = 0; i < 60; i++)
                { if (listOfCells[i].Value2 != null)
                    {
                        if (entry.Key == listOfCells[i].Value2)
                        {                          
                            var cell = sheet.Cells[listOfCells[i].Row, listOfCells[i].Column + 2];
                            if (entry.Key == "STRANKA :")
                            {
                                cell = sheet.Cells[listOfCells[i].Row, listOfCells[i].Column + 1];
                            }
                            cell.Value2 = entry.Value;
                        }
                    }
                }
            }

            var dictionary2 = names.dictCalc2();
            foreach (KeyValuePair<string, dynamic> entry in dictionary2)
            {
                for (int i = 0; i < 60; i++)
                {
                    if (listOfCells2[i].Value2 != null)
                    {
                        if (entry.Key == listOfCells2[i].Value2)
                        {
                            var cell = sheet2.Cells[listOfCells2[i].Row, listOfCells2[i].Column + 2];
                            if (entry.Key == "STRANKA :")
                            {
                                cell = sheet2.Cells[listOfCells2[i].Row, listOfCells2[i].Column + 1];
                            }
                            if (entry.Key == "LETVICA :")
                            {
                                cell = sheet2.Cells[listOfCells2[i].Row, listOfCells2[i].Column + 1];
                            }
                            cell.Value2 = entry.Value;
                        }
                    }
                }
            }


        }
    }

    
}
