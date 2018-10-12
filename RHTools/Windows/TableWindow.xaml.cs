using DevComponents.WPF.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RHTools
{
    public partial class TableWindow : Window
    {
        public TableWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Style cellStyle = new Style(typeof(DataGridCell));

            FrameworkElementFactory textBlockFEF = new FrameworkElementFactory(typeof(TextBlock));
            textBlockFEF.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            textBlockFEF.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center);
            textBlockFEF.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            textBlockFEF.AppendChild(new FrameworkElementFactory(typeof(ContentPresenter)));
            ControlTemplate cellTemplate = new ControlTemplate(typeof(DataGridCell));
            cellTemplate.VisualTree = textBlockFEF;

            cellStyle.Setters.Add(new Setter(DataGridCell.TemplateProperty, cellTemplate));
            //cellStyle.Setters.Add(new Setter(DataGridCell.HeightProperty, 32));
            cellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, new SolidColorBrush(Colors.White)));
            cellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            //cellStyle.Setters.Add(new Setter(DataGridCell.FontSizeProperty, 16));
            cellStyle.Setters.Add(new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold));

            DataGrid1.CellStyle = cellStyle;

            List<TestElement> tes = new List<TestElement>();
            tes.Add(new TestElement() { Id = "ID`1", Name = "Tomson", Info1 = "Info1Info1Info1Info1Info1", Info2 = "Info2", Info3 = "Info3", Info4 = "Info4" });
            tes.Add(new TestElement() { Id = "ID`1", Name = "Tomson", Info1 = "Info1Info1Info1Info1Info1", Info2 = "Info2", Info3 = "Info3", Info4 = "Info4" });
            tes.Add(new TestElement() { Id = "ID`1", Name = "Tomson", Info1 = "Info1Info1Info1Info1Info1", Info2 = "Info2", Info3 = "Info3", Info4 = "Info4" });
            tes.Add(new TestElement() { Id = "ID`1", Name = "Tomson", Info1 = "Info1Info1Info1Info1Info1", Info2 = "Info2", Info3 = "Info3", Info4 = "Info4" });

            DataGrid1.ItemsSource = tes;
            for (int i = 0; i < DataGrid1.Columns.Count; i++)
            {
                DataGridColumn column = DataGrid1.Columns[i];
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }



            InitDataGrid2();
        }

        private void InitDataGrid2()
        {
            Style style = new Style(typeof(DataGrid));
            Style columnHeaderStyle = new Style(typeof(DataGridColumnHeader));
            Style rowStyle = new Style(typeof(DataGridRow));

            style.Setters.Add(new Setter(DataGrid.IsHitTestVisibleProperty, false));
            style.Setters.Add(new Setter(DataGrid.HeadersVisibilityProperty, DataGridHeadersVisibility.Column));
            style.Setters.Add(new Setter(DataGrid.HorizontalAlignmentProperty, HorizontalAlignment.Left));
            style.Setters.Add(new Setter(DataGrid.VerticalAlignmentProperty, VerticalAlignment.Top));
            style.Setters.Add(new Setter(DataGrid.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Hidden));
            style.Setters.Add(new Setter(DataGrid.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Hidden));
            style.Setters.Add(new Setter(DataGrid.GridLinesVisibilityProperty, DataGridGridLinesVisibility.All));
            style.Setters.Add(new Setter(DataGrid.HorizontalGridLinesBrushProperty, new SolidColorBrush(Colors.White)));
            style.Setters.Add(new Setter(DataGrid.VerticalGridLinesBrushProperty, new SolidColorBrush(Colors.White)));
            style.Setters.Add(new Setter(DataGrid.BorderBrushProperty, new SolidColorBrush(Colors.White)));
            style.Setters.Add(new Setter(DataGrid.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            style.Setters.Add(new Setter(DataGrid.BorderThicknessProperty, new Thickness(1)));

            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.HeightProperty, 50d));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.ForegroundProperty, new SolidColorBrush(Colors.White)));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.BorderBrushProperty, new SolidColorBrush(Colors.White)));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.BorderBrushProperty, new SolidColorBrush(Colors.White)));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.BorderThicknessProperty, new Thickness(0, 0, 1, 1)));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.MarginProperty, new Thickness(0)));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.FontSizeProperty, 25d));
            columnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.FontWeightProperty, FontWeights.Normal));

            rowStyle.Setters.Add(new Setter(DataGridRow.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            rowStyle.Setters.Add(new Setter(DataGridRow.BorderBrushProperty, new SolidColorBrush(Colors.White)));

            Style cellStyle = new Style(typeof(DataGridCell));
            FrameworkElementFactory textFe = new FrameworkElementFactory(typeof(TextBlock));
            textFe.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center); //单元格对齐
            textFe.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            textFe.AppendChild(new FrameworkElementFactory(typeof(ContentPresenter)));
            ControlTemplate cellTemplate = new ControlTemplate(typeof(DataGridCell));
            cellTemplate.VisualTree = textFe;

            cellStyle.Setters.Add(new Setter(DataGridCell.TemplateProperty, cellTemplate));
            cellStyle.Setters.Add(new Setter(DataGridCell.HeightProperty, 40d)); // 行高度
            cellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, new SolidColorBrush(Colors.White)));
            cellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            cellStyle.Setters.Add(new Setter(DataGridCell.FontSizeProperty, 12d)); // 字号
            cellStyle.Setters.Add(new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold));

            DataGrid2.Style = style;
            DataGrid2.ColumnHeaderStyle = columnHeaderStyle;
            DataGrid2.RowStyle = rowStyle;
            DataGrid2.CellStyle = cellStyle;

            DataGrid2.Height = 210;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<TestElement> tes = new List<TestElement>();
            tes.Add(new TestElement() { Id = "ID`1", Name = "Tomson", Info1 = "Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1", Info2 = "Info2", Info3 = "Info3", Info4 = "Info4" });
            tes.Add(new TestElement() { Id = "ID`1", Name = "Tomson", Info1 = "Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1", Info2 = "Info2", Info3 = "Info3", Info4 = "Info4" });
            tes.Add(new TestElement() { Id = "ID`1", Name = "Tomson", Info1 = "Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1", Info2 = "Info2", Info3 = "Info3", Info4 = "Info4" });
            tes.Add(new TestElement() { Id = "ID`1", Name = "Tomson", Info1 = "Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1Info1", Info2 = "Info2", Info3 = "Info3", Info4 = "Info4" });


            //必须通过DataGridTextColumn设置TextBlock.TextWrappingProperty,才能使单元格内容自动换行
            {
                DataGridTextColumn column = new DataGridTextColumn();
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                column.Header = "Info1";
                column.Binding = new Binding("Info1");

                Style st = new Style(typeof(TextBlock));
                Setter setter = new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
                Setter setter2 = new Setter(TextBlock.MarginProperty, new Thickness(15));
                st.Setters.Add(setter);
                st.Setters.Add(setter2);

                column.ElementStyle = st;
                DataGrid2.Columns.Add(column);
            }

            DataGrid2.ItemsSource = tes;

            for (int i = 0; i < DataGrid2.Columns.Count; i++)
            {
                DataGridColumn column = DataGrid2.Columns[i];
                column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }


            ImageBrush b = new ImageBrush();
            b.ImageSource = new BitmapImage(new Uri("/RHTools;component/img/1.png", UriKind.Relative));
            MessageBox.Show(b.ImageSource.ToString());
        }
    }

    class TestElement
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        public string Info3 { get; set; }
        public string Info4 { get; set; }
    }
}
