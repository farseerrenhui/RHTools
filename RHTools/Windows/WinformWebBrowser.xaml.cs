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
    public partial class WinformWebBrowser : Window
    {
        string html;

        public WinformWebBrowser()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            html = "<!DOCTYPE html><html lang=\"zh-cn\"><head><meta charset=\"utf-8\"/>" +
                "<title></title>" +
                "<body>" +
                "<P>AAA</P>" +
                "<P>AAA</P>" +
                "<P>AAA</P>" +
                "<div id=\"HiddenDIV\" style=\"visibility:hidden;display:none;\">隐藏的一些内容用于流程处理</div>" +
                "<P>AAA</P>" +
                "<P>AAA</P>" +
                "<P>AAA</P>" +
                "</body>" +
                "</html>";
            WFWebBrowser.DocumentText = html;
        }

        private void Button_Click_DesignModeOn(object sender, RoutedEventArgs e)
        {
            mshtml.IHTMLDocument2 doc = WFWebBrowser.Document.DomDocument as mshtml.IHTMLDocument2;
            doc.designMode = "on";
            Console.WriteLine(html);
        }

        private void Button_Click_DesignModeOff(object sender, RoutedEventArgs e)
        {
            mshtml.IHTMLDocument2 doc = WFWebBrowser.Document.DomDocument as mshtml.IHTMLDocument2;
            doc.designMode = "off";
            Console.WriteLine(html);
        }
    }
}
