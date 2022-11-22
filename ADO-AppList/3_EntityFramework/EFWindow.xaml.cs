using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace ADO_AppList._3_EntityFramework
{
    /// <summary>
    /// Interaction logic for EFWindow.xaml
    /// </summary>
    public partial class EFWindow : Window
    {
        #region Variables

        private EF.FirmContext _firmContext;
        public ObservableCollection<Entities.Department> Departments { get; }
        public ObservableCollection<Entities.Product> Products { get; }
        public ObservableCollection<Entities.Manager> Managers { get; }

        #endregion


        #region Constructor

        public EFWindow()
        {
            InitializeComponent();
            _firmContext = new();
            Departments = new();
            Products = new();
            Managers = new();
            this.DataContext = this;
        }

        #endregion


        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LabelDepartments.Content = _firmContext.Departments.Count();
            LabelManagers.Content = _firmContext.Managers.Count();
            LabelProducts.Content = _firmContext.Products.Count();

            var depQuery = from d in _firmContext.Departments
                           where d.Name != null
                           orderby d.Name descending
                           select d;

            var productsQuery = _firmContext.Products.Where(p => p.Price > 0)
                                                     .OrderBy(p => p.Name);

            var managersQuery = _firmContext.Managers.OrderBy(p => p.Name);

            foreach (var dep in depQuery)
            {
                Departments.Add(dep);
            }
            foreach (var p in productsQuery)
            {
                Products.Add(p);
            }
            foreach (var m in managersQuery)
            {
                Managers.Add(m);
            }
        }

        #endregion
    }
}
