using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class EnvironmentInfos : Window
    {
        public EnvironmentInfos()
        {
            InitializeComponent();

            DataTable dt = new DataTable();
            dt.Columns.Add("Property");
            dt.Columns.Add("Value");

            dt.Rows.Add(new object[] { "Environment.CommandLine", Environment.CommandLine });
            dt.Rows.Add(new object[] { "Environment.CurrentDirectory", Environment.CurrentDirectory });
            dt.Rows.Add(new object[] { "Environment.ExitCode", Environment.ExitCode });
            dt.Rows.Add(new object[] { "Environment.Is64BitOperatingSystem", Environment.Is64BitOperatingSystem });
            dt.Rows.Add(new object[] { "Environment.Is64BitProcess", Environment.Is64BitProcess });
            dt.Rows.Add(new object[] { "Environment.HasShutdownStarted", Environment.HasShutdownStarted });
            dt.Rows.Add(new object[] { "Environment.MachineName", Environment.MachineName });
            dt.Rows.Add(new object[] { "Environment.NewLine", Environment.NewLine });
            dt.Rows.Add(new object[] { "Environment.OSVersion", Environment.OSVersion });
            dt.Rows.Add(new object[] { "Environment.ProcessorCount", Environment.ProcessorCount });
            dt.Rows.Add(new object[] { "Environment.StackTrace", Environment.StackTrace });
            dt.Rows.Add(new object[] { "Environment.SystemDirectory", Environment.SystemDirectory });
            dt.Rows.Add(new object[] { "Environment.TickCount", Environment.TickCount });
            dt.Rows.Add(new object[] { "Environment.UserDomainName", Environment.UserDomainName });
            dt.Rows.Add(new object[] { "Environment.UserInteractive", Environment.UserInteractive });
            dt.Rows.Add(new object[] { "Environment.UserName", Environment.UserName });
            dt.Rows.Add(new object[] { "Environment.Version", Environment.Version });
            dt.Rows.Add(new object[] { "Environment.WorkingSet", Environment.WorkingSet });

            DataGridEnviorment.ItemsSource = dt.DefaultView;
        }
    }
}
