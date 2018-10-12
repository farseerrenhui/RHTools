using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RHTools
{
    public partial class BubbleGrid : UserControl
    {
        public BubbleGrid()
        {
            InitializeComponent();

            //下面初始化标签的代码,需要在创建该类后,手动调用.在构造方法中调用时,由于属性没有手动初始化,所以数量上存在不同步的问题.
            for (int i = 0; i < 100/*VerticalLineCount*/ + 1; i++)
                xLabelString += ("业\n务\n系\n统\n" + i + ",");
            for (int i = 0; i < 100/*HorizontalLineCount*/; i++)
                yLabelString += ("数据指标" + i + ",");
        }

        #region 线框

        private double gridWidth = 800;

        private double gridHeight = 800;

        private double gridLineThickness = 2;
        /// <summary>
        /// 网格线粗细
        /// </summary>
        public double GridLineThickness
        {
            get { return gridLineThickness; }
            set
            {
                gridLineThickness = RectangleBorder.StrokeThickness = value;
                RefreshUI();
            }
        }

        private Brush gridBorderBrush = new SolidColorBrush(Colors.White);
        /// <summary>
        /// 边框颜色
        /// </summary>
        public Brush GridBorderBrush
        {
            get { return gridBorderBrush; }
            set
            {
                gridBorderBrush = RectangleBorder.Stroke = value;
                RefreshUI();
            }
        }

        private double outerBorderThickness = 2;
        /// <summary>
        /// 边框粗细
        /// </summary>
        public double OuterBorderThickness
        {
            get { return outerBorderThickness; }
            set
            {
                outerBorderThickness = value;
                BorderOuter.BorderThickness = new Thickness(value);
                RefreshUI();
            }
        }

        private Brush outerBorderBrush = new SolidColorBrush(Colors.White);
        /// <summary>
        /// 外边框颜色
        /// </summary>
        public Brush OuterBorderBrush
        {
            get { return outerBorderBrush; }
            set
            {
                outerBorderBrush = BorderOuter.BorderBrush = value;
                RefreshUI();
            }
        }

        private Thickness outerMargin = new Thickness(2);
        /// <summary>
        /// 外边框对齐
        /// </summary>
        public Thickness OuterMargin
        {
            get { return outerMargin; }
            set
            {
                outerMargin = value;
                RefreshUI();
            }
        }

        private Brush gridBrush = new SolidColorBrush(Colors.White);
        /// <summary>
        /// 网格线颜色
        /// </summary>
        public Brush GridBrush
        {
            get { return gridBrush; }
            set
            {
                gridBrush = value;
                RefreshUI();
            }
        }

        private int horizontalLineCount = 5;
        /// <summary>
        /// 水平线数量
        /// </summary>
        public int HorizontalLineCount
        {
            get { return horizontalLineCount; }
            set
            {
                horizontalLineCount = value;
                RefreshUI();
            }
        }

        private int verticalLineCount = 5;
        /// <summary>
        /// 垂直线数量
        /// </summary>
        public int VerticalLineCount
        {
            get { return verticalLineCount; }
            set
            {
                verticalLineCount = value;
                RefreshUI();
            }
        }

        #endregion

        #region X轴标签

        private string xLabelString = "";
        /// <summary>
        /// 逗号分隔,自动换行的文字默认左对齐,居中对齐需要输入\n控制
        /// </summary>
        public string XLabelString
        {
            get
            {
                return xLabelString;
            }
            set
            {
                xLabelString = value;
                RefreshUI();
            }
        }

        private _VerticalLabelPosition xLabelPosition = _VerticalLabelPosition.上;
        /// <summary>
        /// 位置
        /// </summary>
        public _VerticalLabelPosition XLabelPosition
        {
            get
            {
                return xLabelPosition;
            }
            set
            {
                xLabelPosition = value;
                RefreshUI();
            }
        }

        private double xLabelHeight = 50;
        /// <summary>
        /// 高度
        /// </summary>
        public double XLabelHeight
        {
            get
            {
                return xLabelHeight;
            }
            set
            {
                xLabelHeight = value;
                RefreshUI();
            }
        }

        private double xLabelFontSize = 21d;
        /// <summary>
        /// 字号
        /// </summary>
        public double XLabelFontSize
        {
            get
            {
                return xLabelFontSize;
            }
            set
            {
                xLabelFontSize = value;
                RefreshUI();
            }
        }

        private double xLabelMargin;
        /// <summary>
        /// 间距
        /// </summary>
        public double XLabelMargin
        {
            get
            {
                return xLabelMargin;
            }
            set
            {
                xLabelMargin = value;
                RefreshUI();
            }
        }

        private Brush xLabelForeground = new SolidColorBrush(Colors.White);
        /// <summary>
        /// 文字颜色
        /// </summary>
        public Brush XLabelForeground
        {
            get
            {
                return xLabelForeground;
            }
            set
            {
                xLabelForeground = value;
                RefreshUI();
            }
        }

        private VerticalAlignment xLabelVerticalAlignment = VerticalAlignment.Center;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        public _XLabelVerticalAlignment XLabelVerticalAlignment
        {
            get
            {
                if (xLabelVerticalAlignment == VerticalAlignment.Top)
                    return _XLabelVerticalAlignment.上;
                else if (xLabelVerticalAlignment == VerticalAlignment.Center)
                    return _XLabelVerticalAlignment.中;
                else
                    return _XLabelVerticalAlignment.下;
            }
            set
            {
                if (value == _XLabelVerticalAlignment.上)
                    xLabelVerticalAlignment = VerticalAlignment.Top;
                else if (value == _XLabelVerticalAlignment.中)
                    xLabelVerticalAlignment = VerticalAlignment.Center;
                else
                    xLabelVerticalAlignment = VerticalAlignment.Bottom;

                RefreshUI();
            }
        }

        #endregion

        #region Y轴标签

        private string yLabelString = "";
        /// <summary>
        /// 逗号分隔,自动换行的文字默认左对齐,居中对齐需要输入\n控制
        /// </summary>
        public string YLabelString
        {
            get
            {
                return yLabelString;
            }
            set
            {
                yLabelString = value;
                RefreshUI();
            }
        }

        private _HorizontalLabelPosition yLabelPosition = _HorizontalLabelPosition.左;
        /// <summary>
        /// 位置
        /// </summary>
        public _HorizontalLabelPosition YLabelPosition
        {
            get
            {
                return yLabelPosition;
            }
            set
            {
                yLabelPosition = value;
                RefreshUI();
            }
        }

        private double yLabelWidth = 160;
        /// <summary>
        /// 宽度
        /// </summary>
        public double YLabelWidth
        {
            get
            {
                return yLabelWidth;
            }
            set
            {
                yLabelWidth = value;
                RefreshUI();
            }
        }

        private double yLabelFontSize = 21d;
        /// <summary>
        /// 字号
        /// </summary>
        public double YLabelFontSize
        {
            get
            {
                return yLabelFontSize;
            }
            set
            {
                yLabelFontSize = value;
                RefreshUI();
            }
        }

        private double yLabelMargin;
        /// <summary>
        /// 间距
        /// </summary>
        public double YLabelMargin
        {
            get
            {
                return yLabelMargin;
            }
            set
            {
                yLabelMargin = value;
                RefreshUI();
            }
        }

        private Brush yLabelForeground = new SolidColorBrush(Colors.White);
        /// <summary>
        /// 文字颜色
        /// </summary>
        public Brush YLabelForeground
        {
            get
            {
                return yLabelForeground;
            }
            set
            {
                yLabelForeground = value;
                RefreshUI();
            }
        }

        private HorizontalAlignment yLabelHorizontalAlignment = HorizontalAlignment.Center;
        /// <summary>
        /// 垂直对齐
        /// </summary>
        public _YLabelHorizontalAlignment YLabelHorizontalAlignment
        {
            get
            {
                if (yLabelHorizontalAlignment == HorizontalAlignment.Left)
                    return _YLabelHorizontalAlignment.左;
                else if (yLabelHorizontalAlignment == HorizontalAlignment.Center)
                    return _YLabelHorizontalAlignment.中;
                else
                    return _YLabelHorizontalAlignment.右;
            }
            set
            {
                if (value == _YLabelHorizontalAlignment.左)
                    yLabelHorizontalAlignment = HorizontalAlignment.Left;
                else if (value == _YLabelHorizontalAlignment.中)
                    yLabelHorizontalAlignment = HorizontalAlignment.Center;
                else
                    yLabelHorizontalAlignment = HorizontalAlignment.Right;

                RefreshUI();
            }
        }

        #endregion

        #region 气泡

        private double bubbleRadius = 5d;
        public double BubbleRadius
        {
            get { return bubbleRadius; }
            set
            {
                bubbleRadius = value;
                DrawBubbles();
            }
        }

        private string bubbleState1;
        public string BubbleState1
        {
            get { return bubbleState1; }
            set
            {
                bubbleState1 = value;
            }
        }

        private string bubbleState2;
        public string BubbleState2
        {
            get { return bubbleState2; }
            set
            {
                bubbleState2 = value;
            }
        }

        #endregion

        private void RefreshUI()
        {
            DrawLines();
            DrawXLabels();
            DrawYLabels();
            DrawBubbles();
        }

        private void DrawXLabels()
        {
            for (int i = CanvasCore.Children.Count - 1; i >= 0; i--)
            {
                if (CanvasCore.Children[i] is Grid)
                {
                    if ((CanvasCore.Children[i] as Grid).Tag.ToString() == "X")
                        CanvasCore.Children.RemoveAt(i);
                }
            }

            double rowWidth = (gridWidth - gridLineThickness * 2) / (verticalLineCount - 1);
            double rowHeight = (gridHeight - gridLineThickness * 2) / (horizontalLineCount - 1);
            double offset = (xLabelPosition == _VerticalLabelPosition.上) ? 0d : (gridHeight + rowHeight / 2);

            string[] labels = xLabelString.Split(new string[] { "," }, StringSplitOptions.None);

            for (int i = 0; i < Math.Min(labels.Length, verticalLineCount); i++)
            {
                string[] texts = labels[i].Split(new string[] { "\\n" }, StringSplitOptions.None);
                Grid grid = new Grid()
                {
                    Tag = "X",
                    Width = rowWidth,
                    Height = xLabelHeight,
                    HorizontalAlignment = HorizontalAlignment.Center,
                };
                StackPanel stackPanel = new StackPanel()
                {
                    VerticalAlignment = xLabelVerticalAlignment,
                };
                switch (xLabelVerticalAlignment)
                {
                    case VerticalAlignment.Top:
                        stackPanel.Margin = new Thickness(0, xLabelMargin, 0, 0);
                        break;
                    case VerticalAlignment.Bottom:
                        stackPanel.Margin = new Thickness(0, 0, 0, xLabelMargin);
                        break;
                    default:
                        break;
                }
                for (int j = 0; j < texts.Length; j++)
                {
                    TextBlock xText = new TextBlock()
                    {
                        Text = texts[j],
                        FontWeight = FontWeights.Bold,
                        FontSize = xLabelFontSize,
                        Foreground = xLabelForeground,
                        TextWrapping = TextWrapping.Wrap,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    stackPanel.Children.Add(xText);
                }

                grid.Children.Add(stackPanel);
                Canvas.SetZIndex(grid, 100);
                Canvas.SetLeft(grid, RectangleBorder.Margin.Left + RectangleBorder.StrokeThickness + rowWidth * i - rowWidth / 2);
                Canvas.SetTop(grid, gridLineThickness + offset);
                CanvasCore.Children.Add(grid);
            }
        }

        private void DrawYLabels()
        {
            for (int i = CanvasCore.Children.Count - 1; i >= 0; i--)
            {
                if (CanvasCore.Children[i] is Grid)
                {
                    if ((CanvasCore.Children[i] as Grid).Tag.ToString() == "Y")
                        CanvasCore.Children.RemoveAt(i);
                }
            }

            double rowWidth = (gridWidth - gridLineThickness * 2) / (verticalLineCount - 1);
            double rowHeight = (gridHeight - gridLineThickness * 2) / (horizontalLineCount - 1);
            double offset = (yLabelPosition == _HorizontalLabelPosition.左) ? 0d : (gridWidth + rowWidth / 2);

            string[] labels = yLabelString.Split(new string[] { "," }, StringSplitOptions.None);

            for (int i = 0; i < Math.Min(labels.Length, horizontalLineCount); i++)
            {
                string[] texts = labels[i].Split(new string[] { "\\n" }, StringSplitOptions.None);
                Grid grid = new Grid()
                {
                    Tag = "Y",
                    //HorizontalAlignment = HorizontalAlignment.Center,
                    Width = yLabelWidth,
                    Height = rowHeight,
                };
                StackPanel stackPanel = new StackPanel()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                };
                switch (yLabelHorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        stackPanel.Margin = new Thickness(yLabelMargin, 0, 0, 0);
                        break;
                    case HorizontalAlignment.Right:
                        stackPanel.Margin = new Thickness(0, 0, yLabelMargin, 0);
                        break;
                    default:
                        break;
                }
                for (int j = 0; j < texts.Length; j++)
                {
                    TextBlock yText = new TextBlock()
                    {
                        Text = texts[j],
                        FontWeight = FontWeights.Bold,
                        FontSize = yLabelFontSize,
                        Foreground = yLabelForeground,
                        TextWrapping = TextWrapping.Wrap,
                        HorizontalAlignment = yLabelHorizontalAlignment,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    stackPanel.Children.Add(yText);
                }
                grid.Children.Add(stackPanel);

                Canvas.SetZIndex(grid, 100);
                Canvas.SetLeft(grid, RectangleBorder.StrokeThickness + offset);
                Canvas.SetTop(grid, RectangleBorder.Margin.Top + RectangleBorder.StrokeThickness + rowHeight * i - rowHeight / 2);
                CanvasCore.Children.Add(grid);
            }
        }

        private void DrawLines()
        {
            for (int i = CanvasCore.Children.Count - 1; i >= 0; i--)
            {
                if (CanvasCore.Children[i] is Line)
                    CanvasCore.Children.RemoveAt(i);
            }

            double rowHeight = (gridHeight - gridLineThickness * 2) / (horizontalLineCount - 1);
            double columnWidth = (gridWidth - gridLineThickness * 2) / (verticalLineCount - 1);

            //调整网格矩形框位置,后面的网格线坐标依赖RectangleBorder.Margin的Left和Top值
            double horizontalOffset = (yLabelPosition == _HorizontalLabelPosition.左) ? yLabelWidth : (0d + columnWidth / 2);
            double verticalLabelOffset = (xLabelPosition == _VerticalLabelPosition.上) ? xLabelHeight : (0d + rowHeight / 2);
            RectangleBorder.Margin = new Thickness(horizontalOffset + gridLineThickness / 2, verticalLabelOffset + gridLineThickness / 2, 0, 0);
            RectangleBorder.StrokeThickness = gridLineThickness;

            for (int i = 1; i < horizontalLineCount - 1; i++)
            {
                Line line = new Line()
                {
                    X1 = RectangleBorder.Margin.Left + RectangleBorder.StrokeThickness,
                    Y1 = RectangleBorder.Margin.Top + rowHeight * i + gridLineThickness,
                    X2 = RectangleBorder.Margin.Left + RectangleBorder.ActualWidth - RectangleBorder.StrokeThickness,
                    Y2 = RectangleBorder.Margin.Top + rowHeight * i + gridLineThickness,
                    StrokeThickness = gridLineThickness,
                    Stroke = gridBrush
                };
                Canvas.SetZIndex(line, 10);
                CanvasCore.Children.Add(line);
            }

            for (int i = 1; i < verticalLineCount - 1; i++)
            {
                Line line = new Line()
                {
                    X1 = RectangleBorder.Margin.Left + columnWidth * i + gridLineThickness,
                    Y1 = RectangleBorder.Margin.Top + RectangleBorder.StrokeThickness,
                    X2 = RectangleBorder.Margin.Left + columnWidth * i + gridLineThickness,
                    Y2 = RectangleBorder.Margin.Top + RectangleBorder.ActualHeight - RectangleBorder.StrokeThickness,
                    StrokeThickness = gridLineThickness,
                    Stroke = gridBrush
                };
                Canvas.SetZIndex(line, 10);
                CanvasCore.Children.Add(line);
            }
        }

        private void DrawBubbles()
        {
            for (int i = CanvasCore.Children.Count - 1; i >= 0; i--)
            {
                if (CanvasCore.Children[i] is Bubble)
                    CanvasCore.Children.RemoveAt(i);
            }

            double rowHeight = (gridHeight - gridLineThickness * 2) / (horizontalLineCount - 1);
            double columnWidth = (gridWidth - gridLineThickness * 2) / (verticalLineCount - 1);

            //for (int i = 0; i < verticalLineCount; i++)
            //{
            //    for (int j = 0; j < horizontalLineCount; j++)
            //    {
            //        Bubble bubble = new Bubble(j, i, "img/green.gif")
            //        {
            //            Width = bubbleRadius * 2,
            //            Height = bubbleRadius * 2,
            //            Stretch = Stretch.Fill,
            //            UseLayoutRounding = true,
            //        };

            //        Canvas.SetZIndex(bubble, 20);
            //        Canvas.SetLeft(bubble, RectangleBorder.Margin.Left + columnWidth * i - bubbleRadius + gridLineThickness);
            //        Canvas.SetTop(bubble, RectangleBorder.Margin.Top + rowHeight * j - bubbleRadius + gridLineThickness);

            //        CanvasCore.Children.Add(bubble);
            //    }
            //}

            foreach (Bubble bubble in _BubbleList)
            {
                bubble.Width = bubble.Height = bubbleRadius * 2;
                Canvas.SetZIndex(bubble, 20);
                Canvas.SetLeft(bubble, RectangleBorder.Margin.Left + columnWidth * bubble.ColumnIndex - bubbleRadius + gridLineThickness);
                Canvas.SetTop(bubble, RectangleBorder.Margin.Top + rowHeight * bubble.RowIndex - bubbleRadius + gridLineThickness);
                CanvasCore.Children.Add(bubble);
            }
        }

        private BitmapImage CreateBitmapImage(string bitmapString)
        {
            if (!File.Exists(bitmapString))
                return null;

            BinaryReader binReader = new BinaryReader(File.Open(bitmapString, FileMode.Open));
            FileInfo fileInfo = new FileInfo(bitmapString);
            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(bytes);
            bitmap.EndInit();
            return bitmap;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            gridWidth = RectangleBorder.Width = this.ActualWidth - yLabelWidth;
            gridHeight = RectangleBorder.Height = this.ActualHeight - xLabelHeight;


            RefreshUI();

        }

        List<Bubble> _BubbleList = new List<Bubble>();
        public void UpdateDataSource(DataTable dt)
        {
            _BubbleList.Clear();
            foreach (DataRow dataRow in dt.Rows)
            {
                string[] names = dataRow["name"].ToString().Split(new string[] { "_" }, StringSplitOptions.RemoveEmptyEntries);
                int rowIndex = int.Parse(names[1]);
                int columnIndex = int.Parse(names[2]);
                string value = dataRow["value"].ToString();

                if (rowIndex < horizontalLineCount && columnIndex < verticalLineCount)
                {
                    string bubbleImageSource = "";
                    switch (value)
                    {
                        case "1":
                            bubbleImageSource = bubbleState1;
                            break;
                        case "0":
                            bubbleImageSource = bubbleState2;
                            break;
                        default:
                            break;
                    }
                    _BubbleList.Add(new Bubble(rowIndex, columnIndex, bubbleImageSource));
                }
            }

            DrawBubbles();
        }
    }

    public enum _HorizontalLabelPosition
    {
        左,
        右
    }

    public enum _VerticalLabelPosition
    {
        上,
        下
    }

    public enum _XLabelVerticalAlignment
    {
        上,
        中,
        下
    }

    public enum _YLabelHorizontalAlignment
    {
        左,
        中,
        右
    }

    class Bubble : Image
    {
        public int RowIndex { get; set; }

        public int ColumnIndex { get; set; }

        public Bubble(int rowIndex, int ColumnIndex, string bubbleImageSource)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = ColumnIndex;

            this.Name = "bubble_" + rowIndex + ColumnIndex;
            this.Source = CreateBitmapImage(bubbleImageSource);
        }

        private BitmapImage CreateBitmapImage(string bitmapString)
        {
            if (!File.Exists(bitmapString))
                return null;

            BinaryReader binReader = new BinaryReader(File.Open(bitmapString, FileMode.Open));
            FileInfo fileInfo = new FileInfo(bitmapString);
            byte[] bytes = binReader.ReadBytes((int)fileInfo.Length);
            binReader.Close();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = new MemoryStream(bytes);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
