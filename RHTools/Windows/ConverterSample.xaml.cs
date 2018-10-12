using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class ConverterSample : Window
    {
        public ConverterSample()
        {
            InitializeComponent();

            List<Plane> planeList = new List<Plane>()
            {
                new Plane(){Category = Category.Bomber,Name = "B-1",State = State.Unknown},
                new Plane(){Category = Category.Bomber,Name = "B-2",State = State.Unknown},
                new Plane(){Category = Category.Fighter,Name = "F-22",State = State.Unknown},
                new Plane(){Category = Category.Fighter,Name = "Su-47",State = State.Unknown},
                new Plane(){Category = Category.Bomber,Name = "B-52",State = State.Unknown},
                new Plane(){Category = Category.Fighter,Name = "J-10",State = State.Unknown},
            };

            this.lstPlane.ItemsSource = planeList;
        }
    }

    /// <summary>
    /// 定义一个飞机类
    /// </summary>
    public class Plane
    {
        public Category Category { get; set; }
        public string Name { get; set; }
        public State State { get; set; }
    }

    /// <summary>
    /// 飞机种类
    /// </summary>
    public enum Category
    {
        Bomber,
        Fighter
    }

    /// <summary>
    /// 状态
    /// </summary>
    public enum State
    {
        Available,
        Locked,
        Unknown
    }

    /// <summary>
    /// 定义一个飞机种类转化成数据源的类,实现IValueConverter接口
    /// </summary>
    public class CategoryToSourceConverter : IValueConverter
    {
        /// <summary>
        /// 将Category转化为uri
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Category c = (Category)value;
            switch (c)
            {
                case Category.Bomber:
                    return @"..\img\icon\Bomber.png";
                case Category.Fighter:
                    return @"..\img\icon\Fighter.png";
                default:
                    return null;
            }
        }

        //不会被调用
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// 定义一个飞机状态转化成bool类型数据源的类,实现IValueConverter接口
    /// </summary>
    public class StateToNullableBoolConverter : IValueConverter
    {
        /// <summary>
        /// 将State转化成bool?
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            State s = (State)value;
            switch (s)
            {
                case State.Locked:
                    return false;
                case State.Available:
                    return true;
                case State.Unknown:
                    return null;
                default:
                    return null;
            }
        }

        /// <summary>
        /// 将bool？转换成State
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? nb = (bool?)value;
            switch (nb)
            {
                case true:
                    return State.Available;
                case false:
                    return State.Locked;
                case null:
                    return State.Unknown;
                default:
                    return State.Unknown;
            }
        }
    }
}
