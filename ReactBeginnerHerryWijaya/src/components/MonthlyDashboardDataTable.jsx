import { createColumnHelper } from "@tanstack/react-table";
import DataTable from "../DataTable";
import { MONTHS } from "../constants";
import { currencyFormatter } from "../utilities";
const columnHelper = createColumnHelper();
const recentTransactionsColumns = [
  columnHelper.accessor("financeType", {
    header: "Finance Type",
    cell: (info) => info.getValue(),
  }),
  columnHelper.accessor("description", {
    header: "Description",
    cell: (info) => info.getValue(),
  }),
  columnHelper.accessor("category", {
    header: "Category",
    cell: (info) => info.getValue(),
  }),
  columnHelper.accessor("amount", {
    header: "Amount",
    cell: (info) => currencyFormatter.format(info.getValue()),
  }),
  columnHelper.accessor("transactionDate", {
    header: "Transaction Date",
    cell: (info) => new Date(info.getValue()).toLocaleDateString(),
  }),
];
function MonthlyDashboardDataTable({ data }) {
  const monthName =
    MONTHS.find((m) => m.value === data.month)?.label ?? "Unknown";
  return (
    <div>
      <h2>
        {monthName} {data.year} Monthly Dashboard
      </h2>
      <div>
        <p>
          Total Income:{currencyFormatter.format(data.totalIncome)} Total
          Expense:
          {currencyFormatter.format(data.totalExpense)} (Balance:
          {currencyFormatter.format(data.balance)} on {data.transactionCount}{" "}
          transactions)
        </p>
      </div>
      <h3>Recent Transactions</h3>
      <DataTable
        data={data.recentTransactions}
        columns={recentTransactionsColumns}
      />
    </div>
  );
}

export default MonthlyDashboardDataTable;
