//#define FILTER_SPOUSE_LIST_BY_GENDER

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
using System.Windows.Shapes;
using DB;


namespace Csaladfa
{
    /// <summary>
    /// Interaction logic for RelationshipEditWindow.xaml
    /// </summary>
    public partial class RelationshipEditWindow : Window
    {
        public RelationshipEditWindow(Relationship rel)
        {
            InitializeComponent();

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
        }
    }
}
