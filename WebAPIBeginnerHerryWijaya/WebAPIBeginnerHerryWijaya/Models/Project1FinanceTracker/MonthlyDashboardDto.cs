namespace WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker
{
    public class MonthlyDashboardDto
    {
        public int Year { get; set; }
        public int Month { get; set; }

        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;

        public int TransactionCount { get; set; }

        public List<RecentTransactionDto> RecentTransactions { get; set; } = [];
    }
}
