using System.Windows;

namespace Jony.Demo.ExportExcel;

public partial class WebViewWindow : Window
{
    private readonly string _url;

    public WebViewWindow(string url)
    {
        InitializeComponent();
        _url = url;
        Loaded += WebViewWindow_Loaded;
    }

    private async void WebViewWindow_Loaded(object sender, RoutedEventArgs e)
    {
        try
        {
            await InitWebViewAsync();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"加载网页失败: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async Task InitWebViewAsync()
    {
        await Browser.EnsureCoreWebView2Async();
        Browser.CoreWebView2.Settings.AreDevToolsEnabled = true;
        Browser.CoreWebView2.Settings.AreDefaultScriptDialogsEnabled = true;
        Browser.CoreWebView2.Settings.IsZoomControlEnabled = true;
        Browser.CoreWebView2.Navigate(_url);
    }
}