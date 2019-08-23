using System;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace RHTools
{
    class IndexButton : Button
    {
        public IndexButton()
        {
            this.Click += IndexButtonClick;
            this.Background = new SolidColorBrush(Colors.AliceBlue);
        }

        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            this.Background = new SolidColorBrush(Colors.DarkGray);
            this.Foreground = new SolidColorBrush(Colors.White);
        }

        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            this.Background = new SolidColorBrush(Colors.AliceBlue);
            this.Foreground = new SolidColorBrush(Colors.Black);
        }

        public void IndexButtonClick(object sender, RoutedEventArgs e)
        {
            // 获取当前程序集 
            Assembly assembly = Assembly.GetExecutingAssembly();
            //类的完全限定名（即包括命名空间）
            string fullClassName = this.GetType().Namespace + "." + (sender as IndexButton).Content.ToString();

            Window window = assembly.CreateInstance(fullClassName) as Window;
            window.Owner = Window.GetWindow(this);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.KeyDown += WindowKeyDown();
            window.ShowDialog();
        }

        /// <summary>
        /// 为窗体添加Esc关闭功能
        /// </summary>
        /// <returns></returns>
        private KeyEventHandler WindowKeyDown()
        {
            void target(object sender, KeyEventArgs e)
            {
                if (e.Key == Key.Escape)
                    (sender as Window).Close();
            }

            KeyEventHandler keyEventHandler = new KeyEventHandler(target);
            return keyEventHandler;
        }
    }
}
