using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RHTools
{
    public partial class PictureAddLogo : Window
    {
        Bitmap bitmap;
        Graphics graphics;

        public PictureAddLogo()
        {
            InitializeComponent();
        }

        private void Button_Click_SelectPicture(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Filter = "JPG(*.jpg)|*.jpg|BMP(*.bmp)|*.bmp|PNG(*.png)|*.png|All files(*.*)|*.*",
            };
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                bitmap = new Bitmap(openFileDialog.FileName);
                graphics = Graphics.FromImage(bitmap);
                imageTarget.Source = new BitmapImage(new Uri(openFileDialog.FileName, UriKind.Absolute));
            }
        }

        private void Button_Click_WriteTextToImage(object sender, RoutedEventArgs e)
        {
            String str = TextBoxText.Text;
            Font font = new Font("Consolas", 18);
            SolidBrush sbrush = new SolidBrush(Color.Black);
            graphics.DrawString(str, font, sbrush, new PointF(30, 10));
            MemoryStream ms = new MemoryStream();
            bitmap.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);

            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Filter = "JPG(*.jpg)|*.jpg|BMP(*.bmp)|*.bmp|PNG(*.png)|*.png|All files(*.*)|*.*",
            };
            if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                FileStream fs = new FileStream(saveFileDialog.FileName, FileMode.OpenOrCreate);
                BinaryWriter w = new BinaryWriter(fs);
                w.Write(ms.ToArray());
                fs.Close();
                ms.Close();

                string path = saveFileDialog.FileName.Substring(0, saveFileDialog.FileName.LastIndexOf("\\"));
                if (Directory.Exists(path))
                    System.Diagnostics.Process.Start("explorer", path);
            }
        }
    }
}
