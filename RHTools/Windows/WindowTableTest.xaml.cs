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
    public partial class WindowTableTest : Window
    {
        public WindowTableTest()
        {
            InitializeComponent();


        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Col1", typeof(string));
            dt.Columns.Add("Col2", typeof(string));
            dt.Columns.Add("Col3", typeof(string));

            dt.Rows.Add(new string[] { "aaa", "bbbbb", "Mono是一个由Xamarin公司（先前是Novell，最早为Ximian）所主持的自由开放源代码项目。[1]  该项目的目标是创建一系列匹配ECMA标准（Ecma-334和Ecma-335）的.NET工具" });
            dt.Rows.Add(new string[] { "aaa", "bbbbb", "Mono是一个由Xamarin公司（先前是Novell，最早为Ximian）所主持的自由开放源代码项目。[1]  该项目的目标是创建一系列匹配ECMA标准（Ecma-334和Ecma-335）的.NET工具" });
            dt.Rows.Add(new string[] { "aaa", "bbbbb", "Mono是一个由Xamarin公司（先前是Novell，最早为Ximian）所主持的自由开放源代码项目。[1]  该项目的目标是创建一系列匹配ECMA标准（Ecma-334和Ecma-335）的.NET工具" });
            dt.Rows.Add(new string[] { "aaa", "bbbbb", "Mono是一个由Xamarin公司（先前是Novell，最早为Ximian）所主持的自由开放源代码项目。[1]  该项目的目标是创建一系列匹配ECMA标准（Ecma-334和Ecma-335）的.NET工具" });
            dt.Rows.Add(new string[] { "aaa", "bbbbb", "Mono是一个由Xamarin公司（先前是Novell，最早为Ximian）所主持的自由开放源代码项目。[1]  该项目的目标是创建一系列匹配ECMA标准（Ecma-334和Ecma-335）的.NET工具" });
            dt.Rows.Add(new string[] { "aaa", "bbbbb", "Mono是一个由Xamarin公司（先前是Novell，最早为Ximian）所主持的自由开放源代码项目。[1]  该项目的目标是创建一系列匹配ECMA标准（Ecma-334和Ecma-335）的.NET工具" });
            dt.Rows.Add(new string[] { "aaa", "bbbbb", "Mono是一个由Xamarin公司（先前是Novell，最早为Ximian）所主持的自由开放源代码项目。[1]  该项目的目标是创建一系列匹配ECMA标准（Ecma-334和Ecma-335）的.NET工具" });
            dt.Rows.Add(new string[] { "aaa", "bbbbb", "Mono是一个由Xamarin公司（先前是Novell，最早为Ximian）所主持的自由开放源代码项目。[1]  该项目的目标是创建一系列匹配ECMA标准（Ecma-334和Ecma-335）的.NET工具" });

            Table1.SetColumn("Col1", 60);
            Table1.SetColumn("Col2", 60);
            Table1.SetColumn("Col3", 160);
            Table1.UpdateDataSource(dt);
            Table1.Width = double.NaN;

            Table2.SetColumn("Col1", 60);
            Table2.SetColumn("Col2", 60);
            Table2.SetColumn("Col3", 160);
            Table2.UpdateDataSource(dt);

            Table3.SetColumn("Col1", 60);
            Table3.SetColumn("Col2", 60);
            Table3.SetColumn("Col3", 160);
            Table3.UpdateDataSource(dt);
            Table3.Width = 240;
            Table3.Height = 120;


            datagrid1.ItemsSource = dt.DefaultView;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Table3.RowHeight = 60;

            col1.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            col2.Width = new DataGridLength(2, DataGridLengthUnitType.Star);
            col3.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            col4.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
        }
    }
}
