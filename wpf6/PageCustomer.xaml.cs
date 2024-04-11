using System;
using System.Collections.Generic;
using System.Data;
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
using wpf6.GymDataSetTableAdapters;

namespace wpf6
{
    /// <summary> 
    /// Логика взаимодействия для PageCustomer.xaml
    /// </summary>
    public partial class PageCustomer : Page
    {
        customerTableAdapter customer = new customerTableAdapter();
        seasonticketTableAdapter seasonticket = new seasonticketTableAdapter();
        trainerTableAdapter trainer = new trainerTableAdapter();
        public static Action<GymDataSet.customerDataTable> CustomerGridChanged;
        public PageCustomer()
        {
            InitializeComponent();
            CustomerGrid.ItemsSource = customer.GetData();
            combobox.ItemsSource = seasonticket.GetData();
            combobox2.ItemsSource = trainer.GetData();
            combobox.DisplayMemberPath = "term";
            combobox2.DisplayMemberPath = "firstname";
            CustomerGridChanged += UpdateCustomerGrid;

        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            if (combobox.SelectedItem != null)
            {
                var firstname = TextBox1.Text;
                var surname = TextBox2.Text;
                var middlename = TextBox3.Text;
                var ID_seasonticket = (int)(combobox2.SelectedItem as DataRowView).Row[0];
                var ID_trainer = (int)(combobox.SelectedItem as DataRowView).Row[0];
                customer.InsertQuery(firstname, surname, middlename, ID_seasonticket, ID_trainer);
                CustomerGrid.ItemsSource = customer.GetData();
            }
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            var ID_customer = Convert.ToInt32((CustomerGrid.SelectedItem as DataRowView).Row[0]);
            customer.DeleteQuery(ID_customer);
            CustomerGrid.ItemsSource = customer.GetData();
        }

        private void Change_Button(object sender, RoutedEventArgs e)
        {
                if (combobox.SelectedItem != null)
                {
                    var Original_id = Convert.ToInt32((CustomerGrid.SelectedItem as DataRowView).Row[0]);
                    var firstname = TextBox1.Text;
                    var surname = TextBox2.Text;
                    var middlename = TextBox3.Text;
                    var ID_seasonticket = (int)(combobox2.SelectedItem as DataRowView).Row[0];
                    var ID_trainer = (int)(combobox.SelectedItem as DataRowView).Row[0];
                    customer.UpdateQuery(firstname, surname, middlename, ID_seasonticket, ID_trainer, Original_id);
                    CustomerGrid.ItemsSource = customer.GetData();
                }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combobox2.SelectedItem != null)
            {
                var ID_seasonticket = (int)(combobox2.SelectedItem as DataRowView).Row[0];
            }
        }

        private void ComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (combobox.SelectedItem != null)
            {
                var ID_trainer = (int)(combobox.SelectedItem as DataRowView).Row[0];
            }
        }
        private void CustomerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CustomerGrid.SelectedItem == null)
            {
                return;
            }
            TextBox1.Text = (CustomerGrid.SelectedItem as DataRowView).Row[1].ToString();
            TextBox2.Text = (CustomerGrid.SelectedItem as DataRowView).Row[2].ToString();
            TextBox3.Text = (CustomerGrid.SelectedItem as DataRowView).Row[3].ToString();
            combobox.SelectedItem = Convert.ToInt32((CustomerGrid.SelectedItem as DataRowView).Row[4]);
            combobox2.SelectedItem = Convert.ToInt32((CustomerGrid.SelectedItem as DataRowView).Row[5]);
        }
        private void UpdateCustomerGrid(GymDataSet.customerDataTable values)
        {
            CustomerGrid.ItemsSource = values;
        }
    }

}
