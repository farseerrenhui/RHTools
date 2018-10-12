using System.Windows;
using System.Windows.Controls;

namespace RHTools
{
    public partial class RoutedEventSample : Window
    {
        public RoutedEventSample()
        {
            InitializeComponent();
        }

        private void Button_Click_AddHandler(object sender, RoutedEventArgs e)
        {
            this.Button1.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));
            this.Button2.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));
            this.Button3.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));

            this.GridButtons.AddHandler(Button.ClickEvent, new RoutedEventHandler(Button_Click));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show((e.OriginalSource as Button).Content.ToString());
        }

    }
}
