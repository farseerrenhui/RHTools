using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class WindowBubbleGrid : Window
    {
        public WindowBubbleGrid()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name");
            dt.Columns.Add("value");
            Random r = new Random();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    dt.Rows.Add(new string[] { "bubble_" + i + "_" + j, ((r.Next()) % 2).ToString() });
                }
            }

            BubbleGrid1.UpdateDataSource(dt);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            BubbleGrid1.HorizontalLineCount++;
        }

        private void Button_Click5(object sender, RoutedEventArgs e)
        {
            BubbleGrid1.VerticalLineCount++;
        }

        private void Button_Click_X(object sender, RoutedEventArgs e)
        {
            BubbleGrid1.XLabelPosition = BubbleGrid1.XLabelPosition == _VerticalLabelPosition.上 ? _VerticalLabelPosition.下 : _VerticalLabelPosition.上;
        }

        private void Button_Click_Y(object sender, RoutedEventArgs e)
        {
            BubbleGrid1.YLabelPosition = BubbleGrid1.YLabelPosition == _HorizontalLabelPosition.左 ? _HorizontalLabelPosition.右 : _HorizontalLabelPosition.左;
        }
    }
}
