import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
  generateRandom,
  getFinances,
  deleteFinance,
  getMonthlyDashboardFinance,
  getYearlyDashboardFinance,
} from "./api/financeTrackerAPI";
import { useCallback, useMemo, useState } from "react";
import {
  createColumnHelper,
  useReactTable,
  getCoreRowModel,
  flexRender,
} from "@tanstack/react-table";
import { currencyFormatter } from "./utilities";

import TextField from "@mui/material/TextField";
import DataTable from "./DataTable";
import { Button, MenuItem, Select } from "@mui/material";
import YearlyDashboardDataTable from "./components/YearlyDashboardDataTable";
import { MONTHS } from "./constants";
import MonthlyDashboardDataTable from "./components/MonthlyDashboardDataTable";
const columnHelper = createColumnHelper();

function FinanceTrackerAPI() {
  const [page, setPage] = useState(0); // MUI uses 0-based pages
  const [rowsPerPage, setRowsPerPage] = useState(20);
  const [count, setCount] = useState(2);
  const [generatedFinances, setGeneratedFinances] = useState([]);
  const { data, isLoading, error } = useQuery({
    queryKey: ["finances", page, rowsPerPage],
    queryFn: () => getFinances(page + 1, rowsPerPage), // GET /api/finance
  });
  const finances = data?.items ?? [];
  const totalCount = data?.totalCount ?? 0;
  const queryClient = useQueryClient();

  const generateMutation = useMutation({
    mutationFn: generateRandom,
    onSuccess: (data) => {
      setGeneratedFinances(data);
      queryClient.invalidateQueries({
        queryKey: ["finances"],
      });
    },
  });
  const deleteMutation = useMutation({
    mutationFn: deleteFinance,
    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["finances"],
      });
    },
  });
  const handleDelete = useCallback(
    (id) => {
      deleteMutation.mutate(id);
    },
    [deleteMutation],
  );
  //
  const [year, setYear] = useState(new Date().getFullYear());

  const { data: yearlyDashboard, refetch: fetchYearlyDashboard } = useQuery({
    queryKey: ["yearlyDashboard", year],
    queryFn: () => getYearlyDashboardFinance(year),
    enabled: false,
  });
  const [month, setMonth] = useState(new Date().getMonth() + 1);

  const { data: monthlyDashboard, refetch: fetchMonthlyDashboard } = useQuery({
    queryKey: ["monthlyDashboard", year, month],
    queryFn: () => getMonthlyDashboardFinance(year, month),
    enabled: false,
  });

  //
  const columns = useMemo(
    () => [
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
      columnHelper.display({
        id: "actions",
        header: "Actions",
        cell: ({ row }) => (
          <button onClick={() => handleDelete(row.original.id)}>Delete</button>
        ),
      }),
    ],
    [handleDelete],
  );

  if (isLoading) return <p>Loading...</p>;

  if (error) return <p>Something went wrong.</p>;

  return (
    <>
      {generatedFinances && (
        <div>
          <h2>Generated Random Finances</h2>
          <DataTable data={generatedFinances} columns={columns} />
        </div>
      )}
      {yearlyDashboard && <YearlyDashboardDataTable data={yearlyDashboard} />}
      {monthlyDashboard && (
        <MonthlyDashboardDataTable data={monthlyDashboard} />
      )}

      <br></br>
      <div>
        <TextField
          type="number"
          label="Number of records"
          value={count}
          onChange={(e) => setCount(Number(e.target.value))}
        />
        <button onClick={() => generateMutation.mutate(count)}>Generate</button>
      </div>
      <br></br>
      <div>
        <Select value={year} onChange={(e) => setYear(e.target.value)}>
          <MenuItem value={2025}>2025</MenuItem>
          <MenuItem value={2026}>2026</MenuItem>
        </Select>
        <Button onClick={() => fetchYearlyDashboard()}>
          Get Yearly Dashboard
        </Button>
      </div>
      <div>
        <Select value={month} onChange={(e) => setMonth(e.target.value)}>
          <MenuItem value={1}>January</MenuItem>
          {MONTHS.map((month) => (
            <MenuItem key={month.value} value={month.value}>
              {month.label}
            </MenuItem>
          ))}
        </Select>

        <Button onClick={() => fetchMonthlyDashboard()}>
          Get Monthly Dashboard
        </Button>
      </div>

      <hr></hr>
      <h2>All Finances</h2>
      <DataTable
        data={finances}
        columns={columns}
        totalCount={totalCount}
        page={page}
        rowsPerPage={rowsPerPage}
        onPageChange={setPage}
        onRowsPerPageChange={setRowsPerPage}
      />
    </>
  );
}

export default FinanceTrackerAPI;
