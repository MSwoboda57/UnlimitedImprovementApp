using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface INutritionDayOfTheWeek
    {
        public List<NutritionDayOfTheWeek> GetAllNutritionDayOfTheWeek();
        public NutritionDayOfTheWeek? GetNutritionDayOfTheWeekById(int id);
        public NutritionDayOfTheWeek CreateNutritionDayOfTheWeek(NutritionDayOfTheWeek nutritionDayOfTheWeek);
        public void UpdateNutritionDayOfTheWeek(NutritionDayOfTheWeek nutritionDayOfTheWeek);
        public void DeleteNutritionDayOfTheWeek(int id);
    }
}
