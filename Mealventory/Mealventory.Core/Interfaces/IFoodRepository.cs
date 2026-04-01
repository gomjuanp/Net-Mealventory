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
        IEnumerable<FoodItem> GetAll(int userId);

        FoodItem? GetById(int id, int userId);

        FoodItem Add(FoodItem item);

        FoodItem? Update(FoodItem item);

        void Delete(int id, int userId);
    }
}
