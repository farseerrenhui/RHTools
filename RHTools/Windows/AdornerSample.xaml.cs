using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace RHTools
{
    public partial class AdornerSample : Window
    {
        public AdornerSample()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            AdornerLayer layerLabel = AdornerLayer.GetAdornerLayer(label1);
            layerLabel.Add(new ScaleAdorner(label1));
        }
    }

    class ScaleAdorner : Adorner
    {
        private Label _adornedElement;

        public ScaleAdorner(Label andornerElement) : base(andornerElement)
        {
            _adornedElement = andornerElement;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            Pen renderPen = new Pen(Brushes.Navy, 1);
            renderPen.DashStyle = new DashStyle(new double[] { 2.5, 2.5 }, 0);

            Rect rect = new Rect(0, 0, _adornedElement.ActualWidth, _adornedElement.ActualHeight);
            drawingContext.DrawRectangle(Brushes.Transparent, renderPen, rect);

            base.OnRender(drawingContext);
        }
    }

}
