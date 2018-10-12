using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Forms;

namespace RHTools
{
    public partial class HttpDownload : Window
    {
        public HttpDownload()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sdf = new SaveFileDialog();
            if (sdf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (DownloadFile(TextBoxUrl.Text, sdf.FileName))
                    System.Windows.MessageBox.Show("Download Finish!");
                else
                    System.Windows.MessageBox.Show("Download Failed!");
            }
        }

        public bool DownloadFile(string url, string path)
        {
            string tempPath = Path.GetDirectoryName(path) + @"\temp";
            Directory.CreateDirectory(tempPath);  //创建临时文件目录
            string tempFile = tempPath + @"\" + Path.GetFileName(path) + ".temp"; //临时文件
            if (File.Exists(tempFile))
            {
                File.Delete(tempFile);    //存在则删除
            }
            try
            {
                FileStream fs = new FileStream(tempFile, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                // 设置参数
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //发送请求并获取相应回应数据
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //直到request.GetResponse()程序才开始向目标网页发送Post请求
                Stream responseStream = response.GetResponseStream();
                //创建本地文件写入流
                //Stream stream = new FileStream(tempFile, FileMode.Create);
                byte[] bArr = new byte[1024];
                int size = responseStream.Read(bArr, 0, (int)bArr.Length);
                while (size > 0)
                {
                    //stream.Write(bArr, 0, size);
                    fs.Write(bArr, 0, size);
                    size = responseStream.Read(bArr, 0, (int)bArr.Length);
                }
                //stream.Close();
                fs.Close();
                responseStream.Close();
                File.Move(tempFile, path);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
