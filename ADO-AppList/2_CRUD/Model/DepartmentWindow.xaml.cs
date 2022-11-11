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

namespace ADO_AppList
{
    /// <summary>
    /// Interaction logic for DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        public Entities.Department Department { get; set; }


        public DepartmentWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DepartmentID.Text = Department.Id.ToString();
            DepartmentName.Text = Department.Name;

            if (Department.Id == Guid.Empty)
            {
                Save.Content = "Добавить";
                DepartmentName.Text = "";
                Delete.IsEnabled = false;
            }
            else
            {
                DepartmentName.Text = Department.Name;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Department.Name = DepartmentName.Text;
            DialogResult = true;
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes ==  MessageBox.Show("Вы уверены?", "Удаление данных",
                                         MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                Department.Name = String.Empty;
                DialogResult = true;
                this.Close();
            }
        }

        private void DepartmentName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Save.IsEnabled = DepartmentName.Text.Trim() == String.Empty ? false : true;
        }
    }
}
