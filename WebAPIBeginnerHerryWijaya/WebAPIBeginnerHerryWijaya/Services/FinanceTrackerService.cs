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

        public async Task<MonthlyDashboardDto> GetMonthlyDashboardAsync(int year, int month)
        {
            var monthlyFinance = await _financeTrackerRepository.GetByMonthAsync(year,month);

            var totalIncome = monthlyFinance
    .Where(f => f.FinanceType == Constant.Income)
    .Sum(f => f.Amount);

            var totalExpense = monthlyFinance
                .Where(f => f.FinanceType == Constant.Expense)
                .Sum(f => f.Amount);

            var recentTransactions = monthlyFinance
                .OrderByDescending(f => f.TransactionDate)
                .Take(5)
                .Select(f => new RecentTransactionDto
                {
                    Id= f.Id,
                    TransactionDate = f.TransactionDate,
                    Amount = f.Amount,
                    FinanceType = f.FinanceType,
                    Category = f.Category,
                    Description = f.Description
                })
                .ToList();
             
            var monthlyDashboard = new MonthlyDashboardDto
            {
                Year = year,
                Month = month,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                RecentTransactions = recentTransactions,
                TransactionCount = monthlyFinance.Count()
            };
            return monthlyDashboard;
        }

        public async Task<YearlyDashboardDto> GetYearlyDashboardAsync(int year)
        {
            var yearFinance=await _financeTrackerRepository.GetByYearAsync(year);
            var totalIncome = yearFinance
    .Where(f => f.FinanceType ==Constant.Income)
    .Sum(f => f.Amount);

            var totalExpense = yearFinance
                .Where(f => f.FinanceType == Constant.Expense)
                .Sum(f => f.Amount);
            var monthlySummaries = yearFinance
    .GroupBy(f => f.TransactionDate.Month)
    .Select(g => new MonthlySummaryDto
    {
        Year = year,
        Month = g.Key,
        Income = g.Where(f => f.FinanceType == Constant.Income)
                  .Sum(f => f.Amount),

        Expense = g.Where(f => f.FinanceType == Constant.Expense)
                   .Sum(f => f.Amount),
    })
    .ToList();
            var yearlyDashboard = new YearlyDashboardDto
            {
                Year = year,
                TotalIncome = totalIncome,
                TotalExpense = totalExpense,
                MonthlySummaries = monthlySummaries
            };  
            return yearlyDashboard;
        }
    }
}
