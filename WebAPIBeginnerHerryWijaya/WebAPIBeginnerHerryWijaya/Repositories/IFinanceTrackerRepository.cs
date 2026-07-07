using WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker;

namespace WebAPIBeginnerHerryWijaya.Repositories
{
    public interface IFinanceTrackerRepository
    {
        Task<IEnumerable<Finance>> GetAllAsync();
        Task AddAsync(Finance finance);

        Task SaveChangesAsync();    
    }
}
