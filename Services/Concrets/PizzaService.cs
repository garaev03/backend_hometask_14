using hometask_14.Services.Interfaces;
using System.Data.SqlClient;

namespace hometask_14.Services.Concrets
{
    internal class PizzaService : IPizzaService
    {
        string? command;

        private bool CheckExistence(string name, string[] sizes, string[] ingredients, string[] types)
        {
            bool _available = true;
            using (SqlConnection connection = new(Program.DatabaseUrl))
            {
                connection.Open();

                SqlCommand cmd = new("select name from pizzas", connection);
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (name == reader.GetString(0))
                            _available = false;
                    }
                }

                cmd = new("select id from sizes order by id", connection);
                for (int i = 0; i < sizes.Length; i++)
                {
                    bool _sizeExist = false;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (int.Parse(sizes[i]) == reader.GetInt32(0))
                            {
                                _sizeExist = true;
                                continue;
                            }
                        }
                    }
                    if (!_sizeExist)
                        _available = false;
                }

                cmd = new("select * from Ingredients order by id", connection);
                for (int i = 0; i < ingredients.Length; i++)
                {
                    bool _ingredientExist = false;
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (int.Parse(ingredients[i]) == reader.GetInt32(0))
                            {
                                _ingredientExist = true;
                                continue;
                            }
                        }
                    }
                    if (!_ingredientExist)
                        _available = false;
                }

                cmd = new("select * from types order by id", connection);
                if (types != null)
                {
                    for (int i = 0; i < types.Length; i++)
                    {
                        bool _typeExist = false;
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                if (int.Parse(types[i]) == reader.GetInt32(0))
                                {
                                    _typeExist = true;
                                    continue;
                                }
                            }
                        }
                        if (!_typeExist)
                            _available = false;
                    }
                }
                connection.Close();
            }

            return _available;
        }

        public void AddProduct(string name, string[] sizes, decimal[] prices, string[] ingredients, string[] types)
        {
            bool available = CheckExistence(name, sizes, ingredients, types);
            if (available)
                using (SqlConnection sql = new(Program.DatabaseUrl))
                {

                    int id = 0;

                    sql.Open();

                    SqlCommand cmd = new("insert into pizzas values (1,@name)", sql);
                    cmd.Parameters.AddWithValue("@name", name);

                    cmd.ExecuteNonQuery();

                    cmd = new("Select Id From Pizzas Where  name='" + name + "'", sql);
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);
                        }
                    }

                    cmd = new("insert into prices values(@price,@id,@size)", sql);
                    for (int i = 0; i < prices.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@price", prices[i]);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.Parameters.AddWithValue("@size", sizes[i]);
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new("insert into pizzaingredients values (@pizzaId,@ingredientId)", sql);
                    for (int i = 0; i < ingredients.Length; i++)
                    {
                        cmd.Parameters.AddWithValue("@pizzaId", id);
                        cmd.Parameters.AddWithValue("@ingredientId", int.Parse(ingredients[i]));
                        cmd.ExecuteNonQuery();
                    }

                    cmd = new("insert into pizzatypes values (@pizzaId,@typeId)", sql);
                    if (types != null)
                    {
                        for (int i = 0; i < types.Length; i++)
                        {
                            cmd.Parameters.AddWithValue("@pizzaId", id);
                            cmd.Parameters.AddWithValue("@typeId", int.Parse(types[i]));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    Console.WriteLine("Product added successfully!");
                    sql.Close();
                }
            else
                Console.WriteLine("\nWrong input.\n");
        }

        public void DeleteById(int id)
        {
            using (SqlConnection sql = new(Program.DatabaseUrl))
            {
                sql.Open();
                SqlCommand deleteType = new("delete from PizzaTypes where PizzaID=" + id, sql);
                SqlCommand deleteIngredient = new("delete from PizzaIngredients where pizzaid=" + id, sql);
                SqlCommand deletePrice = new("delete from prices where pizzaid=" + id, sql);
                SqlCommand deleteName = new("delete from pizzas where id=" + id, sql);

                deleteType.ExecuteNonQuery();
                deleteIngredient.ExecuteNonQuery();
                deletePrice.ExecuteNonQuery();
                deleteName.ExecuteNonQuery();
                Console.WriteLine("\nProduct deleted successfully!\n");
                sql.Close();
            }
        }

        public string GetAll()
        {
            command = "select id,[Pizza Name] from AllPizzas group by id,[Pizza Name] order by id";
            return command;
        }

        public string GetIngredient(int id)
        {
            command = "select ingid,Ingredients from AllPizzas where id=" + id + " group by ingid,Ingredients order by ingid";
            return command;
        }

        public string GetPrice(int id)
        {
            command = "select Size,Price from AllPizzas where id=" + id + " group by Size,Price order by price ";
            return command;
        }

        public string GetType(int id)
        {
            command = "select [type id],Type from AllPizzas where id=" + id + " group by[type id], type order by[type id]";
            return command;
        }

    }
}
