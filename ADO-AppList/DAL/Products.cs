﻿using System.Collections.Generic;
using System.Data.SqlClient;

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

        #endregion
    }
}
