using System;
using System.Windows;
using System.Windows.Controls;

namespace ADO_AppList._2_CRUD.Model
{
    /// <summary>
    /// Interaction logic for ProductsWindow.xaml
    /// </summary>
    public partial class ProductsWindow : Window
    {
        #region Variables

        public Entities.Product Product { get; set; }

        #endregion


        #region Constructor

        public ProductsWindow(Entities.Product product)
        {
            InitializeComponent();
            Product = product;
        }

        #endregion


        #region Events

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ProductID.Text = Product.Id.ToString();
            ProductName.Text = Product.Name;
            ProductPrice.Text = Product.Price.ToString();

            if (Product.Id == Guid.Empty)
            {
                Save.Content = "Добавить";
                ProductName.Text = "";
                Delete.IsEnabled = false;
            }
            else
            {
                ProductName.Text = Product.Name;
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            Product.Name = ProductName.Text;
            Product.Price = Convert.ToDouble(ProductPrice.Text);
            DialogResult = true;
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBoxResult.Yes == MessageBox.Show("Вы уверены?", "Удаление данных",
                            MessageBoxButton.YesNo, MessageBoxImage.Question))
            {
                Product.Name = String.Empty;
                DialogResult = true;
                this.Close();
            }
        }

        private void ProductPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                Convert.ToDouble(ProductPrice.Text);
            }
            catch 
            {
                Save.IsEnabled = false;
                return;
            }
            Save.IsEnabled = true;
        }

        private void ProductName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Save.IsEnabled = ProductName.Text.Trim() == String.Empty ? false : true;
        }

        #endregion
    }
}
