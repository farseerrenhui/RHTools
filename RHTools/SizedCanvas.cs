using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace RHTools
{
    public class SizedCanvas : Canvas
    {
        //改写FrameworkElement.MeasureOverride
        protected override Size MeasureOverride(Size constraint)
        {
            var infinite = new Size(double.PositiveInfinity, double.PositiveInfinity);
            var maxSize = new Size();
            foreach (UIElement ele in InternalChildren)
                if (ele != null)
                {
                    ele.Measure(infinite);
                    SetMaxSize(ref maxSize, ele, constraint);
                }
            return maxSize;
        }

        //通过判断Canvas的附加属性和控件的请求大小来设置最大值
        void SetMaxSize(ref Size size, UIElement ele, Size available)
        {
            double left, top, right, bottom, width, height;
            left = Canvas.GetLeft(ele);
            top = Canvas.GetTop(ele);
            right = Canvas.GetRight(ele);
            bottom = Canvas.GetBottom(ele);
            width = ele.DesiredSize.Width;
            height = ele.DesiredSize.Height;

            if (!Double.IsNaN(left))
                width += left;
            if (!Double.IsNaN(top))
                height += top;
            if (!Double.IsNaN(right) && !Double.IsPositiveInfinity(available.Width))
                width = available.Width - right;
            if (!Double.IsNaN(bottom) && !Double.IsPositiveInfinity(available.Height))
                height = available.Height - bottom;

            size.Width = Math.Max(size.Width, width);
            size.Height = Math.Max(size.Height, height);

        }
    }
}
