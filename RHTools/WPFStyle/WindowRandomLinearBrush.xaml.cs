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
using System.Windows.Threading;

namespace RHTools.WPFStyle
{
    public partial class WindowRandomLinearBrush : Window
    {
        private DispatcherTimer _timer;

        public WindowRandomLinearBrush()
        {
            InitializeComponent();

            _timer = new DispatcherTimer(DispatcherPriority.Send, this.Dispatcher);
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _timer.Tick += Change;
            _timer.Start();
        }

        private void Change(object sender, EventArgs e)
        {
            //_LinearGradientBrush.GradientStops.Clear();

            //Random random = new Random();

            //for (int i = 0; i < 500; i++)
            //{
            //    _LinearGradientBrush.GradientStops.Add(
            //    new GradientStop(
            //        new Color()
            //        {
            //            A = 255,
            //            R = byte.Parse(random.Next(0, 256).ToString()),
            //            G = byte.Parse(random.Next(0, 256).ToString()),
            //            B = byte.Parse(random.Next(0, 256).ToString()),
            //        }, random.Next(0, 10000) / 10000d));
            //}


            _RadialGradientBrush.GradientStops.Clear();
            Random random = new Random();
            for (int i = 0; i < 500; i++)
            {
                _RadialGradientBrush.GradientStops.Add(
                new GradientStop(
                    new Color()
                    {
                        A = 255,
                        R = byte.Parse(random.Next(0, 256).ToString()),
                        G = byte.Parse(random.Next(0, 256).ToString()),
                        B = byte.Parse(random.Next(0, 256).ToString()),
                    }, random.Next(0, 10000) / 10000d));
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
    }
}
