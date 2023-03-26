using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Reflection.PortableExecutable;
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

        private int _selectedPersonId = -1;
        private List<dynamic> _personListItems = new List<dynamic>();

        public void Draw(Canvas canvas, Person pers, int x, int y)
        {
            /*
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
            */
        }

        public MainWindow()
        {
            InitializeComponent();

            /*
            DB.Person[] people = DB.DB.getPerson(25).getChildren(2);
            Debug.WriteLine("Gyerekek:");
            foreach (var person in people)
            { 
                if (person != null)
                    Debug.WriteLine(person.id + " " + person.surname + " " + person.forename);
            }*/


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
            SetSelectedPerson(-1);
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
            _personListItems.Clear();
            foreach (var p in people)
            {
                var person = new
                {
                    id = p.id,
                    forename = p.forename,
                    forenameDisp = p.forename ?? "???",
                    surname = p.surname,
                    surnameDisp = p.surname ?? "???",
                    maidForename = p.maiden_forename,
                    maidForenameDisp = p.maiden_forename ?? "???",
                    maidSurname = p.maiden_surname,
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
                };
                PersonList.Items.Add(person);
                _personListItems.Add(person);
            }
            Debug.WriteLine("Person count: "+PersonList.Items.Count);
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

        private void PersonList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count == 0) return;

            dynamic? selected = e.AddedItems[0];
            Debug.WriteLine($"Selected: { selected }");
            SetSelectedPerson(selected?.id ?? -1);
        }

        private void SetSelectedPerson(int id)
        {
            _selectedPersonId = id;

            List<Settlement> settlements = new List<Settlement> { new Settlement() };
            settlements.AddRange(DB.DB.GetAllSettlements());
            PersonBirthPlaceCombobox.ItemsSource = settlements;
            PersonDeathPlaceCombobox.ItemsSource = settlements;

            var person = DB.DB.getAllPeople().Where(x => x.id == id).FirstOrDefault();
            if (person == null)
            {
                PersonSurnameEntry.Clear();
                PersonMaidenSurnameEntry.Clear();
                PersonForenameEntry.Clear();
                PersonMaidenForenameEntry.Clear();
                GenderCombobox.SelectedIndex = 0;
                BirthDateYearInput.Clear();
                BirthDateMonthInput.SelectedIndex = 0;
                BirthDateDayInput.SelectedIndex = 0;
                PersonBirthPlaceCombobox.SelectedIndex = 0;
                DeathDateYearInput.Clear();
                DeathDateMonthInput.SelectedIndex = 0;
                DeathDateDayInput.SelectedIndex = 0;
                PersonDeathPlaceCombobox.SelectedIndex = 0;
                PersonOccupationEntry.Clear();
                PersonNotesEntry.Clear();
                PersonMarriageList.Items.Clear();

                foreach (var child in RightGrid.Children)
                    (child as dynamic).IsEnabled = false;

                return;
            }

            foreach (var child in RightGrid.Children)
                (child as dynamic).IsEnabled = true;

            //List<Person> spouseListItems = new List<Person> { new Person() };
            //spouseListItems.AddRange(DB.DB.getAllPeople().Where(x => person.gender == null || x.gender == (person.gender == 'M' ? 'F' : 'M') || x.gender == null));

            PersonSurnameEntry.Text = person.surname ?? "";
            PersonMaidenSurnameEntry.Text = person.maiden_surname ?? "";
            PersonForenameEntry.Text = person.forename ?? "";
            PersonMaidenForenameEntry.Text = person.maiden_forename ?? "";
            GenderCombobox.SelectedIndex = person.GenderToIndex();
            BirthDateYearInput.Text = person.birth_year?.ToString() ?? "";
            BirthDateMonthInput.SelectedIndex = (int)(person.birth_month ?? 0);
            BirthDateDayInput.SelectedIndex = (int)(person.birth_day ?? 0);

            if (person.birthPlace != null)
                PersonBirthPlaceCombobox.SelectedIndex = settlements
                    .Select((v, i) => new { sett = v, index = i })
                    .FirstOrDefault(x => x.sett.id == person.birthPlace)?.index ?? -1;
            DeathDateYearInput.Text = person.death_year?.ToString() ?? "";
            DeathDateMonthInput.SelectedIndex = (int)(person.death_month ?? 0);
            DeathDateDayInput.SelectedIndex = (int)(person.death_day ?? 0);
            if (person.birthPlace != null)
                PersonDeathPlaceCombobox.SelectedIndex = settlements
                    .Select((v, i) => new { sett = v, index = i })
                    .FirstOrDefault(x => x.sett.id == person.deathPlace)?.index ?? -1;
            PersonOccupationEntry.Text = person.occupation ?? "";
            PersonNotesEntry.Text = person.notes ?? "";


            PersonMarriageList.Items.Clear();
            foreach (var m in person.GetMarriages())
            {
                var spouseId = m.husband == id ? m.wife : m.husband;
                var spouse = spouseId == null ? null : DB.DB.getPerson((int)spouseId);
                PersonMarriageList.Items.Add(new
                {
                    id = m.id,
                    spouseName = spouse?.FormattedName ?? "???",
                    startDate = DB.DB.DateToString(m.start_year, m.start_month, m.start_day), // FIXME: NULL helyett 0?
                    endDate = DB.DB.DateToString(m.end_year, m.end_month, m.end_day),
                    placeName = (m.location == null ? "???" : DB.DB.GetSettlement((int)m.location)?.DisplayName ?? "???"),
                    isLegal = (m.legal ? "igen" : "nem"),
                });
            }
            PersonMarriageList.SelectedIndex = 0;
            if (PersonMarriageList.Items.Count == 0)
                RelationshipDeleteButton.IsEnabled = false;
        }

        private void NewPersonMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var id = DB.DB.AddPerson();
            Debug.WriteLine($"Added new person with id {id}");
            UpdatePersonList();
            PersonList.SelectedIndex = _personListItems
                .Select((v, i) => new { value = v, index = i })
                .Where(x => x.value.id == id)
                .First().index;
            PersonList.ScrollIntoView(PersonList.SelectedItem);
            SetSelectedPerson(id);
        }

        private void NewCountryMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new TextDialog("Ország neve:");
            dlg.Owner = this;
            var result = dlg.ShowDialog();
            if (result.GetValueOrDefault() == true)
            {
                Debug.WriteLine($"Adding country: { dlg.GetText() }");
                DB.DB.AddCountry(dlg.GetText());
            }

            SetSelectedPerson(_selectedPersonId);
        }

        private void NewProvinceMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var countries = DB.DB.GetAllCountries().Select(x => new TextAndItemDialog.Item(x.id, x.name)).ToList();

            var dlg = new TextAndItemDialog("Megye neve:", "Ország:", countries);
            dlg.Owner = this;
            if (dlg.ShowDialog().GetValueOrDefault() == true)
            {
                Debug.WriteLine($"Adding province '{ dlg.GetText() }' to country (id='{ dlg.GetItemId() }', name='{dlg.GetItemName()}')");
                DB.DB.AddProvince(dlg.GetText(), dlg.GetItemId());
            }

            SetSelectedPerson(_selectedPersonId);
        }

        private void NewSettlementMenuItem_Click(object sender, RoutedEventArgs e)
        {
            var provinces = DB.DB.GetAllProvinces().Select(x => new TextAndItemDialog.Item(x.id, x.name+", "+x.countryName)).ToList();

            var dlg = new TextAndItemDialog("Település neve:", "Megye:", provinces);
            dlg.Owner = this;
            if (dlg.ShowDialog().GetValueOrDefault() == true)
            {
                Debug.WriteLine($"Adding settlement '{dlg.GetText()}' to province (id='{dlg.GetItemId()}', name='{dlg.GetItemName()}')");
                DB.DB.AddSettlement(dlg.GetText(), dlg.GetItemId());
            }

            SetSelectedPerson(_selectedPersonId);
        }

        private static string? EmptyToNull(in string value)
        {
            return value == null ? null : value;
        }

        public static char? IndexToGender(int index)
        {
            return index switch
            {
                1 => 'M',
                2 => 'F',
                3 => 'X',
                _ => null,
            };
        }

        private void PersonInfoSaveButton_Click(object sender, RoutedEventArgs e)
        {
            int personId = (PersonList.SelectedItem as dynamic).id;

            DB.Person person = new Person();
            person.id = personId;
            //person.parents = ;
            person.surname = EmptyToNull(PersonSurnameEntry.Text);
            person.forename = EmptyToNull(PersonForenameEntry.Text);
            person.maiden_surname = EmptyToNull(PersonMaidenSurnameEntry.Text);
            person.maiden_forename = EmptyToNull(PersonMaidenForenameEntry.Text);
            person.gender = IndexToGender(GenderCombobox.SelectedIndex);

            person.birthPlace = ((PersonBirthPlaceCombobox.SelectedItem as Settlement)?.id ?? -1) == -1 ? null : (PersonBirthPlaceCombobox.SelectedItem as Settlement)!.id;
            person.birth_year = BirthDateYearInput.Text == "" ? null : int.Parse(BirthDateYearInput.Text);
            person.birth_month = BirthDateMonthInput.SelectedIndex == 0 ? null : BirthDateMonthInput.SelectedIndex;
            person.birth_day = BirthDateDayInput.SelectedIndex == 0 ? null : BirthDateDayInput.SelectedIndex;

            person.deathPlace = ((PersonDeathPlaceCombobox.SelectedItem as Settlement)?.id ?? -1 ) == -1 ? null : (PersonDeathPlaceCombobox.SelectedItem as Settlement)!.id;
            person.death_year = DeathDateYearInput.Text == "" ? null : int.Parse(DeathDateYearInput.Text);
            person.death_month = DeathDateMonthInput.SelectedIndex == 0 ? null : DeathDateMonthInput.SelectedIndex;
            person.death_day = DeathDateDayInput.SelectedIndex == 0 ? null : DeathDateDayInput.SelectedIndex;
            //person.death_cause - TODO

            person.occupation = EmptyToNull(PersonOccupationEntry.Text);
            person.notes = EmptyToNull(PersonNotesEntry.Text);


            DB.DB.UpdatePerson(person);
            //person.DeleteRelationships();
            UpdatePersonList();
            PersonList.SelectedIndex = _personListItems
                .Select((v, i) => new { value = v, index = i })
                .Where(x => x.value.id == personId)
                .First().index;
            PersonList.ScrollIntoView(PersonList.SelectedItem);
            SetSelectedPerson(person.id);
        }

        private void PersonDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine($"Deleting person: {PersonList.SelectedItem}");
            DB.DB.DeletePerson((PersonList.SelectedItem as dynamic).id);

            UpdatePersonList();
            PersonList.UnselectAll();
            SetSelectedPerson(-1);
        }

        private void RelationshipDeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonMarriageList.SelectedItem == null)
                return;
            Debug.WriteLine($"Deleting relationship: {PersonMarriageList.SelectedItem}");
            DB.DB.DeleteRelationship((PersonMarriageList.SelectedItem as dynamic).id);
            SetSelectedPerson(_selectedPersonId);
        }

        private void PersonMarriageList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Debug.WriteLine($"Selected relationship: {PersonMarriageList.SelectedItem ?? "None"}");

            if (PersonMarriageList.SelectedItem == null)
            {
                RelationshipDeleteButton.IsEnabled = false;
                RelationshipEditButton.IsEnabled = false;
            }
            else
            {
                RelationshipDeleteButton.IsEnabled = true;
                RelationshipEditButton.IsEnabled = true;
            }
        }

        private void RelationshipEditButton_Click(object sender, RoutedEventArgs e)
        {
            if (PersonMarriageList.SelectedItem == null)
                return;
            var rel = DB.DB.getRelationship((PersonMarriageList.SelectedItem as dynamic).id);
            var dlg = new RelationshipEditWindow(rel);
            dlg.Title = "Kapcsolat Szerkesztő";
            dlg.Owner = this;
            dlg.ShowDialog();
        }

        private void RelationshipAddButton_Click(object sender, RoutedEventArgs e)
        {
            var personId = (PersonList.SelectedItem as dynamic).id;
            if (PersonList.SelectedItem == null) return;

            int relId;
            if (DB.DB.getPerson((PersonList.SelectedItem as dynamic).id)!.gender == 'F')
            {
                relId = DB.DB.AddRelationship(null, personId);
            }
            else
            {
                relId = DB.DB.AddRelationship(personId, null);
            }
            SetSelectedPerson(_selectedPersonId);
            PersonMarriageList.SelectedIndex = PersonMarriageList.Items.Count-1;
            RelationshipEditButton_Click(new(), new());
        }
    }
}
