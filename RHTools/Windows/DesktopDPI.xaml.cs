using System;
using System.Management;
using System.Windows;

namespace RHTools
{
    public partial class DesktopDPI : Window
    {
        public DesktopDPI()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (ManagementClass mc = new ManagementClass("Win32_DesktopMonitor"))
            {
                using (ManagementObjectCollection moc = mc.GetInstances())
                {
                    int PixelsPerXLogicalInch = 0;  // dpi for x
                    int PixelsPerYLogicalInch = 0;  // dpi for y
                    foreach (ManagementObject each in moc)
                    {
                        PixelsPerXLogicalInch = int.Parse((each.Properties["PixelsPerXLogicalInch"].Value.ToString()));
                        PixelsPerYLogicalInch = int.Parse((each.Properties["PixelsPerYLogicalInch"].Value.ToString()));
                    }

                    TextBlockDPI.Text += ("PixelsPerXLogicalInch:" + PixelsPerXLogicalInch.ToString());
                    TextBlockDPI.Text += ("PixelsPerYLogicalInch:" + PixelsPerYLogicalInch.ToString());
                }
            }
        }
    }
}
