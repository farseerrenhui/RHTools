using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace RHTools
{
    class IndexButton : Button
    {
        public IndexButton()
        {
            this.Click += IndexButtonClick;
        }

        private void IndexButtonClick(object sender, RoutedEventArgs e)
        {
            // 获取当前程序集 
            Assembly assembly = Assembly.GetExecutingAssembly();
            //类的完全限定名（即包括命名空间）
            string fullClassName = this.GetType().Namespace + "." + (sender as IndexButton).Content.ToString();

            Window window = assembly.CreateInstance(fullClassName) as Window;
            window.Owner = Window.GetWindow(this);
            window.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }
    }
}
