using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface IExercise
    {
        public List<Exercise> GetAllExercises();
        public Exercise? GetExerciseById(int id);
        public Exercise CreateExercise(Exercise exercise);
        public void UpdateExercise(Exercise exercise);
        public void DeleteExercise(int id);
    }
}
