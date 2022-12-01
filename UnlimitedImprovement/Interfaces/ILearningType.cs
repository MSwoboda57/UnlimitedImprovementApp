using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface ILearningType
    {
        public List<LearningType> GetAllLearningTypes();
        public LearningType? GetLearningTypeById(int id);
        public LearningType CreateLearningType(LearningType learningType);
        public void UpdateLearningType(LearningType learningType);
        public void DeleteLearningType(int id);
    }
}
