import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { GetUserCart, UpdateCart } from "../../api/stripe/cart";
import { useEffect, useState } from "react";
import { Checkout } from "../../api/stripe/checkout";

export default function CartPage() {
  const queryClient = useQueryClient();

  const { data, isLoading, refetch } = useQuery({
    queryKey: ["carts"],
    queryFn: () => GetUserCart(),
  });
  const [cartItems, setCartItems] = useState([]);

  useEffect(() => {
    if (data) {
      setCartItems(data.items);
    }
  }, [data]);
  const removeItem = (productId) => {
    setCartItems((items) =>
      items.map((item) =>
        item.productId === productId ? { ...item, removed: true } : item,
      ),
    );
  };
  ////////////////////////

  const totalPrice = cartItems.reduce(
    (total, item) => total + item.unitPrice * item.quantity,
    0,
  );
  const updateQuantity = (productId, quantity) => {
    setCartItems((items) =>
      items.map((item) =>
        item.productId === productId
          ? { ...item, quantity: Math.max(0, quantity) }
          : item,
      ),
    );
  };

  const updateCartMutation = useMutation({
    mutationFn: UpdateCart,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["carts"],
      });
    },
  });
  const checkoutMutation = useMutation({
    mutationFn: Checkout,

    onSuccess: (data) => {
      window.location.href = data.url;
    },
  });
  const handleUpdateCart = async (cartItems) => {
    const cartDto = {
      items: cartItems.map((item) => ({
        productId: item.productId,
        quantity: item.quantity,
      })),
    };
    await updateCartMutation.mutate(cartDto);
  };
  const handleRevert = async () => {
    const result = await refetch();

    if (result.data) {
      setCartItems(result.data.items);
    }
  };
  const handleCheckout = async (cartItems) => {
    handleUpdateCart(cartItems);

    const buyRequest = {
      items: cartItems.map((item) => ({
        productId: item.productId,
        quantity: item.quantity,
      })),
    };

    checkoutMutation.mutate(buyRequest);
  };
  return (
    <>
      <div className="max-w-5xl mx-auto p-6">
        <h1 className="text-3xl font-bold mb-6">Shopping Cart</h1>

        <div className="space-y-4">
          {cartItems
            .filter((item) => !item.removed)
            .map((item) => (
              <div
                key={item.productId}
                className="flex items-center justify-between bg-base-100 shadow rounded-lg p-4"
              >
                <div>
                  <h2 className="font-semibold">{item.productName}</h2>
                  <p>${item.unitPrice}</p>
                </div>

                <div className="join">
                  <button
                    className="btn join-item"
                    onClick={() =>
                      updateQuantity(item.productId, item.quantity - 1)
                    }
                  >
                    -
                  </button>

                  <input
                    type="number"
                    className="input input-bordered join-item w-20 text-center"
                    value={item.quantity}
                    min="1"
                    onChange={(e) =>
                      updateQuantity(item.productId, Number(e.target.value))
                    }
                  />

                  <button
                    className="btn join-item"
                    onClick={() =>
                      updateQuantity(item.productId, item.quantity + 1)
                    }
                  >
                    +
                  </button>
                </div>

                <div className="font-bold">
                  ${(item.unitPrice * item.quantity).toFixed(2)}
                </div>
                <button
                  className="btn btn-error"
                  onClick={() => removeItem(item.productId)}
                >
                  Remove
                </button>
              </div>
            ))}
        </div>

        <div className="mt-8 text-right">
          <p className="text-2xl font-bold">Total: ${totalPrice.toFixed(2)}</p>
          <button
            className="btn btn-primary mt-4"
            onClick={() => handleRevert()}
          >
            Revert Changes
          </button>
          <button
            className="btn btn-primary mt-4"
            onClick={() => handleUpdateCart(cartItems)}
          >
            Update Cart
          </button>

          <button
            className="btn btn-primary mt-4"
            onClick={() => handleCheckout(cartItems)}
          >
            Checkout
          </button>
        </div>
      </div>
    </>
  );
}
