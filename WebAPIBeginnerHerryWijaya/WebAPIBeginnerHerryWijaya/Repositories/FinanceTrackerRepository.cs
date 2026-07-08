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

        public async Task<bool> DeleteAsync(int id)
        {
            var finance = _applicationDb.Finances.FirstOrDefault(f => f.Id == id);
            if (finance == null)
            {
                return false; // or throw a custom NotFound exception
            }
            _applicationDb.Finances.Remove(finance);
            await _applicationDb.SaveChangesAsync();
            return true;
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
