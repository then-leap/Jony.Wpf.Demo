using System.Configuration;
using System.Data;
using System.Windows;
using System.Windows.Markup;
using CommunityToolkit.Mvvm.DependencyInjection;
using Jony.Demo.SideMenu.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Jony.Demo.SideMenu;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public App()
    {
        Ioc.Default.ConfigureServices(
            new ServiceCollection()
                .AddSingleton<MainViewModel>()
                .BuildServiceProvider()
        );

        InitializeComponent();
    }
}