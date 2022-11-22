using ADO_AppList._2_CRUD.Model;
using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
        #region Variables

        private readonly SqlConnection _connection;

        public ObservableCollection<Entities.Department> Departments { get; set; }  // Коллекция, уведомляющая контейнер о изменениях
        private readonly DAL.Departments _departments;
        private DispatcherTimer _departmentTimer = new();
        private int _departmentItemCount = 0;

        public ObservableCollection<Entities.Product> Products { get; set; }
        private readonly DAL.Products _products;
        private DispatcherTimer _productsTimer = new();
        private int _productsItemCount = 0;

        #endregion


        #region Constructor

        public CrudWindow()
        {
            InitializeComponent();
            _connection = new SqlConnection(App.ConnectionString);
            ConnectDB();

            _departments = new DAL.Departments(_connection);
            InitDepartments();

            _products = new DAL.Products(_connection);
            InitProducts();

            this.DataContext = this;
        }

        #endregion


        #region Methods

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

        private void InitDepartments()
        {
            Departments = new(_departments.GetList())
            {
                new Entities.Department
                {
                    Id = Guid.Empty,
                    Name = "Добавить новый отдел"
                }
            };

            _departmentTimer.Interval = new TimeSpan(0, 0, 1);
            _departmentTimer.Tick += DepartmentTimer_Tick;
        }

        private void InitProducts()
        {
            Products = new(_products.GetList())
            {
                new Entities.Product
                {
                    Id = Guid.Empty,
                    Name = "Добавить новый отдел",
                    Price = 0
                }
            };

            _productsTimer.Interval = new TimeSpan(0, 0, 1);
            _productsTimer.Tick += ProductsTimer_Tick;
        }

        private void DepartmentTimer_Tick(object? sender, EventArgs e)
        {
            Departments.Add(_departments.GetList()[_departmentItemCount]);
            _departmentItemCount++;
            if (_departmentItemCount == _departments.GetList().Count)
            {
                _departmentTimer.Stop();
            }
        }

        private void ProductsTimer_Tick(object? sender, EventArgs e)
        {
            Products.Add(_products.GetList()[_productsItemCount]);
            _productsItemCount++;
            if (_productsItemCount == _products.GetList().Count)
            {
                _departmentTimer.Stop();
            }
        }

        #endregion


        #region Window events

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            _connection?.Close();
        }

        private void ListViewItem_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (sender is not ListViewItem)
                return;

            var item = (ListViewItem)sender;

            if (item.Content is Entities.Department department)
            {
                var editWindow = new DepartmentWindow(department);
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

            if (item.Content is Entities.Product product)
            {
                var editWindow = new ProductsWindow(product);
                if (editWindow.ShowDialog() == true)
                {
                    if (product.Id == Guid.Empty)
                    {
                        Guid id = _products.Create(product);
                        Products.Remove(product);
                        product.Id = id;
                        Products.Add(product);
                        Products.Add(new Entities.Product { Id = Guid.Empty, Name = "Добавить новый продукт", Price = 0 });
                    }
                    else
                    {
                        if (product.Name == String.Empty)
                        {
                            Products.Remove(product);
                            _products.Delete(product);
                        }
                        else
                        {
                            int index = Products.IndexOf(product);
                            Products.Remove(product);
                            Products.Insert(index, product);
                            _products.Update(product);
                        }
                    }
                }
            }
        }

        #endregion
    }
}