import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import {
  generateRandom,
  getFinances,
  deleteFinance,
} from "./api/financeTrackerAPI";
import { useCallback, useMemo, useState } from "react";
import {
  createColumnHelper,
  useReactTable,
  getCoreRowModel,
  flexRender,
} from "@tanstack/react-table";
import TextField from "@mui/material/TextField";
import DataTable from "./DataTable";
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
        cell: (info) => info.getValue(),
      }),
      columnHelper.accessor("transactionDate", {
        header: "Transaction Date",
        cell: (info) => info.getValue(),
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
      <h2>Generated Finances</h2>
      <DataTable data={generatedFinances} columns={columns} />
      {/* <button onClick={() => generate Mutation.mutate(2)}>Generate 2</button>; */}
      <TextField
        type="number"
        label="Number of records"
        value={count}
        onChange={(e) => setCount(Number(e.target.value))}
      />
      <button onClick={() => generateMutation.mutate(count)}>Generate</button>
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
