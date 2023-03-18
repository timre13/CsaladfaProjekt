using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using DB;

namespace Csaladfa
{
    public partial class MainWindow : Window
    {
        public static int PERSON_RECT_W = 250;
        public static int PERSON_RECT_H = 100;

        static public double CanvasPanX = 450-PERSON_RECT_W*2+20;
        static public double CanvasPanY = -500/2+PERSON_RECT_H/2;
        private Point _prevCursPos = new Point();
        private bool _isMouseDown = false;

        public void Draw(Canvas canvas, Person pers, int x, int y)
        {
            var rect = new Polygon();
            rect.Fill = pers.GenderToBrush();
            rect.Stroke = Brushes.Gray;
            rect.StrokeThickness = 5;
            rect.Points.Add(new Point(x - CanvasPanX, y - CanvasPanY));
            rect.Points.Add(new Point(x - CanvasPanX + PERSON_RECT_W, y - CanvasPanY));
            rect.Points.Add(new Point(x - CanvasPanX + PERSON_RECT_W, y + PERSON_RECT_H - CanvasPanY));
            rect.Points.Add(new Point(x - CanvasPanX, y + PERSON_RECT_H - CanvasPanY));
            rect.Cursor = Cursors.Hand;
            canvas.Children.Add(rect);

            var nameText = new TextBlock();
            nameText.Text = $"{pers.forename} {pers.surname}";
            nameText.FontSize = 16;
            nameText.FontWeight = FontWeights.Bold;
            nameText.Foreground = Brushes.Black;
            Canvas.SetLeft(nameText, x - CanvasPanX + 10);
            Canvas.SetTop(nameText, y - CanvasPanY + 10);
            canvas.Children.Add(nameText);

            var dateText = new TextBlock();
            dateText.Text = $"{pers.birth_year} - {pers.death_year}";
            dateText.FontSize = 14;
            dateText.Foreground = Brushes.Black;
            Canvas.SetLeft(dateText, x - CanvasPanX + 10);
            Canvas.SetTop(dateText, y - CanvasPanY + 30);
            canvas.Children.Add(dateText);
        }

        public MainWindow()
        {
            InitializeComponent();

            var months = new List<string> { "???" };
            months.AddRange(Enumerable.Range(1, 12).Select(x => new DateTime(2000, x, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("hu"))));
            BirthDateMonthInput.ItemsSource = months;
            DeathDateMonthInput.ItemsSource = months;

            var days = new List<string> { "???" };
            days.AddRange(Enumerable.Range(1, 31).Select(x => x.ToString()));
            BirthDateDayInput.ItemsSource = days;
            DeathDateDayInput.ItemsSource = days;

            Redraw();
            UpdatePersonList();
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
            person1.gender = 'M';
            person1.forename = "Kis";
            person1.surname = "János";
            person1.birth_year = 1958;
            person1.death_year = 1998;
            Draw(canvas, person1, 0, 0);

            Person person2 = new Person();
            person2.gender = 'F';
            Draw(canvas, person2, 20 + PERSON_RECT_W, 0);
        }

        private void UpdatePersonList()
        {
            var people = DB.DB.getAllPeople();
            PersonList.Items.Clear();
            //PersonList.ItemsSource = people;
            foreach (var p in people)
            {
                PersonList.Items.Add(new {
                    forename = p.forename,
                    forenameDisp = p.forename ?? "???",
                    surname = p.surname,
                    surnameDisp = p.surname ?? "???",
                    maidForename= p.maiden_forename,
                    maidForenameDisp = p.maiden_forename ?? "???",
                    maidSurname= p.maiden_surname,
                    maidSurnameDisp = p.maiden_surname ?? "???",
                    genderDisp = p.GenderToDisplayName(),
                    birthPlace = p.birthPlace,
                    deathPlace = p.deathPlace,
                    birthYear = p.birth_year,
                    birthYearDisp = !p.birth_year.HasValue ? "???" : p.birth_year.ToString(),
                    birthMonth = p.birth_month,
                    birthDay = p.birth_day,
                    deathYear = p.death_year,
                    deathYearDisp = (!p.death_year.HasValue ? "???" : (p.death_year == 0 ? "-" : p.death_year.ToString())),
                    deathMonth = p.death_month,
                    deathDay = p.death_day,
                    deathCause = p.death_cause,
                    occupation = p.occupation,
                    notes = p.notes,
                });
            }
            Debug.WriteLine("Person list: "+PersonList.Items.Count);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DB.DB.Close();
        }

        private static readonly Regex _yearRegex = new Regex("[0-9]+");
        private void BirthOrDeathDateYearInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_yearRegex.IsMatch(e.Text) || ((sender as TextBox)!.Text.Length == 0 && e.Text[0] == '0');
        }
    }
}
