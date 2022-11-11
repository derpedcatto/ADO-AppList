using System.Windows;

namespace ADO_AppList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string ConnectionString { get; } = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\sasha\source\repos\ADO-AppList\ADO-AppList\Databases\Sales_DB.mdf;Integrated Security=True";
        public static string ConnectionStringEF { get; } = @"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=EF_DB.mdf;Integrated Security=True";
    }
}