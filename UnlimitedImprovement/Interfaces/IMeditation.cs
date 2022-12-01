using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface IMeditation
    {
        public List<Meditation> GetAllMeditations();
        public Meditation CreateMeditation(Meditation meditation);
        public void UpdateMeditation(Meditation meditation);
        public void DeleteMeditation(int id);
    }
}
