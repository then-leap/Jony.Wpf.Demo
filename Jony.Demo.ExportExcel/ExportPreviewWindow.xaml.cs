using System.IO;
using System.Windows;
using GemBox.Spreadsheet;
using Microsoft.Win32;

namespace Jony.Demo.ExportExcel;

public partial class ExportPreviewWindow : Window
{
    private readonly string _excelTemplatePath;
    private readonly List<ReportItem> _reportData;
    private string _tempFile;

    public ExportPreviewWindow(List<ReportItem> reportData, string excelTemplatePath)
    {
        InitializeComponent();

        _reportData = reportData;
        _excelTemplatePath = excelTemplatePath;
        SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
    }

    // ✅ 异步加载报表与图片
    public async Task LoadPreviewAsync()
    {
        // 1️⃣ 创建临时文件副本（基于模板）
        _tempFile = Path.Combine(Path.GetTempPath(), $"ReportPreview_{Guid.NewGuid():N}.xlsx");
        File.Copy(_excelTemplatePath, _tempFile, true);

        // 2️⃣ 载入工作簿
        var workbook = ExcelFile.Load(_tempFile);
        var sheet = workbook.Worksheets[0];


        // 获取使用区域（更可靠）
        var usedRange = sheet.GetUsedCellRange(); // 返回 CellRange（包含 First/Last row/col）
        var firstRowIndex = usedRange.FirstRowIndex;
        var lastRowIndex = usedRange.LastRowIndex;
        var firstColIndex = usedRange.FirstColumnIndex;
        var lastColIndex = usedRange.LastColumnIndex;

        // 如果没有数据，fallback
        if (lastColIndex < firstColIndex)
        {
            firstColIndex = 0;
            lastColIndex = 0;
        }

        // 计算表格像素宽度（累计每列像素宽）
        double totalWidthPx = 0;
        for (var c = firstColIndex; c <= lastColIndex; c++)
            // GetWidth 返回指定单位的宽度 (LengthUnit.Pixel / Point / etc.)
            // 注意：如果列没有显式设置宽度，GetWidth 也能返回默认计算宽度
            totalWidthPx += sheet.Columns[c].GetWidth(LengthUnit.Pixel);

        // 额外左右留白（保证不会紧贴边缘）
        const int extraMargin = 200;
        var targetWidth = (int)Math.Ceiling(totalWidthPx + extraMargin);

        // 限制最大宽度，防止生成超大图片耗尽内存（可根据需求调整）
        const int maxAllowedWidth = 10000;
        if (targetWidth > maxAllowedWidth)
            targetWidth = maxAllowedWidth;

        // 同理可计算高度（这里用固定高度或根据行高度估算）
        double totalHeightPx = 0;
        for (var r = firstRowIndex; r <= lastRowIndex; r++) totalHeightPx += sheet.Rows[r].GetHeight(LengthUnit.Pixel);

        var targetHeight = (int)Math.Ceiling(totalHeightPx + 200);
        if (targetHeight < 600) targetHeight = 600; // 最小高度

        // 关闭自动缩放分页（否则会触发“压缩列导致字体变形”）
        sheet.PrintOptions.FitWorksheetWidthToPages = 0;
        sheet.PrintOptions.FitWorksheetHeightToPages = 0;

        // 然后转换为图片，使用动态计算的宽高
        var imageOptions = new ImageSaveOptions(ImageSaveFormat.Png)
        {
            PageNumber = 0,
            CropToContent = false, // 保留整页
            Width = targetWidth,
            Height = targetHeight
        };

        var img = workbook.ConvertToImageSource(imageOptions);

        // Freeze 以便跨线程使用
        if (img is Freezable f && !f.IsFrozen)
            f.Freeze();

        // 回到 UI 线程赋值（示例在 await Task.Run 外部做）
        PreviewImage.Source = img;
    }


    private void Export_Click(object sender, RoutedEventArgs e)
    {
        var saveDialog = new SaveFileDialog
        {
            Filter = "Excel 文件 (*.xlsx)|*.xlsx",
            FileName = $"Report_{DateTime.Now:yyyyMMddHHmmss}.xlsx"
        };

        if (saveDialog.ShowDialog() == true)
        {
            File.Copy(_tempFile, saveDialog.FileName, true);
            MessageBox.Show("导出成功！", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }


    private void Close(object sender, RoutedEventArgs e)
    {
        Close();
    }
}