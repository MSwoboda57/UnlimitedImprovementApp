using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface INewIdea
    {
        public List<NewIdeas> GetAllNewIdeas();
        public NewIdeas CreateNewIdea(NewIdeas newIdea);
        public void UpdateNewIdea(NewIdeas newIdea);
        public void DeleteNewIdea(int id);

    }
}
