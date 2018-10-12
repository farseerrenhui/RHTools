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
    public partial class PopupBoxWindow : Window
    {

        public PopupBoxWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_PopupBox(object sender, RoutedEventArgs e)
        {
            PopupBox popupBox = new PopupBox();
            popupBox.ShowDialog();
        }
    }
}
