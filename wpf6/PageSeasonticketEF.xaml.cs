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
using MaterialDesignColors;

namespace wpf6
{
    /// <summary>
    /// Логика взаимодействия для PageSeasonticketEF.xaml
    /// </summary>
    public partial class PageSeasonticketEF : Page
    {
        GymEntities context = new GymEntities();
        public PageSeasonticketEF()
        {
            InitializeComponent();
            SeasonticketEFGrid.ItemsSource = context.seasonticket.ToList();
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            if (SeasonticketEFGrid.SelectedItem != null)
            {
                var selectednick = SeasonticketEFGrid.SelectedItem as seasonticket;
                context.seasonticket.Remove(selectednick);
                context.SaveChanges();
                SeasonticketEFGrid.ItemsSource = context.seasonticket.ToList();
            }
        }

        private void Change_Button(object sender, RoutedEventArgs e)
        {
            if (SeasonticketEFGrid.SelectedItem != null)
            {
                var selected = SeasonticketEFGrid.SelectedItem as seasonticket;
                selected.term = TextBox1.Text;
                selected.price = Convert.ToInt32(TextBox2.Text);
                context.SaveChanges();
                SeasonticketEFGrid.ItemsSource = context.seasonticket.ToList();
            }
        }


        private void Add_Button(object sender, RoutedEventArgs e)
        {
            seasonticket seasonticket = new seasonticket();
            seasonticket.term = TextBox1.Text;
            seasonticket.price = Convert.ToInt32(TextBox2.Text);
            context.seasonticket.Add(seasonticket);
            context.SaveChanges();
            SeasonticketEFGrid.ItemsSource = context.seasonticket.ToList();
        }

        private void SeasonticketGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SeasonticketEFGrid.SelectedItem != null)
            {
                var selected = SeasonticketEFGrid.SelectedItem as seasonticket;
                TextBox1.Text = selected.term;

            }
        }
    }
}
