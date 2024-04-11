using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MaterialDesignColors;

namespace wpf6
{
    /// <summary>
    /// Логика взаимодействия для PageCustomerEF.xaml
    /// </summary>
    public partial class PageCustomerEF : Page
    {
        GymEntities context = new GymEntities();
        private Dictionary<string, int> SeasonticketTerm = new Dictionary<string, int>();
        private Dictionary<string, int> TrainerFirstname = new Dictionary<string, int>();
        public PageCustomerEF()
        {
            InitializeComponent();
            CustomerEFGrid.ItemsSource = context.customer.ToList();

            var pairs = context.seasonticket.ToList().Select(row => new KeyValuePair<string, int>(row.term, row.ID_seasonticket));
            foreach (var pair in pairs)
                SeasonticketTerm.Add(pair.Key, pair.Value);
            combobox.ItemsSource = SeasonticketTerm.Keys;
            var vs = context.trainer.ToList().Select(row => new KeyValuePair<string, int>(row.firstname, row.ID_trainer));
            foreach (var v in vs)
                TrainerFirstname.Add(v.Key, v.Value);
            combobox2.ItemsSource = TrainerFirstname.Keys;
        }
        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            if (CustomerEFGrid.SelectedItem != null)
            {
                var selectedAccount = CustomerEFGrid.SelectedItem as customer;
                context.customer.Remove(selectedAccount);
                context.SaveChanges();
                CustomerEFGrid.ItemsSource = context.customer.ToList();
            }
        }

        private void Change_Button(object sender, RoutedEventArgs e)
        {
            if (CustomerEFGrid.SelectedItem != null)
            {
                var selected = CustomerEFGrid.SelectedItem as customer;
                selected.ID_seasonticket = SeasonticketTerm[combobox.SelectedItem.ToString()];
                selected.ID_trainer = TrainerFirstname[combobox2.SelectedItem.ToString()];
                selected.firstname = TextBox1.Text;
                selected.surname = TextBox2.Text;
                selected.middlename = TextBox3.Text;
                context.SaveChanges();
                CustomerEFGrid.ItemsSource = context.customer.ToList();
            }
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {

            customer customer = new customer();
            customer.ID_seasonticket = SeasonticketTerm[combobox.SelectedItem.ToString()];
            customer.ID_trainer = TrainerFirstname[combobox2.SelectedItem.ToString()];
            customer.firstname = TextBox1.Text;
            customer.surname = TextBox2.Text;
            customer.middlename = TextBox3.Text;
            context.customer.Add(customer);
            context.SaveChanges();
            CustomerEFGrid.ItemsSource = context.customer.ToList();

        }
        private void CustomerEFGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerEFGrid.SelectedItem != null)
            {
                var selected = CustomerEFGrid.SelectedItem as customer;
                TextBox1.Text = selected.firstname;
                TextBox2.Text = selected.surname;
                TextBox3.Text = selected.middlename;
                combobox.SelectedIndex = selected.ID_seasonticket;

                CustomerEFGrid.SelectedItem = selected.ID_seasonticket;
                var FriendNickname = SeasonticketTerm.FirstOrDefault(x => x.Value == selected.ID_seasonticket).Key;
                for (int i = 0; combobox.Items.Count > 0; i++)
                {
                    if (combobox.Items[i].ToString() == FriendNickname)
                    {
                        combobox.SelectedIndex = i;
                        break;
                    }
                }
                combobox2.SelectedIndex = selected.ID_trainer;
                combobox.SelectedItem = selected.ID_seasonticket;
                var GameSingle = TrainerFirstname.FirstOrDefault(x => x.Value == selected.ID_trainer).Key;
                for (int i = 0; combobox2.Items.Count > 0; i++)
                {
                    if (combobox2.Items[i].ToString() == GameSingle)
                    {
                        combobox2.SelectedIndex = i;
                        break;
                    }
                }
            }
        }
    }
}
