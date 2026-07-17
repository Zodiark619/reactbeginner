import { createFileRoute } from "@tanstack/react-router";
import { useQuery } from "@tanstack/react-query";
import { getItems } from "../api/items";
import Pagination from "../components/Pagination";
import { useState } from "react";
import ItemTable from "../components/ItemTable";
export const Route = createFileRoute("/")({
  component: Index,
});

function Index() {
  const [page, setPage] = useState(1);
  const pageSize = 2;
  const { data, isLoading } = useQuery({
    queryKey: ["items", page, pageSize],
    queryFn: () => getItems(page, pageSize),
  });
  const totalPages = Math.ceil((data?.totalCount ?? 0) / pageSize);
  if (isLoading) {
    return <p>Loading...</p>;
  }
  console.log(data);
  return (
    <>
      <ItemTable data={data} />
      <Pagination page={page} totalPages={totalPages} setPage={setPage} />
    </>
  );
}
