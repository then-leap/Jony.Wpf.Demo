using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Jony.Demo.SideMenu.Themes
{
    internal class SkinHelper
    {
        public enum Skin { Default, Dark, Violet }   // 想加几套就加几套

        public static void ChangeTo(Skin skin)
        {
            // 1. 清空旧颜色
            Application.Current.Resources.MergedDictionaries.Clear();

            // 2. 加载新颜色
            var colorDict = new ResourceDictionary();
            colorDict.Source = new Uri($"/HandyControl;component/Themes/Skin{skin}.xaml",
                                       UriKind.RelativeOrAbsolute);

            // 3. 再把样式字典加回来（顺序不能反）
            var themeDict = new ResourceDictionary();
            themeDict.Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml");

            Application.Current.Resources.MergedDictionaries.Add(colorDict);
            Application.Current.Resources.MergedDictionaries.Add(themeDict);
        }
    }
}
