using System;
using System.Collections.Generic;
using System.Linq;
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

namespace wpf6
{
    /// <summary>
    /// Логика взаимодействия для PageTrainerEF.xaml
    /// </summary>
    public partial class PageTrainerEF : Page
    {
        GymEntities context = new GymEntities();
        public PageTrainerEF()
        {
            InitializeComponent();
            TrainerEFGrid.ItemsSource = context.trainer.ToList();
        }
        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            if (TrainerEFGrid.SelectedItem != null)
            {
                var selectedTrainer = TrainerEFGrid.SelectedItem as trainer;
                context.trainer.Remove(selectedTrainer);
                context.SaveChanges();
                TrainerEFGrid.ItemsSource = context.trainer.ToList();
            }
        }

        private void Change_Button(object sender, RoutedEventArgs e)
        {
            if (TrainerEFGrid.SelectedItem != null && TrainerEFGrid.SelectedItem is trainer)
            {
                var selected = TrainerEFGrid.SelectedItem as trainer;
                selected.firstname = TextBox1.Text;
                selected.surname = TextBox2.Text;
                selected.middlename = TextBox3.Text;
                selected.age = Convert.ToInt32(TextBox4.Text);
                context.SaveChanges();
                TrainerEFGrid.ItemsSource = context.trainer.ToList();
            }
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {

            trainer trainer = new trainer();
            trainer.firstname = TextBox1.Text;
            trainer.surname = TextBox2.Text;
            trainer.middlename = TextBox3.Text;
            trainer.age = Convert.ToInt32(TextBox4.Text);
            context.trainer.Add(trainer);
            context.SaveChanges();
            TrainerEFGrid.ItemsSource = context.trainer.ToList();

        }
        private void TrainerEFGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TrainerEFGrid.SelectedItem != null && TrainerEFGrid.SelectedItem is trainer)
            {
                var selected = TrainerEFGrid.SelectedItem as trainer;
                TextBox1.Text = selected.firstname;
                TextBox2.Text = selected.surname;
                TextBox3.Text = selected.middlename;
                TextBox4.Text = selected.age.ToString();


            }
        }
    }
}
