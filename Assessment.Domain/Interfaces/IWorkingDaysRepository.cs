namespace Assessment.Domain.Interfaces
{
    public interface IWorkingDaysRepository
    {
        HashSet<DateTime> GetPublicHolidays();
    }
}
