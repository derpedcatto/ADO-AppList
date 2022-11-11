using System;
using System.Data.SqlClient;
using System.Windows;

namespace ADO_AppList._0_Basics
{
    /// <summary>
    /// Interaction logic for BasicsWindow.xaml
    /// </summary>
    public partial class BasicsWindow : Window
    {
        #region Variables

        private SqlConnection connection;

        #endregion


        #region Constructor

        public BasicsWindow()
        {
            InitializeComponent();
            connection = new SqlConnection(App.ConnectionString);
        }

        #endregion


        #region Methods

        private void ButtonConnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Open();
                MessageBox.Show("Подключение открыто");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonDisconnect_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                connection.Close();
                MessageBox.Show("Подключение закрыто");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ButtonTimestamp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT CURRENT_TIMESTAMP", connection))
                {
                    MessageBox.Show(cmd.ExecuteScalar().ToString());
                }
            }
            catch
            {
                MessageBox.Show("Database is not connected!");
            }
        }

        #endregion
    }
}
