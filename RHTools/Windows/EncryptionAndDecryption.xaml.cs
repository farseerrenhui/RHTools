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
    public partial class EncryptionAndDecryption : Window
    {
        public EncryptionAndDecryption()
        {
            InitializeComponent();
        }

        private void Button_Click_Encryption(object sender, RoutedEventArgs e)
        {
            tb1_result.Text = Encrypt(tb1.Text);
        }

        private void Button_Click_Decryption(object sender, RoutedEventArgs e)
        {
            try
            {
                tb2_result.Text = Decrypt(tb2.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("解密失败!");
            }
        }

        #region 字符串加密解密  

        //定义密钥,4位有效,其他位数未验证
        static string encryptKey = "syxp";

        /// <summary>
        /// 加密字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Encrypt(string str)
        {
            byte[] by = System.Text.Encoding.Default.GetBytes(str);
            //把二进制BYTE数组转换成base64字符串  
            string base64 = Convert.ToBase64String(by);
            if (base64.IndexOf("=") < 0)
            {
                by = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str);
                base64 = Convert.ToBase64String(by);
            }
            return base64;
        }

        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="base64Str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        public static string Decrypt(string base64Str)
        {
            try
            {
                byte[] by = Convert.FromBase64String(base64Str);
                return System.Text.Encoding.Default.GetString(by);
            }
            catch (Exception)
            {
                byte[] by = Convert.FromBase64String(base64Str);
                return System.Text.Encoding.GetEncoding("GB2312").GetString(by);
            }
        }

        #endregion

    }
}
