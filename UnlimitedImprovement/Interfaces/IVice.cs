using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface IVice
    {
        public List<Vice> GetAllVices();
        public Vice? GetViceById(int id);
        public Vice CreateVice(Vice vice);
        public void UpdateVice(Vice vice);
        public void DeleteVice(int id);
    }
}
