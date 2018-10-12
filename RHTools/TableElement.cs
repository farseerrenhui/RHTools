using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Threading;

namespace RHTools
{
    public class TableElement : DataGrid
    {
        private DispatcherTimer _timer;

        /// <summary>
        /// 翻页时间间隔
        /// </summary>
        private int TurnPageInterval { get; set; }

        /// <summary>
        /// 页数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 每页行数
        /// </summary>
        public int RowsPerPage { get; set; }

        /// <summary>
        /// 当前页号
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        private DataTable _dataSource = null;

        public TableElement(int RowsPerPage, int TurnPageInterval)
        {
            this.RowsPerPage = RowsPerPage;
            this.TurnPageInterval = TurnPageInterval;
            this.CurrentPage = 0;

            this.HorizontalAlignment = HorizontalAlignment.Stretch;
            this.HorizontalContentAlignment = HorizontalAlignment.Stretch;
            this.VerticalAlignment = VerticalAlignment.Stretch;
            this.VerticalContentAlignment = VerticalAlignment.Stretch;
        }

        public void UpdateDataSource(DataTable dt)
        {
            // 判断dt数据是否有更新，只有更新时才执行下面的操作
            this._dataSource = dt;
            this.PageCount = (dt.Rows.Count + RowsPerPage - 1) / RowsPerPage;
            this.CurrentPage = 0;

            // 创建并启动定时器
            if (_timer == null)
            {
                _timer = new DispatcherTimer(DispatcherPriority.Send, this.Dispatcher);
                _timer.Interval = new TimeSpan(0, 0, TurnPageInterval);
                _timer.Tick += TurnPage;
            }
            else if (_timer.IsEnabled)
            {
                // 先停止原定时器
                _timer.Stop();
            }
            // 启动定时器
            _timer.Start();

            DataTable showData = _dataSource.Clone();
            if (dt.Rows.Count >= RowsPerPage)
            {
                // 将数据全集中的行拷贝到展示的DataTable中
                for (int i = 0; i < RowsPerPage; i++)
                {
                    showData.Rows.Add(dt.Rows[i].ItemArray);
                }
            }
            else
            {
                // 将有效数据行拷贝到展示表中
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    showData.Rows.Add(dt.Rows[i].ItemArray);
                }

                // 增加剩余的空行
                for (int i = dt.Rows.Count; i < RowsPerPage; i++)
                {
                    showData.Rows.Add(showData.NewRow());
                }
            }

            this.ItemsSource = showData.DefaultView;
        }

        private void TurnPage(object sender, EventArgs e)
        {
            if (PageCount == 0)
                PageCount = 1;

            CurrentPage = (CurrentPage + 1) % PageCount;

            DataTable showData = _dataSource.Clone();
            if ((_dataSource.Rows.Count - CurrentPage * RowsPerPage) >= RowsPerPage)
            {
                // 将数据全集中的行拷贝到展示的DataTable中
                for (int i = CurrentPage * RowsPerPage; i < (CurrentPage + 1) * RowsPerPage; i++)
                {
                    showData.Rows.Add(_dataSource.Rows[i].ItemArray);
                }
            }
            else
            {
                // 将有效数据行拷贝到展示表中
                for (int i = CurrentPage * RowsPerPage; i < _dataSource.Rows.Count; i++)
                {
                    showData.Rows.Add(_dataSource.Rows[i].ItemArray);
                }

                // 增加剩余的空行
                for (int i = (CurrentPage + 1) * RowsPerPage; i > _dataSource.Rows.Count; i--)
                {
                    showData.Rows.Add(showData.NewRow());
                }
            }

            this.ItemsSource = showData.DefaultView;
        }

        double _TableWidth;

        Style _TableStyle = new Style(typeof(DataGrid));
        Style _ColumnHeaderStyle = new Style(typeof(DataGridColumnHeader));
        Style _RowStyle = new Style(typeof(DataGridRow));
        Style _CellStyle = new Style(typeof(DataGridCell));
        FrameworkElementFactory _CellStyleFEF = new FrameworkElementFactory(typeof(TextBlock));
        HorizontalAlignment _CellHorizontalAlignment = HorizontalAlignment.Center;
        VerticalAlignment _CellVerticalAlignment = VerticalAlignment.Center;

