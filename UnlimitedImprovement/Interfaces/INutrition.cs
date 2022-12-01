using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface INutrition
    {
        public List<Nutrition> GetAllNutrition();
        public Nutrition? GetNutritionById(int id);
        public Nutrition CreateNutrition(Nutrition nutrition);
        public void UpdateNutrition(Nutrition nutrition);
        public void DeleteNutrition(int id);
    }
}
