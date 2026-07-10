namespace WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker
{
    public class FinanceQuery
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
        public string? Search { get; set; }
        public string? FinanceType { get; set; }
        public string? Category { get; set; }
        public string? SortBy { get; set; } = "Id";
        public string? SortOrder { get; set; } = "asc";
    }
}
