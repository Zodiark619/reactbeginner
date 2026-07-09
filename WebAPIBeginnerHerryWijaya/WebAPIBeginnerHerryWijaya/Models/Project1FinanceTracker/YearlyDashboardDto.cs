namespace WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker
{
    public class YearlyDashboardDto
    {
        public int Year { get; set; }

        public decimal TotalIncome { get; set; }
        public decimal TotalExpense { get; set; }
        public decimal Balance => TotalIncome - TotalExpense;

        public List<MonthlySummaryDto> MonthlySummaries { get; set; } = [];
    }
}
