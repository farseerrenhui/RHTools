using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RHTools
{
    public partial class StaticDynamicResource : Window
    {
        public StaticDynamicResource()
        {
            InitializeComponent();
        }

        private void Button_Click_ChangeResource(object sender, RoutedEventArgs e)
        {
            this.Resources["res1"] = new TextBlock() { Text = "更改后的TextBlock资源", Foreground = new SolidColorBrush(Colors.Blue) };
            this.Resources["res2"] = new TextBlock() { Text = "更改后的TextBlock资源", Foreground = new SolidColorBrush(Colors.Blue) };
        }
    }
}
