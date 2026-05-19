using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Jony.Demo.SideMenu.Messages;
using Jony.Demo.SideMenu.Models;
using Jony.Demo.SideMenu.Views;
using HandyControl.Themes;
using Jony.Demo.SideMenu.Themes;

namespace Jony.Demo.SideMenu.ViewModels;

public partial class MainViewModel : ObservableObject
{
    [ObservableProperty]
    private string _currentUserName = "管理员";

    [ObservableProperty]
    private object _currentView;

    [ObservableProperty]
    private ObservableCollection<MenuBarModel> _menuBarList;

    public MainViewModel()
    {
        MenuBarList =
        [
            new MenuBarModel { Icon = "\uE80F", Title = "仪表盘", Route = "Dashboard", IsSelected = true},
            new MenuBarModel { Icon = "\uE716", Title = "用户管理", Route = "Users" },
            new MenuBarModel { Icon = "\uE9D9", Title = "数据分析", Route = "Analytics" },
            new MenuBarModel { Icon = "\uE8A5", Title = "报表管理", Route = "Reports" },
            new MenuBarModel
            {
                Icon = "\uE713", Title = "系统设置", Route = "Settings", IsExpanded = false,
                ChildMenuBarModel =
                [
                    new MenuBarModel { Icon = "\uE790", Title = "基本设置", Route = "BasicSettings" },
                    new MenuBarModel { Icon = "\uE72E", Title = "权限管理", Route = "Permissions" },
                    new MenuBarModel { Icon = "\uE81C", Title = "日志管理", Route = "Logs" }
                ]
            },
            new MenuBarModel
            {
                Icon = "\uE713", Title = "系统设置2", Route = "Settings", IsExpanded = false,
                ChildMenuBarModel =
                [
                    new MenuBarModel { Icon = "\uE790", Title = "基本设置", Route = "BasicSettings2" },
                    new MenuBarModel { Icon = "\uE72E", Title = "权限管理", Route = "Permissions2" },
                    new MenuBarModel { Icon = "\uE81C", Title = "日志管理", Route = "Logs2" }
                ]
            }
        ];
        LoadPage("Dashboard");


        WeakReferenceMessenger.Default.Register<NavigateMessage>(this, (r, m) =>
        {
            Navigate(m.Value);
            SetSelectedMenu(m.Value);
        });

    }


    [RelayCommand]
    private void Home(object parameter)
    {
        CurrentView = new TextBlock { Text = "首页内容", FontSize = 20 };
    }

    [RelayCommand]
    private void Personal(object parameter)
    {
        CurrentView = new TextBlock { Text = "个人中心", FontSize = 20 };
    }

    [RelayCommand]
    private void Logout(object parameter)
    {
        System.Windows.MessageBox.Show("退出登录");
    }

    [RelayCommand]
    private void MenuExpanderExpanded()
    {
        // 这里可以做展开收起逻辑
    }

    [RelayCommand]
    private void Navigate(string route)
    {
        LoadPage(route);
        SetSelectedMenu(route);
    }


    [RelayCommand]
    private void FullScreen()
    {

    }    
    
    
    [RelayCommand]
    private void SwitchTheme()
    {
        SkinHelper.ChangeTo(SkinHelper.Skin.Dark);   // 一秒切暗黑
    }


    private void LoadPage(string? pageTag)
    {
        UserControl page = pageTag switch
        {
            "Dashboard" => new DashboardPage(),
            "Users" => new UsersPage(),
            "Analytics" => new AnalyticsPage(),
            "Reports" => new ReportsPage(),
            "Settings" => new SettingsPage(),
            "BasicSettings" => new BasicSettingsPage(),
            "Permissions" => new PermissionsPage(),
            "Logs" => new LogsPage(),
            "BasicSettings2" => new BasicSettingsPage(),
            "Permissions2" => new PermissionsPage(),
            "Logs2" => new LogsPage(),
            _ => new DashboardPage()
        };

        CurrentView = page;
    }

    private void SetSelectedMenu(string route)
    {
        foreach (var menu in MenuBarList)
        {
            menu.IsSelected = false;
            menu.IsExpanded = false; // 先收起所有父菜单
            if (menu.ChildMenuBarModel == null || menu.ChildMenuBarModel.Count == 0) continue;
            foreach (var child in menu.ChildMenuBarModel)
            {
                child.IsSelected = false;
            }
        }

        foreach (var menu in MenuBarList)
        {
            if (menu.Route == route)
            {
                menu.IsSelected = true;
                return;
            }
            if (menu.ChildMenuBarModel == null || menu.ChildMenuBarModel.Count == 0) continue;
            var target = menu.ChildMenuBarModel.FirstOrDefault(c => c.Route == route);
            if (target == null) continue;
            target.IsSelected = true;
            menu.IsExpanded = true;   // 🔥 展开父菜单
            return;
        }
    }
}