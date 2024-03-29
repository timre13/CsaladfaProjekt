﻿//#define FILTER_SPOUSE_LIST_BY_GENDER

using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using DB;


namespace Csaladfa
{
    /// <summary>
    /// Interaction logic for RelationshipEditWindow.xaml
    /// </summary>
    public partial class RelationshipEditWindow : Window
    {
        private Relationship _relationship;

        public RelationshipEditWindow(Relationship rel)
        {
            InitializeComponent();
            _relationship = rel;

            List<Person> wifeListItems = new List<Person> { new Person() };
            wifeListItems.AddRange(DB.DB.getAllPeople()
#if FILTER_SPOUSE_LIST_BY_GENDER
                .Where(x => x.gender == 'F' || x.gender == 'X' || x.gender == null)
#endif
            );
            WifeCombobox.ItemsSource = wifeListItems;
            WifeCombobox.SelectedIndex = wifeListItems
                .Select((v, i) => new {index=i, value=v})
                .Where(x => x.value.id == rel.wife).FirstOrDefault()?.index ?? 0;

            List<Person> husbandListItems = new List<Person> { new Person() };
            husbandListItems.AddRange(DB.DB.getAllPeople()
#if FILTER_SPOUSE_LIST_BY_GENDER
                .Where(x => x.gender == 'M' || x.gender == 'X' || x.gender == null)
#endif
            );
            HusbandCombobox.ItemsSource = husbandListItems;
            HusbandCombobox.SelectedIndex = husbandListItems
                .Select((v, i) => new { index = i, value = v })
                .Where(x => x.value.id == rel.husband).FirstOrDefault()?.index ?? 0;

            List<Settlement> settlements = new List<Settlement> { new Settlement() };
            settlements.AddRange(DB.DB.GetAllSettlements());
            MarriagePlaceCombobox.ItemsSource = settlements;
            MarriagePlaceCombobox.SelectedIndex = settlements
                .Select((v, i) => new { index = i, value = v })
                .Where(x => x.value.id == rel.location).FirstOrDefault()?.index ?? 0;

            MarriageDateYearInput.Text = rel.start_year?.ToString() ?? "";
            DivorceDateYearInput.Text = rel.end_year?.ToString() ?? "";

            var months = new List<string> { "???" };
            months.AddRange(Enumerable.Range(1, 12).Select(x => new DateTime(2000, x, 1).ToString("MMMM", CultureInfo.CreateSpecificCulture("hu"))));
            MarriageDateMonthInput.ItemsSource = months;
            DivorceDateMonthInput.ItemsSource = months;
            MarriageDateMonthInput.SelectedIndex = (int)(rel.start_month ?? 0);
            DivorceDateMonthInput.SelectedIndex = (int)(rel.end_month ?? 0);

            var days = new List<string> { "???" };
            days.AddRange(Enumerable.Range(1, 31).Select(x => x.ToString()));
            MarriageDateDayInput.ItemsSource = days;
            DivorceDateDayInput.ItemsSource = days;
            MarriageDateDayInput.SelectedIndex = (int)(rel.start_day ?? 0);
            DivorceDateDayInput.SelectedIndex = (int)(rel.end_day ?? 0);

            IsLegalCheckBox.IsChecked = rel.legal;
        }

        private static readonly Regex _yearRegex = new Regex("[0-9]+");
        private void StartOrEndDateYearInput_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !_yearRegex.IsMatch(e.Text) || ((sender as TextBox)!.Text.Length == 0 && e.Text[0] == '0');
        }

        private void RelationshipSaveButton_Click(object sender, RoutedEventArgs e)
        {
            _relationship.husband = HusbandCombobox.SelectedIndex == 0 ? null : (HusbandCombobox.SelectedItem as dynamic).id;
            _relationship.wife = WifeCombobox.SelectedIndex == 0 ? null : (WifeCombobox.SelectedItem as dynamic).id;
            _relationship.location = MarriagePlaceCombobox.SelectedIndex == 0 ? null : (MarriagePlaceCombobox.SelectedItem as dynamic).id;
            _relationship.start_year =  MarriageDateYearInput.Text == "0" ? null : int.Parse(MarriageDateYearInput.Text);
            _relationship.start_month = MarriageDateMonthInput.SelectedIndex == 0 ? null : MarriageDateMonthInput.SelectedIndex;
            _relationship.start_day = MarriageDateDayInput.SelectedIndex == 0 ? null : MarriageDateDayInput.SelectedIndex;
            _relationship.legal = IsLegalCheckBox.IsChecked ?? false;
            _relationship.end_year =  DivorceDateYearInput.Text == "0" ? null : int.Parse(DivorceDateYearInput.Text);
            _relationship.end_month = DivorceDateMonthInput.SelectedIndex == 0 ? null : DivorceDateMonthInput.SelectedIndex;
            _relationship.end_day = DivorceDateDayInput.SelectedIndex == 0 ? null : DivorceDateDayInput.SelectedIndex;
            DB.DB.UpdateRelationship(_relationship);

            ((MainWindow)Owner).ReloadPerson();
        }
    }
}
