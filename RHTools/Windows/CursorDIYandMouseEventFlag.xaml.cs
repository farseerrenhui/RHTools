using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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
    public partial class CursorDIYandMouseEventFlag : Window
    {
        #region 鼠标操作支持

        //将枚举作为位域处理
        [Flags]
        enum MouseEventFlag : uint //设置鼠标动作的键值
        {
            Move = 0x0001,               //发生移动
            LeftDown = 0x0002,           //鼠标按下左键
            LeftUp = 0x0004,             //鼠标松开左键
            RightDown = 0x0008,          //鼠标按下右键
            RightUp = 0x0010,            //鼠标松开右键
            MiddleDown = 0x0020,         //鼠标按下中键
            MiddleUp = 0x0040,           //鼠标松开中键
            XDown = 0x0080,
            XUp = 0x0100,
            Wheel = 0x0800,              //鼠标轮被移动
            VirtualDesk = 0x4000,        //虚拟桌面
            Absolute = 0x8000
        }

        //设置鼠标位置
        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);

        //设置鼠标按键和动作
        [DllImport("user32.dll")]
        static extern void mouse_event(MouseEventFlag flags, int dx, int dy, uint data, UIntPtr extraInfo);

        //自定义光标
        [DllImport("user32.dll")]
        static extern IntPtr LoadCursorFromFile(string fileName);

        #endregion

        public CursorDIYandMouseEventFlag()
        {
            InitializeComponent();

            this.Cursor = new Cursor(AppDomain.CurrentDomain.BaseDirectory + "\\resources\\Kitty-Arrow.cur");
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            int step = 5;

            if (e.Key == Key.Left)
            {
                mouse_event(MouseEventFlag.Move, -step, 0, 0, UIntPtr.Zero);
            }
            else if (e.Key == Key.Right)
            {
                mouse_event(MouseEventFlag.Move, step, 0, 0, UIntPtr.Zero);
            }
            else if (e.Key == Key.Up)
            {
                mouse_event(MouseEventFlag.Move, 0, -step, 0, UIntPtr.Zero);
            }
            else if (e.Key == Key.Down)
            {
                mouse_event(MouseEventFlag.Move, 0, step, 0, UIntPtr.Zero);
            }
            else if (e.Key == Key.C)
            {
                mouse_event(MouseEventFlag.Move, 10, 10, 0, UIntPtr.Zero);
            }
        }
    }
}
