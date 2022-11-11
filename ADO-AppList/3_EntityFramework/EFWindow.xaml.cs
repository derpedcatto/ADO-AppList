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
        private EF.FirmContext _firmContext;
        public ObservableCollection<Entities.Department> Departments { get; }
        public ObservableCollection<Entities.Product> Products { get; }

        public EFWindow()
        {
            InitializeComponent();
            _firmContext = new();
            Departments = new();
            Products = new();
            this.DataContext = this;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LabelDepartments.Content = _firmContext.Departments.Count();

            var cntQuery = from p in _firmContext.Products
                           where p.Price > 0
                           select p;
            LabelProducts.Content = cntQuery.Count();

            var depQuery = from d in _firmContext.Departments
                           where d.Name != null
                           orderby d.Name descending
                           select d;
            foreach(var dep in depQuery)
            {
                Departments.Add(dep);
            }

            var query = _firmContext.Products.Where(p => p.Price > 0)
                                             .OrderBy(p => p.Name);
            foreach (var p in query)
            {
                Products.Add(p);
            }
        }
    }
}
