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

namespace RHTools
{
    public partial class PopupBox : Window
    {
        int WIDTH = System.Windows.Forms.SystemInformation.WorkingArea.Width;
        int HEIGHT = System.Windows.Forms.SystemInformation.WorkingArea.Height;
        int nowHeight = System.Windows.Forms.SystemInformation.WorkingArea.Height;

        int speed = 5;

        DispatcherTimer timerUp = new DispatcherTimer();
        DispatcherTimer timerDown = new DispatcherTimer();

        public PopupBox()
        {
            InitializeComponent();

            Point p = new Point(WIDTH - this.Width, HEIGHT);
            this.Left = p.X;
            this.Top = p.Y;

            timerUp.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timerUp.Tick += timerUp_Tick;
            timerUp.Start();

            timerDown.Interval = new TimeSpan(0, 0, 0, 0, 1);
            timerDown.Tick += timerDown_Tick;
        }

        private void timerUp_Tick(object sender, EventArgs e)
        {
            if (nowHeight > HEIGHT - this.Height)
            {
                Point p = new Point(WIDTH - this.Width, nowHeight);//注：其高度""都是自上向下增加的！
                this.Left = p.X;
                this.Top = p.Y;
                nowHeight -= speed;
            }
            else
            {
                timerUp.Stop();
            }
        }

        private void timerDown_Tick(object sender, EventArgs e)
        {
            #region 下降

            if (nowHeight <= HEIGHT)
            {
                Point p = new Point(WIDTH - this.Width, nowHeight);
                this.Left = p.X;
                this.Top = p.Y;
                nowHeight += speed;
            }
            else
            {
                CanClose = true;
                this.Close();//自动关闭[弹出窗口]
            }

            #endregion

            #region 透明消失

            if (this.Opacity != 0)
            {
                this.Opacity -= 0.01;
            }
            else
            {
                this.Close();
            }

            #endregion
        }

        public void Disappear()
        {
            timerDown.Start();
        }

        bool CanClose = false;

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !CanClose;
            timerDown.Start();
        }
    }
}
