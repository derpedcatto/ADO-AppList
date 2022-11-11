using ADO_AppList._3_EntityFramework;
using ADO_AppList._0_Basics;
using System.Windows;

namespace ADO_AppList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BasicsButton_Click(object sender, RoutedEventArgs e)
        {
            new BasicsWindow().ShowDialog();
        }

        private void SalesButton_Click(object sender, RoutedEventArgs e)
        {
            new SalesWindow().ShowDialog();
        }

        private void CRUDButton_Click(object sender, RoutedEventArgs e)
        {
            new CrudWindow().ShowDialog();
        }

        private void EFButton_Click(object sender, RoutedEventArgs e)
        {
            new EFWindow().ShowDialog();
        }
    }
}
