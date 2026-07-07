using Microsoft.EntityFrameworkCore;
using WebAPIBeginnerHerryWijaya.Data;
using WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker;

namespace WebAPIBeginnerHerryWijaya.Repositories
{
    public class FinanceTrackerRepository : IFinanceTrackerRepository
    {
        private readonly ApplicationDbContext _applicationDb;

        public FinanceTrackerRepository(ApplicationDbContext applicationDb)
        {
            _applicationDb = applicationDb;
        }
        public async Task AddAsync(Finance finance)
        {
            await _applicationDb.Finances.AddAsync(finance);
       
        }

        public async Task<IEnumerable<Finance>> GetAllAsync()
        {
           var finances =await _applicationDb.Finances.ToListAsync();
            return  finances;
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDb.SaveChangesAsync();
        }
    }
}
