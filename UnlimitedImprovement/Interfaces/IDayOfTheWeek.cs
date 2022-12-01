using UnlimitedImprovement.Models;

namespace UnlimitedImprovement.Interfaces
{
    public interface IDayOfTheWeek
    {
        public List<DaysOfTheWeek> GetAllDayOfTheWeek();
        public DaysOfTheWeek? GetDayOfTheWeekById(int id);
        public DaysOfTheWeek CreateDayOfTheWeek(DaysOfTheWeek dayOfTheWeek);
        public void UpdateDayOfTheWeek(DaysOfTheWeek dayOfTheWeek);
        public void DeleteDayOfTheWeek(int id);
    }
}
