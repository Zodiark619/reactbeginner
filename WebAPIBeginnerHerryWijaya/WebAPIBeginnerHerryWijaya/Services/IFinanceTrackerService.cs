using WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker;

namespace WebAPIBeginnerHerryWijaya.Services
{
    public interface IFinanceTrackerService
    {
        Task<IEnumerable<Finance>> SeedFinance(int count);
        Task<Finance> GenerateRandomFinance();
    }
}
