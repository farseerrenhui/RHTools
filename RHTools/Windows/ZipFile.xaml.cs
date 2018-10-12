using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace RHTools
{
    public partial class ZipFile : Window
    {
        public ZipFile()
        {
            InitializeComponent();
        }

        private void Button_Click_Compress(object sender, RoutedEventArgs e)
        {
            CompressZipFileClass CZ = new CompressZipFileClass();
            CZ.ZipFileFromDirectory(TextBox1.Text.Trim(), TextBox2.Text.Trim(), 0);
        }

        private void Button_Click_Extract(object sender, RoutedEventArgs e)
        {
            ExtractZipFileClass EZ = new ExtractZipFileClass();
            EZ.unZipFile2(TextBox3.Text.Trim(), TextBox4.Text.Trim());
        }

        private void Button_Click_Copy(object sender, RoutedEventArgs e)
        {
            CopyDirectory(TextBox5.Text.Trim(), TextBox6.Text.Trim());
        }

        /// <summary>
        /// 文件夹拷贝
        /// </summary>
        /// <param name="srcdir"></param>
        /// <param name="desdir"></param>
        private void CopyDirectory(string srcdir, string desdir)
        {
            string folderName = srcdir.Substring(srcdir.LastIndexOf("\\") + 1);
            string desfolderdir = desdir + "\\" + folderName;

            if (desdir.LastIndexOf("\\") == (desdir.Length - 1))
            {
                desfolderdir = desdir + folderName;
            }
            string[] filenames = Directory.GetFileSystemEntries(srcdir);

            foreach (string file in filenames)// 遍历所有的文件和目录
            {
                if (Directory.Exists(file))// 先当作目录处理如果存在这个目录就递归Copy该目录下面的文件
                {
                    string currentdir = desfolderdir + "\\" + file.Substring(file.LastIndexOf("\\") + 1);
                    if (!Directory.Exists(currentdir))
                    {
                        Directory.CreateDirectory(currentdir);
                    }

                    CopyDirectory(file, desfolderdir);
                }
                else // 否则直接copy文件
                {
                    string srcfileName = file.Substring(file.LastIndexOf("\\") + 1);
                    srcfileName = desfolderdir + "\\" + srcfileName;

                    if (!Directory.Exists(desfolderdir))
                    {
                        Directory.CreateDirectory(desfolderdir);
                    }

                    //File.Copy(file, srcfileName);
                    FileInfo fi = new FileInfo(file);
                    fi.CopyTo(srcfileName);
                }
            }//foreach 
        }//function end
    }

    /// <summary>
    /// 文件压缩类
    /// </summary>
    public class CompressZipFileClass
    {
        /// <summary>
        /// 所有文件缓存
        /// </summary>        
        List<string> files = new List<string>();

        /// <summary>
        /// 所有空目录缓存
        /// </summary>        
        List<string> paths = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootPath">要压缩的根目录</param>  
        /// <param name="destinationPath">保存路径</param>  
        /// <param name="compressLevel">压缩程度，范围0-9，数值越大，压缩程序越高</param>  
        public void ZipFileFromDirectory(string rootPath, string destinationPath, int compressLevel)
        {
            GetAllDirectories(rootPath);

            string rootMark = rootPath + "\\";//得到当前路径的位置，以备压缩时将所压缩内容转变成相对路径。  
            Crc32 crc = new Crc32();
            ZipOutputStream outPutStream = new ZipOutputStream(File.Create(destinationPath));
            outPutStream.SetLevel(compressLevel);
            foreach (string file in files)
            {
                FileStream fileStream = File.OpenRead(file);//打开压缩文件  
                byte[] buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, buffer.Length);
                ZipEntry entry = new ZipEntry(file.Replace(rootMark, string.Empty));
                entry.DateTime = DateTime.Now;
                entry.Size = fileStream.Length;
                fileStream.Close();
                crc.Reset();
                crc.Update(buffer);
                entry.Crc = crc.Value;
                outPutStream.PutNextEntry(entry);
                outPutStream.Write(buffer, 0, buffer.Length);
            }

            this.files.Clear();

            foreach (string emptyPath in paths)
            {
                ZipEntry entry = new ZipEntry(emptyPath.Replace(rootMark, string.Empty) + "/");
                outPutStream.PutNextEntry(entry);
            }

            this.paths.Clear();
            outPutStream.Finish();
            outPutStream.Close();
            GC.Collect();
        }

        /// <summary>
        /// 取得目录下所有文件及文件夹，分别存入files及paths  
        /// </summary>
        /// <param name="rootPath">根目录</param>  
        private void GetAllDirectories(string rootPath)
        {
            string[] subPaths = Directory.GetDirectories(rootPath);//得到所有子目录  
            foreach (string path in subPaths)
            {
                GetAllDirectories(path);//对每一个字目录做与根目录相同的操作：即找到子目录并将当前目录的文件名存入List  
            }
            string[] files = Directory.GetFiles(rootPath);
            foreach (string file in files)
            {
                this.files.Add(file);//将当前目录中的所有文件全名存入文件List  
            }
            if (subPaths.Length == files.Length && files.Length == 0)//如果是空目录  
            {
                this.paths.Add(rootPath);//记录空目录  
            }
        }
    }

    public class ExtractZipFileClass
    {

        /// <summary>
        /// 从把第一个参数的文件(*.zip),解压到第二个参数指定的目录
        /// </summary>
        /// <param name="TargetFile">绝对路径:待解压文件(F://temp.zip)</param>
        /// <param name="fileDir">绝对路径:解压后路径(H://temp),即设定文件解压的路径</param>
        /// <param name="zipname">绝对路径:判断这个路径下有没有与第二个参数相同的目录</param>
        /// <returns></returns>
        public bool unZipFile(string TargetFile, string fileDir)
        {
            string rootFile = " ";
            try
            {
                //string zipname1 = zipname.Substring(0, zipname.IndexOf("."));
                string pathEnd = fileDir;
                //+ "\\" + System.IO.Path.GetFileName(zipname1);
                DirectoryInfo dir1 = new DirectoryInfo(fileDir);
                if (dir1.Exists)//存在
                {


                    if (Directory.GetDirectories(fileDir).Length > 0)
                    {
                        foreach (string var in Directory.GetDirectories(fileDir))
                        {

                            foreach (string var1 in Directory.GetFiles(var))
                            {
                                File.Delete(var1);
                            }

                            Directory.Delete(var, true);

                        }
                    }
                    if (Directory.GetFiles(fileDir).Length > 0)
                    {
                        foreach (string var in Directory.GetFiles(fileDir))
                        {
                            File.Delete(var);
                        }
                    }


                }

                //读取压缩文件(zip文件)，准备解压缩
                ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
                ZipEntry theEntry;
                string path = "";
                //解压出来的文件保存的路径

                string rootDir = " ";
                //根目录下的第一个子文件夹的名称
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    rootDir = System.IO.Path.GetDirectoryName(theEntry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf("\\") >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                    }
                    string dir = System.IO.Path.GetDirectoryName(theEntry.Name);
                    //根目录下的第一级子文件夹的下的文件夹的名称
                    string fileName = System.IO.Path.GetFileName(theEntry.Name);
                    //根目录下的文件名称
                    if (dir != " ")
                    //创建根目录下的子文件夹,不限制级别
                    {
                        if (!Directory.Exists(fileDir + "\\" + dir))
                        {
                            path = fileDir + "\\" + dir;
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == " " && fileName != "")
                    //根目录下的文件
                    {
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != " " && fileName != "")
                    //根目录下的第一级子文件夹下的文件
                    {
                        if (dir.IndexOf("\\") > 0)
                        //指定文件保存的路径
                        {
                            path = fileDir + "\\" + dir;
                        }
                    }

                    if (dir == rootDir)
                    //判断是不是需要保存在根目录下的文件
                    {
                        path = fileDir + "\\" + rootDir;
                    }

                    //以下为解压缩zip文件的基本步骤
                    //基本思路就是遍历压缩文件里的所有文件，创建一个相同的文件。
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(path + "\\" + fileName);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                }
                s.Close();

                return true;
            }

            catch (Exception ex)
            {
                //LogHelper.WriteError(typeof(ZipFileClass), ex);
                return false;
            }


        }

        /// <summary>
        /// 不清空保存目录
        /// </summary>
        /// <param name="TargetFile"></param>
        /// <param name="fileDir"></param>
        /// <returns></returns>

        public bool unZipFile2(string TargetFile, string fileDir)
        {
            string rootFile = " ";
            try
            {
                //string zipname1 = zipname.Substring(0, zipname.IndexOf("."));
                string pathEnd = fileDir;
                //+ "\\" + System.IO.Path.GetFileName(zipname1);
                DirectoryInfo dir1 = new DirectoryInfo(fileDir);


                //读取压缩文件(zip文件)，准备解压缩
                ZipInputStream s = new ZipInputStream(File.OpenRead(TargetFile.Trim()));
                ZipEntry theEntry;
                string path = "";
                //解压出来的文件保存的路径

                string rootDir = " ";
                //根目录下的第一个子文件夹的名称
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    rootDir = System.IO.Path.GetDirectoryName(theEntry.Name);
                    //得到根目录下的第一级子文件夹的名称
                    if (rootDir.IndexOf("\\") >= 0)
                    {
                        rootDir = rootDir.Substring(0, rootDir.IndexOf("\\") + 1);
                    }
                    string dir = System.IO.Path.GetDirectoryName(theEntry.Name);
                    //根目录下的第一级子文件夹的下的文件夹的名称
                    string fileName = System.IO.Path.GetFileName(theEntry.Name);
                    //根目录下的文件名称
                    if (dir != " ")
                    //创建根目录下的子文件夹,不限制级别
                    {
                        if (!Directory.Exists(fileDir + "\\" + dir))
                        {
                            path = fileDir + "\\" + dir;
                            //在指定的路径创建文件夹
                            Directory.CreateDirectory(path);
                        }
                    }
                    else if (dir == " " && fileName != "")
                    //根目录下的文件
                    {
                        path = fileDir;
                        rootFile = fileName;
                    }
                    else if (dir != " " && fileName != "")
                    //根目录下的第一级子文件夹下的文件
                    {
                        if (dir.IndexOf("\\") > 0)
                        //指定文件保存的路径
                        {
                            path = fileDir + "\\" + dir;
                        }
                    }

                    if (dir == rootDir)
                    //判断是不是需要保存在根目录下的文件
                    {
                        path = fileDir + "\\" + rootDir;
                    }

                    //以下为解压缩zip文件的基本步骤
                    //基本思路就是遍历压缩文件里的所有文件，创建一个相同的文件。
                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(path + "\\" + fileName);

                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                streamWriter.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }

                        streamWriter.Close();
                    }
                }
                s.Close();

                return true;
            }

            catch (Exception ex)
            {
                //LogHelper.WriteError(typeof(ZipFileClass), ex);
                return false;
            }


        }

    }

}
