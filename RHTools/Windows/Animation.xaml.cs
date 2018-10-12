using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace RHTools
{
    public partial class Animation : Window
    {
        public Animation()
        {
            InitializeComponent();
        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            DoubleAnimationUsingKeyFrames dakX = new DoubleAnimationUsingKeyFrames();

            //创建、添加关键帧
            KeySpline ks = new KeySpline() { ControlPoint1 = new Point(0, 1), ControlPoint2 = new Point(1, 0) };
            SplineDoubleKeyFrame kf = new SplineDoubleKeyFrame() { KeyTime = KeyTime.FromPercent(1), Value = 200, KeySpline = ks };
            dakX.KeyFrames.Add(kf);

            //执行动画
            this.tt.BeginAnimation(TranslateTransform.XProperty, dakX);
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            DoubleAnimation daX = new DoubleAnimation();
            DoubleAnimation daY = new DoubleAnimation();

            //指定起点
            //daX.From = 0;
            //daY.From = 0;

            //指定终点(指定幅度时,不能指定终点,否则幅度无效)
            //Random r = new Random();
            //daX.To = r.NextDouble() * 300;
            //daY.To = r.NextDouble() * 300;

            //指定幅度
            daX.By = 100;
            daY.By = 100;

            //指定时长
            Duration duration = new Duration(TimeSpan.FromMilliseconds(300));
            daX.Duration = daY.Duration = duration;

            //动画的主题是translateTransform变形,而非Button
            this.tt2.BeginAnimation(TranslateTransform.XProperty, daX);
            this.tt2.BeginAnimation(TranslateTransform.YProperty, daY);
        }

        private void Button_Click3(object sender, RoutedEventArgs e)
        {
            DoubleAnimation daX = new DoubleAnimation();
            DoubleAnimation daY = new DoubleAnimation();

            //设置反弹
            BounceEase be = new BounceEase();
            be.Bounces = 3;
            be.Bounciness = 2;
            daY.EasingFunction = be;

            //指定终点
            daX.To = 1000;
            daY.To = 600;

            //指定时长
            Duration duration = new Duration(TimeSpan.FromMilliseconds(2000));
            daX.Duration = daY.Duration = duration;

            //动画的主题是translateTransform变形,而非Button
            this.tt3.BeginAnimation(TranslateTransform.XProperty, daX);
            this.tt3.BeginAnimation(TranslateTransform.YProperty, daY);
        }

        private void Button_Click4(object sender, RoutedEventArgs e)
        {
            DoubleAnimationUsingKeyFrames dakX = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames dakY = new DoubleAnimationUsingKeyFrames();

            //设置动画总时长
            dakX.Duration = new Duration(TimeSpan.FromMilliseconds(900));
            dakY.Duration = new Duration(TimeSpan.FromMilliseconds(900));

            //创建、添加关键帧
            LinearDoubleKeyFrame x_kf_1 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame x_kf_2 = new LinearDoubleKeyFrame();
            LinearDoubleKeyFrame x_kf_3 = new LinearDoubleKeyFrame();

            x_kf_1.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300));
            x_kf_1.Value = 200;
            x_kf_2.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(600));
            x_kf_2.Value = 0;
            x_kf_3.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(900));
            x_kf_3.Value = 200;
            dakX.KeyFrames.Add(x_kf_1);
            dakX.KeyFrames.Add(x_kf_2);
            dakX.KeyFrames.Add(x_kf_3);

            dakY.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(300)), Value = 0 });
            dakY.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(600)), Value = 180 });
            dakY.KeyFrames.Add(new LinearDoubleKeyFrame() { KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(900)), Value = 180 });

            //执行动画
            this.tt4.BeginAnimation(TranslateTransform.XProperty, dakX);
            this.tt4.BeginAnimation(TranslateTransform.YProperty, dakY);
        }

        private void Button_Click_EventTriggerButton(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show("只是一个普通的Button.");
        }
    }
}
