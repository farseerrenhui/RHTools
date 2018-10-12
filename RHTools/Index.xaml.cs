using RHTools.WPFStyle;
using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Windows;
using static RHTools.Win32HardwareInfos;

namespace RHTools
{
    public partial class Index : Window
    {
        public Index()
        {
            InitializeComponent();

            string win32ToolTip = "";
            foreach (HardwareEnum hardwareEnum in Enum.GetValues(typeof(HardwareEnum)))
                win32ToolTip += hardwareEnum.ToString() + "\r\n";
            Win32HardwareInfos.ToolTip = win32ToolTip.Trim();
        }
    }
}
