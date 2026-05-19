using System.Windows;
using Jony.Demo.SideMenu.ViewModels;

namespace Jony.Demo.SideMenu;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}