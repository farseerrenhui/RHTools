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
using System.Xml;

namespace RHTools
{
    public partial class XML2JSON : Window
    {
        public XML2JSON()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(TextBoxSource.Text);

            string jsonText = Newtonsoft.Json.JsonConvert.SerializeXmlNode(doc);
            TextBoxTarget.Text = jsonText;
        }
    }
}
