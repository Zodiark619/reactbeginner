function DeleteConfirmModal({ deleteItem, onClose, handleDeleteConfirm }) {
  return (
    <dialog className="modal modal-open">
      <div className="modal-box">
        <h3 className="text-lg font-bold text-error">Delete Item</h3>

        <p className="py-4">
          Are you sure you want to delete <strong>{deleteItem.name}</strong>?
        </p>

        <div className="modal-action">
          <button className="btn" onClick={onClose}>
            Cancel
          </button>

          <button
            className="btn btn-error"
            onClick={() => {
              handleDeleteConfirm(deleteItem.id);
              onClose();
            }}
          >
            Delete
          </button>
        </div>
      </div>
    </dialog>
  );
}

export default DeleteConfirmModal;
