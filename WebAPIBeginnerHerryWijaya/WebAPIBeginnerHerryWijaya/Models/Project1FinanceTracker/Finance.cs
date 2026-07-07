using System.ComponentModel.DataAnnotations;

namespace WebAPIBeginnerHerryWijaya.Models.Project1FinanceTracker
{
    public class Finance
    {
        public int Id { get; set; }
     
        public string FinanceType { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
   
        public string Category { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
