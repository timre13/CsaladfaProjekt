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

namespace Csaladfa
{
    class Person
    {
        public string FirstName { get; private set; } = "";
        public string LastName { get; private set; } = "";

        public void Draw()
        {
            var rect = new Polygon();
            rect.Fill = Brushes.Red;
            rect.Stroke = new LinearGradientBrush(Colors.Green, Colors.Blue, 10);
            rect.StrokeThickness = 5;
            rect.Points.Add(new Point(100, 100));
            rect.Points.Add(new Point(300, 100));
            rect.Points.Add(new Point(300, 300));
            rect.Points.Add(new Point(100, 300));
            //canvas.Children.Add(rect);
        }
    }

    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Person person = new Person();
            person.Draw();
        }
    }
}
