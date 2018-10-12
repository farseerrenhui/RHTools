using System;
using System.IO;
using System.Windows;

namespace RHTools
{
    public partial class FileInfos : Window
    {
        public FileInfos()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            foreach (String s in Directory.EnumerateFiles(TextBoxPath.Text))
            {
                TextBoxFiles.AppendText(s + "\n");
            }
        }
    }
}
