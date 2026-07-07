using WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker;
using WebAPIBeginnerHerryWijaya.Repositories;
using WebAPIBeginnerHerryWijaya.Utilities;

namespace WebAPIBeginnerHerryWijaya.Services
{
    public class FinanceTrackerService : IFinanceTrackerService
    {
        private readonly IFinanceTrackerRepository _financeTrackerRepository;
        private readonly Random _random; 
        private static readonly string[] _financesTypes = Constant.FinanceTypes ;
        private static readonly string[] _categoriesExpense = Constant.CategoriesExpense ;
        private static readonly string[] _categoriesIncome = Constant.CategoriesIncome ;
        public FinanceTrackerService(IFinanceTrackerRepository financeTrackerRepository)
        {
            _financeTrackerRepository = financeTrackerRepository;
            _random = new Random();
        }

        public async Task<Finance> GenerateRandomFinance()
        {
         var finance = new Finance
            {
                Amount = _random.Next(1, 10000),
                FinanceType=  _financesTypes[_random.Next(_financesTypes.Length)] ,
             TransactionDate = DateTime.Today.AddDays(-_random.Next(365))
         };
                finance.Category = finance.FinanceType == "Income" ? _categoriesIncome[_random.Next(_categoriesIncome.Length)] : _categoriesExpense[_random.Next(_categoriesExpense.Length)];
            finance.Description =
      $"Randomly generated {finance.FinanceType} transaction of {finance.Amount:C} for {finance.Category} on {finance.TransactionDate:d}.";

            await  _financeTrackerRepository.AddAsync(finance);
            return finance;
        }

        public async Task<IEnumerable<Finance>> SeedFinance(int count)
        {
            var finances = new List<Finance>();
            for(int i = 0; i < count; i++)
            {
              finances.Add( await  GenerateRandomFinance());
            }
            await _financeTrackerRepository.SaveChangesAsync();
            return finances;
        }
    }
}
