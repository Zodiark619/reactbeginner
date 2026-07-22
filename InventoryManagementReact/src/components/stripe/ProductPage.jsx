import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { AddToCart } from "../../api/stripe/cart";
import { GetProducts } from "../../api/stripe/product";

function ProductPage() {
  const queryClient = useQueryClient();

  const { data, isLoading } = useQuery({
    queryKey: ["products"],
    queryFn: () => GetProducts(),
  });
  const addToCartMutation = useMutation({
    mutationFn: AddToCart,

    onSuccess: () => {
      queryClient.invalidateQueries({
        queryKey: ["carts"],
      });
    },
  });
  const handleAddToCart = (product) => {
    addToCartMutation.mutate({ productId: product.id, quantity: 1 });
  };
  if (isLoading) return <p>Loading...</p>;
  return (
    <>
      {data.map((product) => {
        return (
          <div key={product.id} className="card bg-base-100 w-96 shadow-sm">
            <div className="card-body">
              <h2 className="card-title">{product.name}</h2>
              <p>{product.price}</p>
              <div className="card-actions justify-end">
                <button
                  onClick={() => handleAddToCart(product)}
                  className="btn btn-primary"
                >
                  Add to cart
                </button>
              </div>
            </div>
          </div>
        );
      })}
    </>
  );
}

export default ProductPage;
