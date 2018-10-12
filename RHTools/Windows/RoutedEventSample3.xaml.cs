using System.Windows;
using System.Windows.Controls;

namespace RHTools
{
    public partial class RoutedEventSample3 : Window
    {
        Person person = new Person() { Id = 101, Name = "Lucy" };

        public RoutedEventSample3()
        {
            InitializeComponent();

            //为外层Grid添加路由事件侦听器
            //写法1
            //gridPerson.AddHandler(Person.NameChangedEvent, new RoutedEventHandler(PersonNameChangedhandler));
            //stackPanelPerson.AddHandler(Person.NameChangedEvent, new RoutedEventHandler(PersonNameChangedhandler));
            //写法2
            Person.AddNameChangedHandler(gridPerson, new RoutedEventHandler(PersonNameChangedhandler));
            Person.AddNameChangedHandler(stackPanelPerson, new RoutedEventHandler(PersonNameChangedhandler));

        }

        //gridPerson捕捉到NameChangedEvent后的处理器
        private void PersonNameChangedhandler(object sender, RoutedEventArgs e)
        {
            Person person = e.OriginalSource as Person;
            MessageBox.Show(sender.GetType() + ".\n" + "OriginalSource(Person):\nName:" + person.Name + " ID:" + person.Id);
        }

        private void buttonPerson_1_Click(object sender, RoutedEventArgs e)
        {
            //单独的改变Name属性并不会引发事件,NameChangedEvent只是一个名称而已,必须使用RaiseEvent
            person.Name = "Lily(Button1)";
        }

        private void buttonPerson_2_Click(object sender, RoutedEventArgs e)
        {
            //只要使用RaiseEvent就会引发事件

            RoutedEventArgs args = new RoutedEventArgs(Person.NameChangedEvent, person);
            //被点击的按钮发送路由事件
            (sender as Button).RaiseEvent(args);
        }

        private void buttonPerson_3_Click(object sender, RoutedEventArgs e)
        {
            person.Name = "Lucia(Button3)";

            RoutedEventArgs args = new RoutedEventArgs(Person.NameChangedEvent, person);
            //指定buttonPerson发送路由事件
            buttonPerson.RaiseEvent(args);
        }
    }

    class Person
    {
        //声明并定义路由事件
        public static readonly RoutedEvent NameChangedEvent = EventManager.RegisterRoutedEvent("NameChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(Person));

        //为界面元素添加路由事件侦听
        public static void AddNameChangedHandler(DependencyObject d, RoutedEventHandler h)
        {
            UIElement e = d as UIElement;
            if (e != null)
                e.AddHandler(Person.NameChangedEvent, h);
        }

        //移除侦听
        public static void RemoveNameChangedHandler(DependencyObject d, RoutedEventHandler h)
        {
            UIElement e = d as UIElement;
            if (e != null)
                e.RemoveHandler(Person.NameChangedEvent, h);
        }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}
