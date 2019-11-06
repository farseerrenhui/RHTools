using System;
using System.Collections.Generic;
using System.IO;
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
    public partial class FileNameBatch : Window
    {

        public FileNameBatch()
        {
            InitializeComponent();
        }

        private void ListBoxFiles_Drop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            for (int i = 0; i < s.Length; i++)
                (sender as ListBox).Items.Add(s[i]);
        }

        private void ButtonReplace_Click(object sender, RoutedEventArgs e)
        {
            int count = 0;
            foreach (String item in ListBoxFiles.Items)
            {
                FileInfo fi = new FileInfo(item);
                if (item.Contains(TextBoxBefore.Text))
                {
                    fi.MoveTo(item.Replace(TextBoxBefore.Text, TextBoxAfter.Text));
                    count++;
                }
            }
            System.Windows.Forms.MessageBox.Show(count + " Files Renamed.");
            ListBoxFiles.Items.Clear();
        }
    }
}
