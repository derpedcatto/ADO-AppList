using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace ADO_AppList.DAL
{
    public class Products
    {
        #region Variables

        private readonly SqlConnection _connection;

        #endregion


        #region Constructor

        public Products(SqlConnection connection)
        {
            _connection = connection;
        }

        #endregion


        #region Methods

        public List<Entities.Product> GetList()
        {
            List<Entities.Product> products = new();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = "SELECT Id, Name, Price FROM Products";
                using SqlDataReader res = cmd.ExecuteReader();
                while (res.Read())
                {
                    products.Add(new()
                    {
                        Id = res.GetGuid(0),
                        Name = res.GetString(1),
                        Price = res.GetDouble(2)
                    });
                }
            }
            return products;
        }

        public void Update([NotNull] Entities.Product product)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "UPDATE Products SET Name = N'{0}', Price = {1} WHERE Id = '{2}'",
                    product.Name, product.Price.ToString().Replace(',', '.'), product.Id);
                cmd.ExecuteNonQuery();
            }
        }

        public Guid Create([NotNull] Entities.Product product)
        {
            Guid id = Guid.NewGuid();
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "INSERT INTO Products(Id, Name, Price) VALUES('{0}', N'{1}', {2})",
                    id, product.Name, product.Price.ToString().Replace(',', '.'));
                cmd.ExecuteNonQuery();
            }
            return id;
        }

        public void Delete([NotNull] Entities.Product product)
        {
            using (SqlCommand cmd = _connection.CreateCommand())
            {
                cmd.CommandText = String.Format(
                    "DELETE FROM Products WHERE Id = '{0}' ",
                    product.Id);
                cmd.ExecuteNonQuery();
            }
        }

        #endregion
    }
}