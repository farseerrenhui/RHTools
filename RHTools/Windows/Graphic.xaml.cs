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
    public partial class Graphic : Window
    {
        public Graphic()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 渐变颜色画圆
        /// </summary>
        /// <param name="O">原点</param>
        /// <param name="list">渐变颜色组(RGB)</param>
        /// <param name="lineWidth"></param>
        /// <param name="count">密度(即线条数)</param>
        /// <param name="len">圆半径(像素)</param>
        /// <param name="totleAngle">区域度数</param>
        /// <param name="startAngle">开始度数</param>
        private void DrawingRound(Point O, List<int[]> list, double lineWidth, double count, int len, int totleAngle, int startAngle)
        {
            int lscount = list.Count - 1;

            for (int i = 0; i < count; i++)
            {
                if (count > list.Count * 2)
                {
                    int divisor = (int)((i / (count / lscount)) - (i / (count / lscount)) % 1);
                    int remainder = (int)(i % (count / lscount));

                    int[] a1 = list[divisor];
                    int[] a2 = list[divisor + 1];

                    int[] rgb = new int[3] { a1[0] + (a2[0] - a1[0]) * remainder / ((int)count / lscount), a1[1] + (a2[1] - a1[1]) * remainder / ((int)count / lscount), a1[2] + (a2[2] - a1[2]) * remainder / ((int)count / lscount) };

                    //颜色
                    SolidColorBrush scb = new SolidColorBrush(Color.FromRgb((byte)rgb[0], (byte)rgb[1], (byte)rgb[2]));

                    Path myPath = new Path();
                    myPath.Stroke = scb;
                    myPath.StrokeThickness = lineWidth;

                    LineGeometry line = new LineGeometry();
                    line.StartPoint = new Point(O.X + len * Math.Sin(Math.PI * ((totleAngle * (i / count) + startAngle) / 180 + startAngle))
                      , O.Y + len * Math.Cos(Math.PI * ((totleAngle * (i / count) + startAngle) / 180)));
                    line.EndPoint = O;

                    myPath.Data = line;

                    canvas.Children.Add(myPath);
                }
                else
                {
                    //颜色
                    SolidColorBrush scb = new SolidColorBrush(Color.FromRgb(0, 0, 0));

                    Path myPath = new Path();
                    myPath.Stroke = scb;
                    myPath.StrokeThickness = lineWidth;

                    LineGeometry line = new LineGeometry();
                    line.StartPoint = new Point(350 + len * Math.Sin(Math.PI * (totleAngle * (i / count) / 180 + startAngle)), 350 + len * Math.Cos(Math.PI * (totleAngle * (i / count) / 180 + startAngle)));
                    line.EndPoint = O;

                    myPath.Data = line;

                    canvas.Children.Add(myPath);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            List<int[]> list = new List<int[]>();
            list.Add(new int[3] { 255, 0, 0 });
            list.Add(new int[3] { 0, 255, 0 });
            Point point = new Point(350, 350);
            double count = 1500;
            DrawingRound(point, list, 2, count, 300, 360, 90);

            //遮盖了一个白色圆形
            //List<int[]> list2 = new List<int[]>();
            //list2.Add(new int[3] { 255, 255, 255 });
            //list2.Add(new int[3] { 255, 255, 255 });
            //DrawingRound(point, list2, 2, count, 260, 360, 90);
        }
    }
}
