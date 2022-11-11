using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace ADO_AppList.DAL
{
    public class Departments
    {
        #region Variables

        private readonly SqlConnection _connection;

        #endregion


        #region Constructor

        public Departments(SqlConnection connection)
        {
            _connection = connection;
        }

        #endregion


        #region Methods

        public List<Entities.DepartmentManagerCount> GetDepartmentManagerCount()
        {
            List<Entities.DepartmentManagerCount> managerCountList = new();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Departments.Name, COUNT(Managers.Id) AS Managers FROM Managers, Departments WHERE Departments.Id = Managers.Id_main_dep GROUP BY Departments.Name";
                SqlDataReader res = cmd.ExecuteReader();

                while (res.Read())
                {
                    managerCountList.Add(new Entities.DepartmentManagerCount
                    {
                        Name = res.GetString(0),
                        Count = res.GetInt32(1)
                    });
                }

                res.Close();
                return managerCountList;
            }
        }

        public List<Entities.Department> GetList()
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                List<Entities.Department> departments = new();
                cmd.CommandText = "SELECT Id, Name FROM Departments";
                SqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    departments.Add(new Entities.Department
                    {
                        Id = res.GetGuid(0),
                        Name = res.GetString(1)
                    });
                }
                res.Close();
                return departments;
            }
        }

        public void Update([NotNull] Entities.Department department)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "UPDATE Departments SET Name = N'{0}' WHERE Id = '{1}'",
                    department.Name, department.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public Guid Create([NotNull] Entities.Department department)
        {
            Guid id = Guid.NewGuid();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "INSERT INTO Departments(Id, Name) VALUES('{0}', N'{1}')",
                    id, department.Name);
                cmd.ExecuteNonQuery();
            }
            return id;
        }

        public void Delete([NotNull] Entities.Department department)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "DELETE FROM Departments WHERE Id = '{0}' ",
                    department.Id);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}
