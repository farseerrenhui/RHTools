using RHTools.WPFStyle;
using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

            TextBoxName.Focus();
        }

        private void TextBoxName_TextChanged(object sender, TextChangedEventArgs e)
        {
            StackPanelContainer.Children.Clear();

            string name = TextBoxName.Text.Trim().ToLower();
            if (name.Length > 0)
            {
                try
                {
                    foreach (TabItem item in TabControlContainer.Items)
                    {
                        foreach (IndexButton indexButton in ((item.Content as ScrollViewer).Content as StackPanel).Children)
                        {
                            String buttonName = indexButton.Content.ToString().ToLower();
                            if (Regex.IsMatch(buttonName, name))
                                StackPanelContainer.Children.Add(new IndexButton() { Content = indexButton.Content });
                        }
                    }
                }
                catch (Exception) { }
            }

            ScrollViewerContainer.Visibility = name.Length > 0 ? Visibility.Visible : Visibility.Hidden;
            TabControlContainer.Visibility = name.Length > 0 ? Visibility.Hidden : Visibility.Visible;
        }

        private void Button_Click_Clear(object sender, RoutedEventArgs e)
        {
            TextBoxName.Clear();
        }

        private void TextBoxName_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int count = StackPanelContainer.Children.Count;
            if (count >= 1 && e.Key == Key.Down)
            {
                StackPanelContainer.Children[0].Focus();
                e.Handled = true;
            }
        }

        private void Window_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            int count = StackPanelContainer.Children.Count;

            if (e.Key == Key.Down && count > 0 && StackPanelContainer.Children[count - 1].IsFocused)
            {
                StackPanelContainer.Children[0].Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Up && count > 0 && StackPanelContainer.Children[0].IsFocused)
            {
                StackPanelContainer.Children[count - 1].Focus();
                e.Handled = true;
            }
            else if (e.Key == Key.Escape)
            {
                TextBoxName.Clear();
            }
            else if (e.Key == Key.Enter)
            {
                if (count == 1)
                {
                    IndexButton indexButton = StackPanelContainer.Children[0] as IndexButton;
                    indexButton.IndexButtonClick(indexButton, null);
                }
            }
            else if (e.Key != Key.Up && e.Key != Key.Down)
            {
                TextBoxName.Focus();
            }
        }
    }
}
