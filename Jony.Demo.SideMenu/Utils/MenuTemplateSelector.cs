using System.Windows;
using System.Windows.Controls;
using Jony.Demo.SideMenu.Models;

namespace Jony.Demo.SideMenu.Utils;

public class MenuTemplateSelector : DataTemplateSelector
{
    public DataTemplate ParentMenuTemplate { get; set; }  // Expander模板
    public DataTemplate LeafMenuTemplate { get; set; }    // RadioButton模板

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
        if (item is not MenuBarModel model) return base.SelectTemplate(item, container);
        if (model.ChildMenuBarModel != null && model.ChildMenuBarModel.Count > 0)
            return ParentMenuTemplate; // 有子菜单
        else
            return LeafMenuTemplate;   // 没有子菜单
    }
}