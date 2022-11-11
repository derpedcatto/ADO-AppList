using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace ADO_AppList
{
    /// <summary>
    /// Interaction logic for CrudWindow.xaml
    /// </summary>
    public partial class CrudWindow : Window
    {
        public ObservableCollection<Entities.Department> Departments { get; set; }  // Коллекция, уведомляющая контейнер о изменениях
        private readonly SqlConnection _connection;
        private readonly DAL.Departments _departments;
        private DispatcherTimer _timer = new();

        private int n = 0;


        public CrudWindow()
        {
            InitializeComponent();
            _connection = new SqlConnection(App.ConnectionString);
            ConnectDB();

            _departments = new DAL.Departments(_connection);
            Departments = new(_departments.GetList())
            {
                new Entities.Department
                {
                    Id = Guid.Empty,
                    Name = "Добавить новый отдел"
                }
            };
                
            _timer.Interval = new TimeSpan(0,0,1);
            _timer.Tick += Timer_Tick;

            this.DataContext = this;
        }


        private void ConnectDB()
        {
            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Connection error", MessageBoxButton.OK, MessageBoxImage.Error);
                this.Close();
            }
        }


        private void Timer_Tick(object? sender, EventArgs e)
        {
            Departments.Add(_departments.GetList()[n]);
            n++;
            if (n == _departments.GetList().Count)
            {
                _timer.Stop();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // _timer.Start();
        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _connection?.Close();
        }

        private void ListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is ListViewItem item)
            {
                if (item.Content is Entities.Department department)
                {
                    var editWindow = new DepartmentWindow() { Department = department };
                    if (editWindow.ShowDialog() == true)
                    {
                        if (department.Id == Guid.Empty)
                        {
                            Guid id = _departments.Create(department);
                            Departments.Remove(department);
                            department.Id = id;
                            Departments.Add(department);
                            Departments.Add(new Entities.Department { Id = Guid.Empty, Name = "Добавить новый отдел" });
                        }
                        else
                        {
                            if (department.Name == String.Empty)
                            {
                                Departments.Remove(department);
                                _departments.Delete(department);
                            }
                            else
                            {
                                int index = Departments.IndexOf(department);
                                Departments.Remove(department);
                                Departments.Insert(index, department);
                                _departments.Update(department);
                            }
                        }
                    }
                }
            }
        }
    }
}