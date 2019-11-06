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
    public partial class ReplaceTool : Window
    {
        public ReplaceTool()
        {
            InitializeComponent();
        }


        private void SelectAll(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
                (sender as TextBox).SelectAll();
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }


        private void Add_Click(object sender, RoutedEventArgs e)
        {
            ListBoxRules.Items.Add(new ReplaceToolRuleControl());
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxRules.SelectedItem != null)
                ListBoxRules.Items.Remove(ListBoxRules.SelectedItem);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ListBoxRules.Items.Clear();
        }


        private void Replace_Click(object sender, RoutedEventArgs e)
        {
            string result = TextBoxSrc.Text;
            foreach (ReplaceToolRuleControl rule in ListBoxRules.Items)
                result = result.Replace(rule.From, rule.To);
            TextBoxDst.Text = result;
        }
    }
}
