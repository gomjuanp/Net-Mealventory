// TODO: This has to be changed to use database

using Mealventory.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mealventory.Core.Interfaces
{
    public interface IFoodRepository
    {
        IEnumerable<FoodItem> GetAll();

        FoodItem GetById(int id);

        FoodItem Add(FoodItem item);

        FoodItem Update(FoodItem item);

        void Delete(int id);
    }
}
