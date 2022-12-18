using hometask_14.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hometask_14.Services.Concrets
{
    internal class CategoryService : ICategoryService
    {
        string? command;
        public string GetAll()
        {
            command = "select * from categories order by id";
            return command;
        }

        public void GetAllIngredients()
        {
            using(SqlConnection sql = new(Program.DatabaseUrl))
            {
                SqlCommand cmd = new("select * from Ingredients order by id", sql);
                sql.Open();
                using(SqlDataReader reader=cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetInt32(0)+". "+reader.GetString(1));
                    }
                }
            }
        }

        public void GetAllSizes()
        {
            using (SqlConnection sql = new(Program.DatabaseUrl))
            {
                SqlCommand cmd = new("select * from sizes", sql);
                sql.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetInt32(0) + ". " + reader.GetString(1)+" "+reader.GetInt32(2));
                    }
                }
            }
        }

        public void GetAllTypes()
        {
            using (SqlConnection sql = new(Program.DatabaseUrl))
            {
                SqlCommand cmd = new("select * from types order by id", sql);
                sql.Open();
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader.GetInt32(0) + ". " + reader.GetString(1));
                    }
                }
            }
        }
    }
}
