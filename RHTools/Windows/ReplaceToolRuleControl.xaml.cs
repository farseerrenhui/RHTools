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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RHTools
{
    public partial class ReplaceToolRuleControl : UserControl
    {
        public ReplaceToolRuleControl()
        {
            InitializeComponent();
        }

        public string From
        {
            get { return TextBoxFrom.Text; }
            set { TextBoxFrom.Text = value; }
        }

        public string To
        {
            get { return TextBoxTo.Text; }
            set { TextBoxTo.Text = value; }
        }
    }
}
