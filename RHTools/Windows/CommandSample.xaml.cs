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
    public partial class CommandSample : Window
    {
        //声明并定义命令
        private RoutedCommand clearCmd = new RoutedCommand("Clear", typeof(CommandSample));

        public CommandSample()
        {
            InitializeComponent();

            //把命令赋值给命令源(发送者)并指定快捷键
            this.button1.Command = this.clearCmd;
            this.clearCmd.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Alt, "Alt+C"));
            //指定命令目标
            this.button1.CommandTarget = this.textBoxA;

            //创建命令关联
            CommandBinding commandBinding = new CommandBinding();
            commandBinding.Command = this.clearCmd;
            commandBinding.CanExecute += new CanExecuteRoutedEventHandler(cb_CanExecute);
            commandBinding.Executed += new ExecutedRoutedEventHandler(cb_Execute);

            //把命令安置textBoxA外层的控件上(包含textBoxA)
            this.tabItem1.CommandBindings.Add(commandBinding);
            //安置在以下控件上同样生效
            //this.stackPanel1.CommandBindings.Add(commandBinding);
            //this.textBoxA.CommandBindings.Add(commandBinding);
        }

        private void cb_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(textBoxA.Text))
                e.CanExecute = false;
            else
                e.CanExecute = true;

            //避免继续继续向上传递而降低程序性能
            e.Handled = true;
        }

        private void cb_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            textBoxA.Clear();

            //避免继续继续向上传递而降低程序性能
            e.Handled = true;
        }

        #region New Command

        private void New_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.nameTextBox.Text))
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }
        private void New_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter == null)
                MessageBox.Show("快捷键触发的命令没有CommandParameter");

            if (e.Parameter.ToString() == "Teacher")
                this.listBoxNewItems.Items.Add(string.Format("New Teacher :{0}", nameTextBox.Text));
            else if (e.Parameter.ToString() == "Student")
                this.listBoxNewItems.Items.Add(string.Format("New Student :{0}", nameTextBox.Text));
        }

        #endregion
    }

    public class MyCommand
    {
        private static RoutedUICommand requery;

        public static RoutedUICommand Requery
        {
            get { return MyCommand.requery; }
            /*set { MyCommand.requery = value; }*/
        }

        static MyCommand()
        {
            InputGestureCollection inputs = new InputGestureCollection();
            inputs.Add(new KeyGesture(Key.E, ModifierKeys.Control));
            inputs.Add(new KeyGesture(Key.R, ModifierKeys.Control, "[Ctrl]+[R]"));
            requery = new RoutedUICommand("Requery", "Requery", typeof(MyCommand), inputs);
        }
    }
}
