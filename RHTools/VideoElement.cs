using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace RHTools
{
    public class VideoElement : MediaElement
    {
        public VideoElement()
        {
            
        }

        private bool autoRestart = true;
        public bool AutoRestart
        {
            get { return autoRestart; }
            set { autoRestart = value; }
        }

        private string filePath = "";
        public string FilePath
        {
            get { return filePath; }
            set
            {
                filePath = value;
                this.Source = new Uri(value);
            }
        }
    }
}
