using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// <summary>
    /// RegexTool.xaml 的交互逻辑
    /// </summary>
    public partial class RegexTool : Window
    {
        public RegexTool()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBox.Show(Regex.IsMatch(TextBoxString.Text.Trim(), TextBoxRegex.Text.Trim()).ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("语法错误");
            }
        }

        private void Button_Click_Help(object sender, RoutedEventArgs e)
        {
            string fileRegexHtml = Environment.CurrentDirectory + "\\resources\\regex.html";
            Process.Start(fileRegexHtml);
        }
    }
}
