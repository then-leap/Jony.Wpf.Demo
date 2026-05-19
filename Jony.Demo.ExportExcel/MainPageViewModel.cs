using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;

namespace Jony.Demo.ExportExcel;

public partial class MainPageViewModel : ObservableObject
{
    [ObservableProperty]
    private List<ReportItem> reportData = new();

    [ObservableProperty]
    private string? selectedTemplatePath;

    public MainPageViewModel()
    {
        // 模拟数据
        ReportData =
        [
            new ReportItem { Id = 1, Name = "产品A", Value = 100 },
            new ReportItem { Id = 2, Name = "产品B", Value = 250 },
            new ReportItem { Id = 3, Name = "产品C", Value = 400 }
        ];
    }

    [RelayCommand]
    private void SelectTemplate()
    {
        var dlg = new OpenFileDialog
        {
            Filter = "Excel 模板 (*.xlsx)|*.xlsx",
            Title = "选择报表模板"
        };
        if (dlg.ShowDialog() == true)
        {
            SelectedTemplatePath = dlg.FileName;
        }
    }

    [RelayCommand]
    private async Task Export()
    {
        if (string.IsNullOrEmpty(SelectedTemplatePath))
        {
            MessageBox.Show("请先选择模板！");
            return;
        }

        var preview = new ExportPreviewWindow(ReportData, SelectedTemplatePath);
        preview.Show();
        await preview.LoadPreviewAsync();
    }
    
    [RelayCommand]
    private void EnterBrower()
    {
        // 假设你预览的地址（可以换成动态的 URL）
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string fileName = "test.html";
        string fullPath = Path.Combine(baseDir, fileName);
        string fileUrl = new Uri(fullPath).AbsoluteUri;

        
        // 打开一个内嵌 WebView2 的窗口
        var window = new WebViewWindow(fileUrl);
        window.Show();
    }
}