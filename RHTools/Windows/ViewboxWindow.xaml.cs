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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RHTools
{
    public partial class ViewboxWindow : Window
    {
        public ViewboxWindow()
        {
            InitializeComponent();
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DoubleAnimation animationWidth = new DoubleAnimation() {
                From = 0,
                To = 320,
                Duration = new Duration(TimeSpan.Parse("0:0:2")),
            };
            DoubleAnimation animationHeight = new DoubleAnimation()
            {
                From = 0,
                To = 240,
                Duration = new Duration(TimeSpan.Parse("0:0:2")),
            };

            Viewbox1.BeginAnimation(Viewbox.WidthProperty, animationWidth);
            Viewbox1.BeginAnimation(Viewbox.HeightProperty, animationHeight);
        }

        private void Button_Click_Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
}
