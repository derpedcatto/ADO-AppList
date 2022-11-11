using System.Windows;
using System.Data.SqlClient;
using System.Windows.Media;
using System.Text;
using System.Windows.Controls;

namespace ADO_AppList
{
    /// <summary>
    /// Interaction logic for SalesWindow.xaml
    /// </summary>
    public partial class SalesWindow : Window
    {
        #region Variables

        private readonly SqlConnection _connection;
        private readonly DAL.Departments _departments;
        private readonly DAL.Products _products;

        #endregion


        #region Constructor

        public SalesWindow()
        {
            InitializeComponent();
            _connection = new SqlConnection(App.ConnectionString);
            _departments = new DAL.Departments(_connection);
            _products = new DAL.Products(_connection);
        }

        #endregion



        #region Helper methods

        private void UpdateMonitorLabel(string sqlString, Label monitorLabel)
        {
            using (SqlCommand cmd = new(sqlString, _connection))
            {
                try
                {
                    monitorLabel.Content = cmd.ExecuteScalar();
                    monitorLabel.Foreground = Brushes.Green;
                }
                catch
                {
                    monitorLabel.Content = "--";
                    monitorLabel.Foreground = Brushes.Red;
                }
            }
        }

        #endregion


        #region Methods

        private void ShowMonitor()
        {
            try
            {
                _connection.Open();
                MonitorDBLabel.Content = "Подключено";
                MonitorDBLabel.Foreground = Brushes.Green;
            }
            catch
            {
                MonitorDBLabel.Content = "Отключено";
                MonitorDBLabel.Foreground = Brushes.Red;
            }

            UpdateMonitorLabel("SELECT COUNT(*) FROM Departments", MonitorDepartmentsLabel);
            UpdateMonitorLabel("SELECT COUNT(*) FROM Managers", MonitorDepartmentsLabel);
            UpdateMonitorLabel("SELECT COUNT(*) FROM Products", MonitorDepartmentsLabel);
            UpdateMonitorLabel("SELECT COUNT(*) FROM Sales", MonitorDepartmentsLabel);
        }

        private void ShowDepartments()
        {
            StringBuilder sb = new();
            foreach (Entities.Department department in _departments.GetList())
            {
                sb.AppendLine(department.ToString());
            }
            DepartmentsInfo.Text = sb.ToString();
        }

        private void ShowProducts()
        {
            ProductsGrid.ItemsSource = _products.GetList();
            ProductsGrid.Columns[0].Visibility = Visibility.Collapsed;
        }

        private void ShowManagers()
        {
            ManagersGrid.ItemsSource = _departments.GetDepartmentManagerCount();
        }

        #endregion


        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowMonitor();
            ShowDepartments();
            ShowProducts();
            ShowManagers();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _connection?.Close();
        }

        #endregion
    }
}