using System.Windows;
using System.Windows.Controls;

namespace RHTools
{
    public partial class Transform : Window
    {
        public Transform()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((sender as Button).ActualWidth.ToString());
        }
    }
}
