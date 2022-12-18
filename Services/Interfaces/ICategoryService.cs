using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hometask_14.Services.Interfaces
{
    internal interface ICategoryService : IRestoranService
    {
        public void GetAllIngredients();
        public void GetAllSizes();
        public void GetAllTypes();
    }
}
