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
    public partial class StyleSample : Window
    {
        public StyleSample()
        {
            InitializeComponent();

            List<Player> personList = new List<Player>();
            personList.Add(new Player() { ID = "1", Name = "Alice", Age = 16 });
            personList.Add(new Player() { ID = "2", Name = "Billy", Age = 17 });
            personList.Add(new Player() { ID = "3", Name = "Cate", Age = 18 });
            personList.Add(new Player() { ID = "4", Name = "Tom", Age = 16 });
            personList.Add(new Player() { ID = "5", Name = "Jeff", Age = 16 });
            listBox1.ItemsSource = personList;
        }
    }

    class LengthToBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int textLength = (int)value;
            return textLength > 10 ? true : false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //未用到
            throw new NotImplementedException();
        }
    }

    class Player
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }

}
