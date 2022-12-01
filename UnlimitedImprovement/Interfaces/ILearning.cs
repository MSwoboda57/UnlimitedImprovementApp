using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface ILearning
    {
        public List<Learning> GetAllLearning();
        public Learning? GetLearningById(int id);
        public Learning CreateLearning(Learning learning);
        public void UpdateLearning(Learning learning);
        public void DeleteLearning(int id);
    }
}
