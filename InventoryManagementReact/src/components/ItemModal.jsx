import { useForm } from "@tanstack/react-form";

function ItemModal({
  editingItem,
  onClose,
  handleUpdateConfirm,
  handleCreateConfirm,
  isPending,
}) {
  const form = useForm({
    defaultValues: {
      name: editingItem?.name ?? "",
      price: editingItem?.price ?? 0,
      //    quantity: editingItem?.quantity ?? 0,
    },

    onSubmit: async ({ value }) => {
      try {
        if (editingItem) {
          handleUpdateConfirm({
            id: editingItem.id,
            ...value,
          });
        } else {
          handleCreateConfirm(value);
        }
        onClose();
      } catch (err) {
        console.error(err);
      }
    },
  });
  return (
    <dialog className="modal modal-open">
      <div className="modal-box">
        <h3 className="font-bold text-lg">
          {editingItem ? "Edit Item" : "Add Item"}
        </h3>

        {/* Form goes here */}
        <form
          onSubmit={(e) => {
            e.preventDefault();
            form.handleSubmit();
          }}
        >
          <form.Field name="name">
            {(field) => (
              <div className="mb-4">
                <label className="block mb-1">Name</label>
                <input
                  className="input input-bordered w-full"
                  value={field.state.value}
                  onChange={(e) => field.handleChange(e.target.value)}
                />
              </div>
            )}
          </form.Field>

          <form.Field name="price">
            {(field) => (
              <div className="mb-4">
                <label className="block mb-1">Price</label>
                <input
                  type="number"
                  className="input input-bordered w-full"
                  value={field.state.value}
                  onChange={(e) => field.handleChange(Number(e.target.value))}
                />
              </div>
            )}
          </form.Field>

          {/* <form.Field name="quantity">
            {(field) => (
              <div className="mb-4">
                <label className="block mb-1">Quantity</label>
                <input
                  type="number"
                  className="input input-bordered w-full"
                  value={field.state.value}
                  onChange={(e) => field.handleChange(Number(e.target.value))}
                />
              </div>
            )}
          </form.Field> */}
          <div className="modal-action">
            <button type="button" className="btn" onClick={onClose}>
              Cancel
            </button>

            <button
              type="submit"
              className="btn btn-primary"
              disabled={isPending}
            >
              {isPending ? "Saving..." : "Save"}
            </button>
          </div>
        </form>
        {/* form end */}
      </div>
    </dialog>
  );
}
export default ItemModal;
