import { createFileRoute } from "@tanstack/react-router";
import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  getItemsAPI,
  updateItemAPI,
  deleteItemAPI,
  createItemAPI,
} from "../api/items";
import Pagination from "../components/Pagination";
import { useState } from "react";
import ItemTable from "../components/ItemTable";
import ItemModal from "../components/ItemModal";
import DeleteConfirmModal from "../components/DeleteConfirmModal";
export const Route = createFileRoute("/")({
  component: Index,
});

function Index() {
  const [open, setOpen] = useState(false);
  const [editingItem, setEditingItem] = useState(null);
  const [deleteItem, setDeleteItem] = useState(null);
  const [page, setPage] = useState(1);
  const pageSize = 2;
  const queryClient = useQueryClient();
  const deleteItemMutation = useMutation({
    mutationFn: deleteItemAPI,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["items"],
      });
    },
  });
  const updateItemMutation = useMutation({
    mutationFn: updateItemAPI,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["items"],
      });
    },
  });
  const createItemMutation = useMutation({
    mutationFn: createItemAPI,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["items"],
      });
    },
  });
  const { data, isLoading } = useQuery({
    queryKey: ["items", page, pageSize],
    queryFn: () => getItemsAPI(page, pageSize),
  });
  const totalPages = Math.ceil((data?.totalCount ?? 0) / pageSize);
  if (isLoading) {
    return <p>Loading...</p>;
  }
  const handleEdit = (item) => {
    setEditingItem(item);
    setOpen(true);
  };
  const handleAdd = () => {
    setEditingItem(null);
    setOpen(true);
  };
  const handleDelete = (item) => {
    setDeleteItem(item);
  };
  const handleDeleteConfirm = (id) => {
    deleteItemMutation.mutate(id);
  };
  const handleCreateConfirm = (item) => {
    createItemMutation.mutate(item);
  };
  const handleUpdateConfirm = (item) => {
    updateItemMutation.mutate(item);
  };
  return (
    <>
      <button className="btn btn-primary" onClick={handleAdd}>
        Add Item
      </button>

      <ItemTable
        data={data}
        handleEdit={handleEdit}
        handleDelete={handleDelete}
      />
      <Pagination page={page} totalPages={totalPages} setPage={setPage} />
      {open && (
        <ItemModal
          editingItem={editingItem}
          onClose={() => {
            setOpen(false);
          }}
          handleUpdateConfirm={handleUpdateConfirm}
          handleCreateConfirm={handleCreateConfirm}
          isPending={
            createItemMutation.isPending || updateItemMutation.isPending
          }
        />
      )}
      {deleteItem && (
        <DeleteConfirmModal
          deleteItem={deleteItem}
          onClose={() => setDeleteItem(null)}
          handleDeleteConfirm={handleDeleteConfirm}
        />
      )}
    </>
  );
}
