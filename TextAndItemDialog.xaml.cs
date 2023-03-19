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

namespace Csaladfa
{
    public partial class TextAndItemDialog : Window
    {
        public class Item
        {
            public int Id = -1;
            private string _name = "";
            public string Name { get => _name; }

            public Item(int id, string name)
            {
                Id = id;
                _name = name;
            }
        }

        private List<Item> _items;

        public TextAndItemDialog(string textPrompt, string itemPrompt, in List<Item> items)
        {
            InitializeComponent();

            if (items.Count == 0) throw new ArgumentOutOfRangeException();

            TextEntry.Focus();
            TextPromptLabel.Text = textPrompt;
            ItemPromptLabel.Text = itemPrompt;
            _items = items;
            ItemComboBox.ItemsSource = _items;
        }

        public string GetText()
        {
            return TextEntry.Text;
        }

        public int GetItemId()
        {
            return _items[ItemComboBox.SelectedIndex].Id;
        }

        public string GetItemName()
        {
            return _items[ItemComboBox.SelectedIndex].Name;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
