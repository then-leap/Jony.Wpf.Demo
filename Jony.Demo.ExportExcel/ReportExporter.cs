using ClosedXML.Excel;
using System.Collections.Generic;
using System.IO;
using Jony.Demo.ExportExcel;

public class ReportExporter
{
    public MemoryStream FillTemplate(string templatePath, IEnumerable<ReportItem> dataList)
    {
        using var workbook = new XLWorkbook(templatePath);
        var ws = workbook.Worksheet(1);

        // 替换占位符
        ws.Cell("B2").Value = DateTime.Now.ToString("yyyy-MM-dd");

        int startRow = 5;
        foreach (var item in dataList)
        {
            ws.Cell(startRow, 1).Value = item.Id;
            ws.Cell(startRow, 2).Value = item.Name;
            ws.Cell(startRow, 3).Value = item.Value;
            startRow++;
        }

        var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;
        return stream;
    }
}