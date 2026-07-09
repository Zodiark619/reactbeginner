using Stripe;
using Stripe.Climate;
using System.Diagnostics;

namespace WebAPIBeginnerHerryWijaya.Utilities
{
    public static  class Constant
    {
        public const string Income = "Income";
        public const string Expense = "Expense";
        public static readonly string[] FinanceTypes = new string[]
        {
            "Income",
            "Expense"
        };
        public static readonly string[] CategoriesExpense = new string[]
        {
            "Food",
            "Transportation",
            "Housing",
            "Utilities",
            "Phone",
            "Shopping",
            "Entertainment",
            "Healthcare",
            "Education",
            "Fitness",
            "Travel",
            "Pets",
            "Gifts",
            "Work",
            "Taxes",
            "Loan",
            "Miscellaneous"
        };
        public static readonly string[] CategoriesIncome = new string[]
        {
            "Salary",
    "Bonus",
    "Freelance",
    "Investment",
    "Interest",
    "Rental",
    "Gift",
    "Refund",
    "Other"
        };
    }
}
