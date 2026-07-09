import { createColumnHelper } from "@tanstack/react-table";
import DataTable from "../DataTable";
import { currencyFormatter } from "../utilities";
const columnHelper = createColumnHelper();
const monthlySummaryColumns = [
  columnHelper.accessor("month", {
    header: "Month",
  }),
  columnHelper.accessor("income", {
    header: "Income",
  }),
  columnHelper.accessor("expense", {
    header: "Expense",
  }),
  columnHelper.accessor("balance", {
    header: "Balance",
  }),
];
function YearlyDashboardDataTable({ data }) {
  return (
    <div>
      <h2>{data.year} Yearly Dashboard</h2>
      <div>
        <p>
          Total Income:{currencyFormatter.format(data.totalIncome)} Total
          Expense:
          {currencyFormatter.format(data.totalExpense)} (Balance:
          {currencyFormatter.format(data.balance)})
        </p>
      </div>
      <DataTable data={data.monthlySummaries} columns={monthlySummaryColumns} />
    </div>
  );
}

export default YearlyDashboardDataTable;
