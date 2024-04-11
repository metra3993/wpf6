using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Principal;
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
using MaterialDesignColors;
using wpf6.GymDataSetTableAdapters;

namespace wpf6
{
    /// <summary>
    /// Логика взаимодействия для PageCustomer.xaml
    /// </summary>
    public partial class PageSeasonticket : Page
    {
        seasonticketTableAdapter seasonticketTableAdapter = new seasonticketTableAdapter();
        public PageSeasonticket()
        {
            InitializeComponent();
            SeasonticketGrid.ItemsSource = seasonticketTableAdapter.GetData();
        }
        private void Add_Button(object sender, RoutedEventArgs e)
            {
                var term = TextBox1.Text;
                var price = Convert.ToInt32(TextBox2.Text);
                seasonticketTableAdapter.InsertQuery(term, price);
                SeasonticketGrid.ItemsSource = seasonticketTableAdapter.GetData();
            }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            var ID_seasonticket = Convert.ToInt32((SeasonticketGrid.SelectedItem as DataRowView).Row[0]);
            seasonticketTableAdapter.DeleteQuery(ID_seasonticket);
            SeasonticketGrid.ItemsSource = seasonticketTableAdapter.GetData();
        }

        private void Change_Button(object sender, RoutedEventArgs e)
        {
            if (SeasonticketGrid.SelectedItem != null)
            {
                var Original_id = Convert.ToInt32((SeasonticketGrid.SelectedItem as DataRowView).Row[0]);
                var term = TextBox1.Text;
                var price = Convert.ToInt32(TextBox2.Text);
                seasonticketTableAdapter.UpdateQuery(term, price, Original_id);
                SeasonticketGrid.ItemsSource = seasonticketTableAdapter.GetData();
            }
            else
            {
                // Обработка ситуации, когда ничего не выбрано в DataGrid
                MessageBox.Show("Выберите строку для изменения.");
            }
        }


        private void SeasonticketGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SeasonticketGrid.SelectedItem == null)
            {
                return;
            }
            TextBox1.Text = (SeasonticketGrid.SelectedItem as DataRowView).Row[1].ToString();
            TextBox2.Text = (SeasonticketGrid.SelectedItem as DataRowView).Row[2].ToString();
        }

        private void TextBox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox2_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}