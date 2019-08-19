using RHTools.WPFStyle;
using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Windows;
using System.Windows.Controls;
using static RHTools.Win32HardwareInfos;

namespace RHTools
{
    public partial class Index : Window
    {
        public Index()
        {
            InitializeComponent();

            string win32ToolTip = "";
            foreach (HardwareEnum hardwareEnum in Enum.GetValues(typeof(HardwareEnum)))
                win32ToolTip += hardwareEnum.ToString() + "\r\n";
            Win32HardwareInfos.ToolTip = win32ToolTip.Trim();
        }

        private void TextBoxName_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            StackPanelContainer.Children.Clear();

            string name = TextBoxName.Text.Trim().ToLower();
            if (name.Length > 0)
            {
                foreach (TabItem item in TabControlContainer.Items)
                {
                    foreach (IndexButton indexButton in ((item.Content as ScrollViewer).Content as StackPanel).Children)
                    {
                        if (indexButton.Content.ToString().ToLower().Contains(name))
                            StackPanelContainer.Children.Add(new IndexButton() { Content = indexButton.Content });
                    }
                }
            }

            ScrollViewerContainer.Visibility = name.Length > 0 ? Visibility.Visible : Visibility.Hidden;
            TabControlContainer.Visibility = name.Length > 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            TextBoxName.Clear();
        }
    }
}
