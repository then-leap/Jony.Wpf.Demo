using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Jony.Demo.SideMenu.Models;

public partial class MenuBarModel : ObservableObject
{
    [ObservableProperty]
    private string _title;

    [ObservableProperty]
    private string _icon;

    [ObservableProperty]
    private string _route;

    [ObservableProperty]
    private bool _isSelected;

    [ObservableProperty]
    private bool _isExpanded;

    [ObservableProperty]
    private ObservableCollection<MenuBarModel> _childMenuBarModel;
}

