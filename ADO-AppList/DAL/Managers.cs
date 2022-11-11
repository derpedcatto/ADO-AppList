using System.Collections.Generic;
using System.Data.SqlClient;

namespace ADO_AppList.DAL
{
    public class Managers
    {
        #region Variables

        private readonly SqlConnection _connection;

        #endregion


        #region Constructor

        public Managers(SqlConnection connection)
        {
            _connection = connection;
        }

        #endregion


        #region Methods

        public List<Entities.Manager> GetList()
        {
            List<Entities.Manager> managers = new();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Name, Surname, Secname, Id_main_dep, Id_sec_dep, Id_chief FROM Managers";
                using SqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    managers.Add(new()
                    {
                        Id = res.GetGuid(0),
                        Name = res.GetString(1),
                        Surname = res.GetString(2),
                        Secname = res.GetString(3),
                        Id_main_dep = res.GetGuid(4),
                        Id_sec_dep = res.GetGuid(5),
                        Id_chief = res.GetGuid(6)
                    });
                }
            }
            return managers;
        }

        #endregion
    }
}