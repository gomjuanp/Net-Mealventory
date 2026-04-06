// Owner 1: "Juan Pablo Ordonez Gomez" has added 92% of the code in this file
// Owner 2: "Daniel Bajenov" has added 8% of the code in this file


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

        IEnumerable<FoodItem> GetByUser(int userId);

        FoodItem? GetById(int id, int userId);

        FoodItem Add(FoodItem item);

        FoodItem? Update(FoodItem item);

        void Delete(int id, int userId);
    }
}