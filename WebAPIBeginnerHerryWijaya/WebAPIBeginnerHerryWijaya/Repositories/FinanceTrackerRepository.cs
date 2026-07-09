using Microsoft.EntityFrameworkCore;
using WebAPIBeginnerHerryWijaya.Controllers;
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

        public async Task<PagedResult<Finance>> GetAllAsync(int page, int pageSize)
        {
            var query = _applicationDb.Finances.AsQueryable();

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderBy(f => f.Id) // Always order before Skip()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PagedResult<Finance>
            {
                Items = items,
                TotalCount = totalCount
            };
        }

        public async Task<List<Finance>> GetByMonthAsync(int year, int month)
        {
            return await _applicationDb.Finances
         .Where(f => f.TransactionDate.Year == year && f.TransactionDate.Month == month)
         .ToListAsync();
        }

        public async Task<List<Finance>> GetByYearAsync(int year)
        {
            return await _applicationDb.Finances
        .Where(f => f.TransactionDate.Year == year)
        .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDb.SaveChangesAsync();
        }
    }
}
