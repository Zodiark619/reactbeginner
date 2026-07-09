using WebAPIBeginnerHerryWijaya.Controllers;
using WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker;

namespace WebAPIBeginnerHerryWijaya.Repositories
{
    public interface IFinanceTrackerRepository
    {
        Task<PagedResult<Finance>> GetAllAsync(int page, int pageSize);
        Task AddAsync(Finance finance);
        Task<bool> DeleteAsync(int id);
        Task SaveChangesAsync();
        Task<List<Finance>> GetByYearAsync(int year);

        Task<List<Finance>> GetByMonthAsync(int year, int month);
    }
}
