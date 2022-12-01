using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface IExerciseDayOfTheWeek
    {
        public List<ExerciseDayOfTheWeek> GetAllExerciseDayOfTheWeek();
        public ExerciseDayOfTheWeek? GetExerciseDayOfTheWeekById(int id);
        public ExerciseDayOfTheWeek CreateExerciseDayOfTheWeek(ExerciseDayOfTheWeek exerciseDayOfTheWeek);
        public void UpdateExerciseDayOfTheWeek(ExerciseDayOfTheWeek exerciseDayOfTheWeek);
        public void DeleteExerciseDayOfTheWeek(int id);
    }
}
