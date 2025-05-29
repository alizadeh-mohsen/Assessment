using Assessment.Domain.Interfaces;
using Assessment.Infra.Data.Context;

namespace Assessment.Infra.Data.Repository
{
    public class WorkingDaysRepository : IWorkingDaysRepository
    {
        private AssessmentDBContext _context;

        public WorkingDaysRepository(AssessmentDBContext ctx)
        {
            _context = ctx;
        }

        public HashSet<DateTime> GetPublicHolidays()
        {
            var holidays =  _context.PublicHolidays.Select(h => h.Date.Date).ToHashSet();
            return holidays;
        }
    }
}