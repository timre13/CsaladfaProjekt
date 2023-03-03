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
        public string? FirstName = "";
        public string? LastName = "";
        public GenderEnum? Gender;

        public enum GenderEnum
        {
            Male, Female, Other
        }

        private static Brush GenderToBrush(GenderEnum? gender)
        {
            if (gender == null)
                return Brushes.Gray;
            switch (gender)
            {
            case GenderEnum.Male:
                return Brushes.LightBlue;
            case GenderEnum.Female:
                return Brushes.LightPink;
            case GenderEnum.Other:
            default:
                return Brushes.LightYellow;
            }
        }

        public static int PERSON_RECT_W = 250;
        public static int PERSON_RECT_H = 100;

        public void Draw(Canvas canvas, int x, int y)
        {
            var rect = new Polygon();
            rect.Fill = GenderToBrush(Gender);
            rect.Stroke = new LinearGradientBrush(Colors.Green, Colors.Blue, 10);
            rect.StrokeThickness = 5;
            rect.Points.Add(new Point(x - MainWindow.CanvasPanX, y - MainWindow.CanvasPanY));
            rect.Points.Add(new Point(x - MainWindow.CanvasPanX + PERSON_RECT_W, y - MainWindow.CanvasPanY));
            rect.Points.Add(new Point(x - MainWindow.CanvasPanX + PERSON_RECT_W, y + PERSON_RECT_H - MainWindow.CanvasPanY));
            rect.Points.Add(new Point(x - MainWindow.CanvasPanX, y + PERSON_RECT_H - MainWindow.CanvasPanY));
            rect.Cursor = Cursors.Hand;
            canvas.Children.Add(rect);
        }
    }

    public partial class MainWindow : Window
    {
        static public double CanvasPanX = -Person.PERSON_RECT_W*2+20;
        static public double CanvasPanY = -800/2+Person.PERSON_RECT_H/2;
        private Point _prevCursPos = new Point();
        private bool _isMouseDown = false;

        public MainWindow()
        {
            InitializeComponent();
            Redraw();
        }

        private void canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = true;
        }

        private void canvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            _isMouseDown = false;
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            var pos = e.GetPosition(canvas);
            if (_isMouseDown)
            {
                CanvasPanX += _prevCursPos.X - pos.X;
                CanvasPanY += _prevCursPos.Y - pos.Y;
            }
            _prevCursPos = pos;
            Redraw();
        }

        private void Redraw()
        {
            canvas.Children.Clear();

            Person person1 = new Person();
            person1.Gender = Person.GenderEnum.Male;
            person1.Draw(canvas, 0, 0);

            Person person2 = new Person();
            person2.Gender = Person.GenderEnum.Female;
            person2.Draw(canvas, 20 + Person.PERSON_RECT_W, 0);
        }
    }
}
