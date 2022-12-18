using hometask_14.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hometask_14.Services.Concrets
{
    internal class ComboMenuService : IComboMenusService
    {
        string? command;

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public string GetAll()
        {
            command = "select id,[name] from ComboAllInfo group by id,[name] order by id";
            return command;
        }

        public string GetIngredient(int id)
        {
            command = "select inid,Ingredients from ComboAllInfo where id="+id+" order by id";
            return command;
        }

        public string GetPrice(int id)
        {
            command = "select price from ComboAllInfo where id="+id+" group by price";
            return command;
        }

        public string GetType(int id)
        {
            command = "select typeid,type from comboallinfo where id="+id+" group by typeid,type order by typeid";
            return command;
        }
    }
}
