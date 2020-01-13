using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;

namespace RHTools
{
    public partial class ImageCutter : Window
    {
        public ImageCutter()
        {
            InitializeComponent();
        }

        private void ButtonReplace_Clear(object sender, RoutedEventArgs e)
        {
            ListBoxFiles.Items.Clear();
        }

        private void ListBoxFiles_Drop(object sender, DragEventArgs e)
        {
            string[] s = (string[])e.Data.GetData(DataFormats.FileDrop, false);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i].ToLower().EndsWith(".png") || s[i].ToLower().EndsWith(".jpg") || s[i].ToLower().EndsWith(".bmp"))
                    (sender as System.Windows.Controls.ListBox).Items.Add(s[i]);
            }
        }

        string transFolder;

        private void ButtonReplace_Cut(object sender, RoutedEventArgs e)
        {
            int buttonHeight = int.Parse(TextBoxBottomHeight.Text);

            foreach (String item in ListBoxFiles.Items)
            {
                FileInfo file = new FileInfo(item);

                Bitmap img = new Bitmap(file.FullName);
                Image newImage = CutOffBottom(img, buttonHeight);

                transFolder = file.Directory + "\\trans\\";
                Directory.CreateDirectory(transFolder);

                newImage.Save(transFolder + file.Name, ImageFormat.Jpeg);
                newImage.Dispose();
            }

            MessageBox.Show("Cut Finish");
            System.Diagnostics.Process.Start("explorer", transFolder);
        }

        Image CutOffBottom(Bitmap img, int cutHeight)
        {
            int newHeight = img.Height - cutHeight;
            Bitmap newImage = new Bitmap(img.Width, newHeight);

            Graphics g = Graphics.FromImage(newImage);
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
            g.DrawImage(img, new Rectangle(0, 0, img.Width, newHeight), new Rectangle(0, 0, img.Width, newHeight), GraphicsUnit.Pixel);
            g.Dispose();

            return newImage;
        }

        string brightFolder;
        int dgree;

        private void ButtonReplace_Bright(object sender, RoutedEventArgs e)
        {
            dgree = int.Parse(TextBoxDegree.Text);

            foreach (String item in ListBoxFiles.Items)
            {
                FileInfo file = new FileInfo(item);
                Bitmap img = new Bitmap(file.FullName);

                Bitmap newImage = BrightnessP(KiContrast(img, dgree), dgree);

                brightFolder = file.Directory + "\\bright\\";
                Directory.CreateDirectory(brightFolder);

                newImage.Save(brightFolder + file.Name, ImageFormat.Jpeg);
                newImage.Dispose();
            }

            MessageBox.Show("Bright Finish");
            System.Diagnostics.Process.Start("explorer", brightFolder);
        }

        /// <summary>
        /// 图像对比度调整
        /// </summary>
        /// <param name="b">原始图</param>
        /// <param name="degree">对比度[-100, 100]</param>
        /// <returns></returns>
        public static Bitmap KiContrast(Bitmap b, int degree)
        {
            if (b == null)
            {
                return null;
            }

            if (degree < -100) degree = -100;
            if (degree > 100) degree = 100;

            try
            {
                double pixel = 0;
                double contrast = (100.0 + degree) / 100.0;
                contrast *= contrast;
                int width = b.Width;
                int height = b.Height;
                BitmapData data = b.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* p = (byte*)data.Scan0;
                    int offset = data.Stride - width * 3;
                    for (int y = 0; y < height; y++)
                    {
                        for (int x = 0; x < width; x++)
                        {
                            // 处理指定位置像素的对比度
                            for (int i = 0; i < 3; i++)
                            {
                                pixel = ((p[i] / 255.0 - 0.5) * contrast + 0.5) * 255;
                                if (pixel < 0) pixel = 0;
                                if (pixel > 255) pixel = 255;
                                p[i] = (byte)pixel;
                            } // i
                            p += 3;
                        } // x
                        p += offset;
                    } // y
                }
                b.UnlockBits(data);
                return b;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 亮度
        /// </summary>
        /// <param name="a"></param>
        /// <param name="v"></param>
        /// <returns></returns>
        private static Bitmap BrightnessP(Bitmap a, int v)
        {
            BitmapData bmpData = a.LockBits(new Rectangle(0, 0, a.Width, a.Height), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
            int bytes = a.Width * a.Height * 3;
            IntPtr ptr = bmpData.Scan0;
            int stride = bmpData.Stride;
            unsafe
            {
                byte* p = (byte*)ptr;
                int temp;
                for (int j = 0; j < a.Height; j++)
                {
                    for (int i = 0; i < a.Width * 3; i++, p++)
                    {
                        temp = (int)(p[0] + v);
                        temp = (temp > 255) ? 255 : temp < 0 ? 0 : temp;
                        p[0] = (byte)temp;
                    }
                    p += stride - a.Width * 3;
                }
            }
            a.UnlockBits(bmpData);
            return a;
        }
    }
}
