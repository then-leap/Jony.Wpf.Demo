# Jony.Wpf.Demo

本仓库包含两个 WPF 示例项目：

- `Jony.Demo.ExportExcel`：演示导出 Excel 和 WebView2 集成的 WPF 应用
- `Jony.Demo.SideMenu`：演示带侧边菜单、主题与多页面导航的 WPF 应用

## 项目结构

- `Jony.Demo.ExportExcel/`
  - WPF 应用，用于导出数据、预览报表等功能
- `Jony.Demo.SideMenu/`
  - WPF 应用，包含侧边导航栏、页面切换和自定义样式
- `Jony.Demo.sln`
  - 解决方案文件，可同时加载两个示例项目

## 运行说明

1. 使用 Visual Studio 打开 `Jony.Demo.sln`
2. 设置要运行的启动项目：
   - `Jony.Demo.ExportExcel`
   - 或 `Jony.Demo.SideMenu`
3. 选择目标框架 `.NET 8.0` 并启动调试

## 依赖项

- .NET 8.0
- WPF
- `Microsoft.Web.WebView2`（`Jony.Demo.ExportExcel` 项目可选）

## 提示

- 若要在 `Jony.Demo.ExportExcel` 中测试 WebView2 功能，请确保本机已安装 WebView2 运行时
- 通过 Visual Studio 的 NuGet 管理器还原依赖项

---

文档由仓库初始化时生成，便于快速了解项目内容与运行方式。