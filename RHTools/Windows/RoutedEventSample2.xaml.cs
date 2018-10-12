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
    public partial class RoutedEventSample2 : Window
    {
        public RoutedEventSample2()
        {
            InitializeComponent();
        }

        private void ReportTimeHandler(object sender, ReportTimeEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            string timeStr = e.ClickTime.ToString("HH:mm:ss") + ":" + e.ClickTime.Millisecond;
            string content = string.Format("{0}到达{1}", timeStr, element.Name);
            listbox1_1.Items.Add(content);

            //传递过程可终止
            //如果传递过程中遇到了包含"panel"的元素,则不再继续传递
            //if (element.Name.Contains("panel"))
            //    e.Handled = true;
        }

    }

    class TimeButton : Button
    {
        //1.声明并注册路由事件.
        public static readonly RoutedEvent ReportTimeEvent = EventManager.RegisterRoutedEvent("ReportTime", RoutingStrategy.Tunnel, typeof(EventHandler<ReportTimeEventArgs>), typeof(TimeButton));

        //2.为路由事件添加CLR事件包装.
        public event RoutedEventHandler ReportTime
        {
            add { this.AddHandler(ReportTimeEvent, value); }
            remove { this.RemoveHandler(ReportTimeEvent, value); }
        }

        //3.创建可以激发路由事件的方法.
        protected override void OnClick()
        {
            base.OnClick();

            ReportTimeEventArgs args = new ReportTimeEventArgs(ReportTimeEvent, this);
            args.ClickTime = DateTime.Now;
            this.RaiseEvent(args);
        }
    }

    /// <summary>
    /// 用于承载时间消息的事件参数
    /// </summary>
    class ReportTimeEventArgs : RoutedEventArgs
    {
        public ReportTimeEventArgs(RoutedEvent routedEvent, object source)
            : base(routedEvent, source) { }

        public DateTime ClickTime { get; set; }
    }

}
