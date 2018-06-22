using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Metodos_Informes {
    public class InfomesExcel {

        public static void CrearExcel(System.Data.DataTable dt,string titulo,string tituloI) {

            Application excel = new Application();
            excel.Visible = false;
            excel.DisplayAlerts = false;

            Workbook workbook = excel.Workbooks.Add(Type.Missing);

            Worksheet worksheet = (Worksheet)workbook.ActiveSheet;
            worksheet.Name = "Informe"+titulo;
            worksheet.Range[worksheet.Cells[1,1],worksheet.Cells[1,8]].Merge();
            worksheet.Range[worksheet.Cells[2,1],worksheet.Cells[2,8]].Merge();
            worksheet.Cells[1,1] = "Hostal Doña Clarita";
            worksheet.Cells[2,1] = "Informe "+tituloI;
            worksheet.Cells[1,1].Font.Size = 14;
            worksheet.Cells[2,1].Font.Size = 14;
            worksheet.Cells[3,1] = string.Empty;

            Range cellRange;

            int rowcount = 3;

            foreach (DataRow datarow in dt.Rows) {
                rowcount += 1;
                for (int i = 1;i <= dt.Columns.Count;i++) {

                    if (rowcount == 4) {
                        worksheet.Cells[4,i] = dt.Columns[i - 1].ColumnName;
                        worksheet.Cells.Font.Color = System.Drawing.Color.Black;
                    }

                    worksheet.Cells[rowcount + 1,i] = datarow[i - 1].ToString();

                    if (rowcount > 3) {
                        if (i == dt.Columns.Count) {
                            if (rowcount % 2 == 0) {
                                cellRange = worksheet.Range[worksheet.Cells[rowcount,1],worksheet.Cells[rowcount,dt.Columns.Count]];
                            }
                        }
                    }
                }
            }

            cellRange = worksheet.Range[worksheet.Cells[4,1],worksheet.Cells[rowcount,dt.Columns.Count]];
            cellRange.EntireColumn.AutoFit();
            Borders border = cellRange.Borders;
            border.LineStyle = XlLineStyle.xlContinuous;
            border.Weight = 2d;

            cellRange = worksheet.Range[worksheet.Cells[1,1],worksheet.Cells[2,dt.Columns.Count]];

            
            if (File.Exists(@"C:\Users\shogu\Desktop\Informe_" + titulo)) {
                File.Delete(@"C:\Users\shogu\Desktop\Informe_" + titulo);
                workbook.SaveAs(@"C:\Users\shogu\Desktop\Informe_" + titulo);
            }
            else {
                workbook.SaveAs(@"C:\Users\shogu\Desktop\Informe_" + titulo);
            }

            workbook.Close();
            excel.Quit();
        }
    }
}