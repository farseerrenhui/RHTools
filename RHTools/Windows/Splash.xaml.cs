using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace RHTools
{
    public partial class Splash : Window
    {
        private string content = "";

        delegate void ThreadDelegate();
        ThreadDelegate contentDelegate;
        ThreadDelegate closeDelegate;

        public Splash()
        {
            InitializeComponent();

            contentDelegate = delegate ()
            {
                ChangeContent(content);
            };
            closeDelegate = delegate ()
            {
                Close();
            };

            Thread t = new Thread(new ThreadStart(Show));
            t.Start();
        }

        void ChangeContent(string content)
        {
            this.label1.Content = content;
        }

        private void Show()
        {
            content = "显示的内容,3秒后关闭";
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, contentDelegate);
            Thread.Sleep(1000);

            content = "显示的内容,2秒后关闭";
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, contentDelegate);
            Thread.Sleep(1000);

            content = "显示的内容,1秒后关闭";
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, contentDelegate);
            Thread.Sleep(1000);

            this.Dispatcher.BeginInvoke(DispatcherPriority.Background, closeDelegate);
        }
    }
}
