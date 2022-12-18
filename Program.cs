using hometask_14.Services.Concrets;
using hometask_14.Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Cryptography;

namespace hometask_14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string errorValid = "please enter valid key!";
            ICategoryService categoryService = new CategoryService();
            IPizzaService pizzaService = new PizzaService();
            IComboMenusService comboMenuService = new ComboMenuService();

            while (true)
            {

            Categories:
                Console.WriteLine("\n-------------------------------------------");
                GetInfos(categoryService.GetAll());
                Console.WriteLine("0. Exit App\n-------------------------------------------\n");
                Console.Write("Enter Category: "); int value = int.Parse(Console.ReadLine());
                Console.WriteLine();
            Products:
                switch (value)
                {
                    case 0:
                        return;
                    case 1:
                        GetInfos(pizzaService.GetAll());
                        break;
                    case 2:
                        GetInfos(comboMenuService.GetAll());
                        break;
                    default:
                        Console.WriteLine(errorValid);
                        goto Categories;
                }

                Console.WriteLine("\n-1. Add product\n-2. Delete Product\n0. Go back\n");
                Console.Write("\nEnter product: "); int productId = int.Parse(Console.ReadLine());

                if (productId == 0)
                    goto Categories;
                else if (productId == -1)
                {
                    string[] sizes = new string[0];
                    decimal[] prices = new decimal[0];

                    Console.Write("\nEnter new product name: ");
                    string? prodname = Console.ReadLine();
                    categoryService.GetAllIngredients();

                    Console.Write("\nEnter ingredients: ");
                    string? ingredinetsString = Console.ReadLine();
                    string[] ingredients = ingredinetsString.Split(' ');

                    if (value == 1)
                    {
                        Console.WriteLine("\nSelect size to enter price\n");
                        categoryService.GetAllSizes();
                        Console.WriteLine("\nWhich sizes you want to enter?");
                        Console.Write("Enter size: ");
                        string? sizesString = Console.ReadLine();
                        sizes = sizesString.Split(' ');
                        prices = new decimal[sizes.Length];
                        for (int i = 0; i < prices.Length; i++)
                        {
                            Console.Write("Enter price for size number " + sizes[i] + ": ");
                            prices[i] = Convert.ToDecimal(Console.ReadLine());
                        }
                    }


                    Console.WriteLine("\nEnter type:");
                    categoryService.GetAllTypes();
                    Console.WriteLine("\nif you dont want to enter type press 0");
                    Console.Write("\nEnter type(s): ");
                    string[] types = Console.ReadLine().Split(' ');
                    if (types[0] == "0")
                        types = null;

                    if (value == 1)
                        pizzaService.AddProduct(prodname, sizes, prices, ingredients, types);



                    goto Products;
                }
                else if (productId == -2)
                {
                    Console.WriteLine("\nIf you want go back press 0");
                    Console.Write("Enter product id you want to delete: ");
                    productId = int.Parse(Console.ReadLine());

                    if (value == 1 && productId != 0)
                        pizzaService.DeleteById(productId);

                    goto Products;
                }

            ProductsInside:
                Console.WriteLine("\n1. Price\n2. Type\n3. Ingredients\n0. Go back\n");
                Console.Write("\nEnter key: "); int value2 = int.Parse(Console.ReadLine());
                switch (value2)
                {
                    case 0:
                        goto Products;
                    case 1:
                        if (value == 1)
                            GetInfos(pizzaService.GetPrice(productId));
                        else if (value == 2)
                            GetInfos(comboMenuService.GetPrice(productId));
                        goto ProductsInside;
                    case 2:
                        if (value == 1)
                            GetInfos(pizzaService.GetType(productId));
                        else if (value == 2)
                            GetInfos(comboMenuService.GetType(productId));
                        goto ProductsInside;
                    case 3:
                        if (value == 1)
                            GetInfos(pizzaService.GetIngredient(productId));
                        else if (value == 2)
                            GetInfos(comboMenuService.GetIngredient(productId));
                        goto ProductsInside;
                    default:
                        Console.WriteLine(errorValid);
                        goto ProductsInside;
                }

            }
        }

        public static string DatabaseUrl = "Data Source=SS;Database=PizzaMizzaDB;Trusted_Connection=True;";

        public static void GetInfos(string sqlCommand)
        {
            using (SqlConnection connection = new SqlConnection(DatabaseUrl))
            {
                connection.Open();

                SqlCommand cmd = new(sqlCommand, connection);

                try
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        bool _exist = false;
                        int i = 1;
                        while (reader.Read())
                        {
                            _exist = true;
                            if (sqlCommand.ToLower().Contains("from allpizzas"))
                            {
                                if (sqlCommand.ToLower().Contains("[pizza name]"))
                                    Console.WriteLine(reader.GetInt32(0) + ". " + reader.GetString(1));
                                else if (sqlCommand.ToLower().Contains("size,price"))
                                {
                                    Console.Write(reader.GetString(0) + "-----");
                                    Console.WriteLine(reader.GetDecimal(1));
                                }
                                else
                                    Console.WriteLine(reader.GetInt32(0) + ". " + reader.GetString(1));
                            }

                            else if (sqlCommand.ToLower().Contains("from comboallinfo"))
                            {
                                if (sqlCommand.ToLower().Contains("price"))
                                    Console.WriteLine(reader.GetDecimal(0));

                                else
                                    Console.WriteLine(reader.GetInt32(0) + ". " + reader.GetString(1));
                            }

                            else if (sqlCommand.ToLower().Contains("from categories"))
                                Console.WriteLine(reader.GetInt32(0) + ". " + reader.GetString(1));
                        }

                        if (!_exist)
                            Console.WriteLine("there is no information in the table.");
                    }
                }
                catch
                {
                    Console.WriteLine("table doesn't exist.");
                }
                connection.Close();
            }
        }
    }
}
