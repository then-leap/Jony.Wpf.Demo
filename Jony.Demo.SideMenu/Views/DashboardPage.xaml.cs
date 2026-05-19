using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Jony.Demo.SideMenu.Messages;

namespace Jony.Demo.SideMenu.Views;

public partial class DashboardPage : UserControl
{
    public DashboardPage()
    {
        InitializeComponent();
    }

    private void JumpToLog(object sender, RoutedEventArgs e)
    {
        WeakReferenceMessenger.Default.Send(new NavigateMessage("Logs"));
    }
}