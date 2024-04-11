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
    /// Логика взаимодействия для PageTrainer.xaml
    /// </summary>
    public partial class PageTrainer : Page
    {
        trainerTableAdapter trainer = new trainerTableAdapter();
        public static Action<GymDataSet.trainerDataTable> TrainerGridChanged;
        public PageTrainer()
        {
            InitializeComponent();
            TrainerGrid.ItemsSource = trainer.GetData();
            TrainerGridChanged += UpdateTrainerGrid;
        }

        private void Add_Button(object sender, RoutedEventArgs e)
        {
            var firstname = TextBox1.Text;
            var surname = TextBox2.Text;
            var middlename = TextBox3.Text;
            var age = Convert.ToInt32(TextBox4.Text);
            trainer.InsertQuery(firstname, surname, middlename, age);
            TrainerGrid.ItemsSource = trainer.GetData();
        }

        private void Delete_Button(object sender, RoutedEventArgs e)
        {
            var ID_trainer = Convert.ToInt32((TrainerGrid.SelectedItem as DataRowView).Row[0]);
            trainer.DeleteQuery(ID_trainer);
            TrainerGrid.ItemsSource = trainer.GetData();
        }

        private void Change_Button(object sender, RoutedEventArgs e)
        {
            if (TrainerGrid.SelectedItem != null)
            {
                var Original_id = Convert.ToInt32((TrainerGrid.SelectedItem as DataRowView).Row[0]);
                var firstname = TextBox1.Text;
                var surname = TextBox2.Text;
                var middlename = TextBox3.Text;
                var age = Convert.ToInt32(TextBox4.Text);
                trainer.UpdateQuery(firstname, surname, middlename, age, Original_id);
                TrainerGrid.ItemsSource = trainer.GetData();
            }
            else
            {
                MessageBox.Show("Выберите строку для изменения.");
            }
        }


        private void TrainerGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TrainerGrid.SelectedItem == null)
            {
                return;
            }
            TextBox1.Text = (TrainerGrid.SelectedItem as DataRowView).Row[1].ToString();
            TextBox2.Text = (TrainerGrid.SelectedItem as DataRowView).Row[2].ToString();
            TextBox3.Text = (TrainerGrid.SelectedItem as DataRowView).Row[3].ToString();
            TextBox4.Text = (TrainerGrid.SelectedItem as DataRowView).Row[4].ToString();
        }

        private void UpdateTrainerGrid(GymDataSet.trainerDataTable values)
        {
            TrainerGrid.ItemsSource = values;
        }
    }
}
