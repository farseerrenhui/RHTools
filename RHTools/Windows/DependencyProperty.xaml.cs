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
    public partial class DependencyPropertySample : Window
    {
        //如果stu对象不是成员变量,则构造方法中的Binging失效
        Student stu;

        public DependencyPropertySample()
        {
            InitializeComponent();

            stu = new Student();
            stu.SetBinding(Student.NameProperty, new Binding("Text") { Source = this.textBox1, Mode = BindingMode.TwoWay });
            this.textBox2.SetBinding(TextBox.TextProperty, new Binding("Name") { Source = stu, Mode = BindingMode.TwoWay });
        }

        private void Button_Click_Show(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(stu.Name);
        }

        private void Button_Click_Attach(object sender, RoutedEventArgs e)
        {
            Student student = new Student();
            System.Windows.Forms.MessageBox.Show("属性附加前:" + School.GetGrade(student));
            School.SetGrade(student, int.Parse(this.textBox3.Text));
            System.Windows.Forms.MessageBox.Show("属性附加后:" + School.GetGrade(student));
        }
    }

    class School : DependencyObject
    {
        public static int GetGrade(DependencyObject obj)
        {
            return (int)obj.GetValue(GradeProperty);
        }

        public static void SetGrade(DependencyObject obj, int value)
        {
            obj.SetValue(GradeProperty, value);
        }

        // Using a DependencyProperty as the backing store for Grade.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GradeProperty =
            DependencyProperty.RegisterAttached("Grade", typeof(int), typeof(School), new PropertyMetadata(0));
    }

    class Student : DependencyObject
    {
        //将Name属性暴露出来,以访问CLR属性的方式来操作依赖属性
        public string Name
        {
            get { return (string)GetValue(NameProperty); }
            set { SetValue(NameProperty, value); }
        }

        //依赖属性包装器的作用是以"实例属性"的形式向外界暴露依赖属性,这样,一个依赖属性才能成为数据源的一个Path
        public static readonly DependencyProperty NameProperty //这里是命名约定,成员变量名后加Property后缀
            = DependencyProperty.Register
            (
                "Name",//将来会使用名为"Name"的CLR属性来包装它
                typeof(string),//参数类型
                typeof(Student)//宿主类型,这里需要把NameProperty与Student关联
            );

        //添加FrameworkElement类中的SetBinding方法来升级这个类,使其实例对象可以通过SetBinding来关联数据
        public BindingExpressionBase SetBinding(DependencyProperty dp, BindingBase binding)
        {
            return BindingOperations.SetBinding(this, dp, binding);
        }
    }

}
