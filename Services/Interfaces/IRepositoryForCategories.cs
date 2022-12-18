using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hometask_14.Services.Interfaces
{
    internal interface IRepositoryForCategories:IRestoranService
    {
        public string GetPrice(int id);
        public string GetType(int id);
        public string GetIngredient(int id);
        public void DeleteById(int id);
    }
}
