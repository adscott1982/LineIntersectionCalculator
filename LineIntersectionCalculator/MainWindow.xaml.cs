using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LineIntersectionCalculator
{
    using System.Text.RegularExpressions;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private static bool IsTextAllowed(string text)
        {
            Regex regex = new Regex("[^0-9.-]+"); //regex that matches disallowed text
            return !regex.IsMatch(text);
        }

        private void TextBoxCheckIsNumber(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            try
            {
                var line1Start = new Point(double.Parse(this.Line1StartX.Text), double.Parse(this.Line1StartY.Text));
                var line1End = new Point(double.Parse(this.Line1EndX.Text), double.Parse(this.Line1EndY.Text));
                var line2Start = new Point(double.Parse(this.Line2StartX.Text), double.Parse(this.Line2StartY.Text));
                var line2End = new Point(double.Parse(this.Line2EndX.Text), double.Parse(this.Line2EndY.Text));

                var intersection = new Intersection(line1Start, line1End, line2Start, line2End);

                this.ResultX.Content = "X = " + intersection.IntersectionPoint.X;
                this.ResultY.Content = "Y = " + intersection.IntersectionPoint.Y;
            }
            catch
            {
                MessageBox.Show("Invalid input");
            }
        }
    }
}
