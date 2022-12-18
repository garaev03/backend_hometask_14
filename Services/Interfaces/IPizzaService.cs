using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hometask_14.Services.Interfaces
{
    internal interface IPizzaService : IRepositoryForCategories
    {
        public void AddProduct(string name, string[] sizes, decimal[] prices, string[] ingredients,string[] types);
    }
}