        public Thickness TableBorderThickness
        {
            get { return this.BorderThickness; }
            set
            {
                this.BorderThickness = value;
                this._TableWidth = value.Left + value.Right;
            }
        }

        public void SetTableStyle()
        {
            _TableStyle.Setters.Add(new Setter(DataGrid.CanUserAddRowsProperty, false));
            _TableStyle.Setters.Add(new Setter(DataGrid.AutoGenerateColumnsProperty, false));
            _TableStyle.Setters.Add(new Setter(DataGrid.IsHitTestVisibleProperty, false));
            _TableStyle.Setters.Add(new Setter(DataGrid.HeadersVisibilityProperty, DataGridHeadersVisibility.Column));
            _TableStyle.Setters.Add(new Setter(DataGrid.HorizontalAlignmentProperty, HorizontalAlignment.Left));
            _TableStyle.Setters.Add(new Setter(DataGrid.VerticalAlignmentProperty, VerticalAlignment.Top));
            _TableStyle.Setters.Add(new Setter(DataGrid.HorizontalScrollBarVisibilityProperty, ScrollBarVisibility.Hidden));
            _TableStyle.Setters.Add(new Setter(DataGrid.VerticalScrollBarVisibilityProperty, ScrollBarVisibility.Hidden));
            _TableStyle.Setters.Add(new Setter(DataGrid.GridLinesVisibilityProperty, DataGridGridLinesVisibility.All));
            _TableStyle.Setters.Add(new Setter(DataGrid.HorizontalGridLinesBrushProperty, new SolidColorBrush(Colors.White)));
            _TableStyle.Setters.Add(new Setter(DataGrid.VerticalGridLinesBrushProperty, new SolidColorBrush(Colors.White)));
            _TableStyle.Setters.Add(new Setter(DataGrid.BorderBrushProperty, new SolidColorBrush(Colors.White)));
            _TableStyle.Setters.Add(new Setter(DataGrid.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            _TableStyle.Setters.Add(new Setter(DataGrid.BorderThicknessProperty, new Thickness(1)));

            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.HeightProperty, 50d));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.ForegroundProperty, new SolidColorBrush(Colors.White)));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.BorderBrushProperty, new SolidColorBrush(Colors.White)));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.HorizontalContentAlignmentProperty, HorizontalAlignment.Center));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.VerticalContentAlignmentProperty, VerticalAlignment.Center));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.BorderBrushProperty, new SolidColorBrush(Colors.White)));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.BorderThicknessProperty, new Thickness(0, 0, 1, 1)));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.MarginProperty, new Thickness(0)));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.FontSizeProperty, 25d));
            _ColumnHeaderStyle.Setters.Add(new Setter(DataGridColumnHeader.FontWeightProperty, FontWeights.Normal));

            _RowStyle.Setters.Add(new Setter(DataGridRow.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            _RowStyle.Setters.Add(new Setter(DataGridRow.BorderBrushProperty, new SolidColorBrush(Colors.White)));

            _CellStyleFEF.SetValue(TextBlock.HorizontalAlignmentProperty, HorizontalAlignment.Center); //单元格对齐
            _CellStyleFEF.SetValue(TextBlock.VerticalAlignmentProperty, VerticalAlignment.Center);
            _CellStyleFEF.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            _CellStyleFEF.AppendChild(new FrameworkElementFactory(typeof(ContentPresenter)));
            ControlTemplate cellTemplate = new ControlTemplate(typeof(DataGridCell));
            cellTemplate.VisualTree = _CellStyleFEF;

            _CellStyle.Setters.Add(new Setter(DataGridCell.TemplateProperty, cellTemplate));
            //cellStyle.Setters.Add(new Setter(DataGridCell.HeightProperty, 40d)); // 行高度
            _CellStyle.Setters.Add(new Setter(DataGridCell.ForegroundProperty, new SolidColorBrush(Colors.White)));
            _CellStyle.Setters.Add(new Setter(DataGridCell.BackgroundProperty, new SolidColorBrush(Colors.Transparent)));
            _CellStyle.Setters.Add(new Setter(DataGridCell.FontSizeProperty, 12d)); // 字号
            _CellStyle.Setters.Add(new Setter(DataGridCell.FontWeightProperty, FontWeights.Bold));

            this.Style = _TableStyle;
            this.ColumnHeaderStyle = _ColumnHeaderStyle;
            this.RowStyle = _RowStyle;
            this.CellStyle = _CellStyle;
        }

        public TableElement() : this(5, 3)
        {
            SetTableStyle();
        }

        public void SetColumn(string header, double width)
        {
            DataGridTextColumn dgc = new DataGridTextColumn();
            dgc.Header = header;
            dgc.Width = width;
            Binding binding = new Binding(header);
            binding.Mode = BindingMode.OneWay;
            dgc.Binding = binding;

            //支持多行显示的风格设置 20161018 renhui
            Style style = new Style(typeof(TextBlock));
            Setter setter = new Setter(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            style.Setters.Add(setter);
            dgc.ElementStyle = style;

            this.Columns.Add(dgc);

            _TableWidth += width;
            this.Width = _TableWidth;
        }

        private void SetCellHorizontalAlignment(object value)
        {
            _CellStyleFEF = new FrameworkElementFactory(typeof(TextBlock));
            _CellStyleFEF.SetValue(TextBlock.HorizontalAlignmentProperty, value);
            _CellStyleFEF.SetValue(TextBlock.VerticalAlignmentProperty, _CellVerticalAlignment);
            _CellStyleFEF.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            _CellStyleFEF.AppendChild(new FrameworkElementFactory(typeof(ContentPresenter)));
            ControlTemplate cellTemplate = new ControlTemplate(typeof(DataGridCell));
            cellTemplate.VisualTree = _CellStyleFEF;
            _CellStyle.Setters.Add(new Setter(DataGridCell.TemplateProperty, cellTemplate));
        }

        private void SetCellVerticalAlignment(object value)
        {
            _CellStyleFEF = new FrameworkElementFactory(typeof(TextBlock));
            _CellStyleFEF.SetValue(TextBlock.HorizontalAlignmentProperty, _CellHorizontalAlignment);
            _CellStyleFEF.SetValue(TextBlock.VerticalAlignmentProperty, value);
            _CellStyleFEF.SetValue(TextBlock.TextWrappingProperty, TextWrapping.Wrap);
            _CellStyleFEF.AppendChild(new FrameworkElementFactory(typeof(ContentPresenter)));
            ControlTemplate cellTemplate = new ControlTemplate(typeof(DataGridCell));
            cellTemplate.VisualTree = _CellStyleFEF;
            _CellStyle.Setters.Add(new Setter(DataGridCell.TemplateProperty, cellTemplate));
        }

        public _HorizontalAlignment CellHorizontalAlignment
        {
            get { return _HorizontalAlignment.中; }
            set
            {
                if (value == _HorizontalAlignment.右)
                {
                    SetCellHorizontalAlignment(HorizontalAlignment.Right);
                    this.CellStyle = _CellStyle;

                    foreach (DataGridColumn column in this.Columns)
                    {
                        column.CellStyle = _CellStyle;
                    }
                }
            }
        }

        public _VerticalAlignment CellVerticalAlignment
        {
            get { return _VerticalAlignment.中; }
            set
            {
                if (value == _VerticalAlignment.下)
                {
                    SetCellVerticalAlignment(VerticalAlignment.Bottom);
                    this.CellStyle = _CellStyle;

                    foreach (DataGridColumn column in this.Columns)
                    {
                        column.CellStyle = _CellStyle;
                    }
                }
            }
        }
    }

    public enum _HorizontalAlignment
    {
        左,
        中,
        右
    }

    public enum _VerticalAlignment
    {
        上,
        中,
        下
    }

}
