using Microsoft.Win32;
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
    public partial class WindowMediaElementTest : Window
    {
        public WindowMediaElementTest()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void OnMouseDownBrowse(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                myMediaElement.Source = new Uri(ofd.FileName);
            }
            
        }

        // Play the media.
        void OnMouseDownPlayMedia(object sender, RoutedEventArgs args)
        {

            // The Play method will begin the media if it is not currently active or 
            // resume media if it is paused. This has no effect if the media is
            // already running.
            myMediaElement.Play();

            // Initialize the MediaElement property values.
            InitializePropertyValues();

        }

        // Pause the media.
        void OnMouseDownPauseMedia(object sender, RoutedEventArgs args)
        {

            // The Pause method pauses the media if it is currently running.
            // The Play method can be used to resume.
            myMediaElement.Pause();

        }

        // Stop the media.
        void OnMouseDownStopMedia(object sender, RoutedEventArgs args)
        {

            // The Stop method stops and resets the media to be played from
            // the beginning.
            myMediaElement.Stop();

        }

        // Change the volume of the media.
        private void ChangeMediaVolume(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.Volume = (double)volumeSlider.Value;
        }

        // Change the speed of the media.
        private void ChangeMediaSpeedRatio(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            myMediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }

        // When the media opens, initialize the "Seek To" slider maximum value
        // to the total number of miliseconds in the length of the media clip.
        private void Element_MediaOpened(object sender, EventArgs e)
        {
            timelineSlider.Maximum = myMediaElement.NaturalDuration.TimeSpan.TotalMilliseconds;
        }

        // When the media playback is finished. Stop() the media to seek to media start.
        private void Element_MediaEnded(object sender, EventArgs e)
        {
            myMediaElement.Stop();
        }

        // Jump to different parts of the media (seek to). 
        private void SeekToMediaPosition(object sender, RoutedPropertyChangedEventArgs<double> args)
        {
            int SliderValue = (int)timelineSlider.Value;

            // Overloaded constructor takes the arguments days, hours, minutes, seconds, miniseconds.
            // Create a TimeSpan with miliseconds equal to the slider value.
            TimeSpan ts = new TimeSpan(0, 0, 0, 0, SliderValue);
            myMediaElement.Position = ts;
        }

        void InitializePropertyValues()
        {
            // Set the media's starting Volume and SpeedRatio to the current value of the
            // their respective slider controls.
            myMediaElement.Volume = (double)volumeSlider.Value;
            myMediaElement.SpeedRatio = (double)speedRatioSlider.Value;
        }

        private void Button_Click_SET(object sender, RoutedEventArgs e)
        {
            Grid1.Width = int.Parse(TextBoxWidth.Text); 
            Grid1.Height = int.Parse(TextBoxHeight.Text); 
        }
    }
}
