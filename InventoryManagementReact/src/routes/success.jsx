import { createFileRoute } from "@tanstack/react-router";
import { GetOrderBySession } from "../api/stripe/checkout";
import { useQuery } from "@tanstack/react-query";

export const Route = createFileRoute(`/success`)({
  component: SuccessPayment,
});

function SuccessPayment() {
  const { session_id } = Route.useSearch();

  const { data, isLoading } = useQuery({
    queryKey: ["order", session_id],
    queryFn: () => GetOrderBySession(session_id),
    enabled: !!session_id,
    refetchInterval: (query) =>
      query.state.data?.status === "pending" ? 2000 : false,
  });
  if (isLoading) {
    return <div>Loading...</div>;
  }
  if (!data) {
    return <div>Order not found.</div>;
  }
  return (
    <>
      {data.status === "paid" ? (
        <>
          <h1>Payment Successful</h1>

          <p>Order #{data.id}</p>

          {data.items.map((item) => (
            <div key={item.product.id}>
              {item.product.name} (${item.unitPrice} × {item.quantity} = $
              {item.unitPrice * item.quantity})
            </div>
          ))}
          <hr></hr>
          <div>
            Total : $
            {data.items.reduce(
              (total, item) => total + item.quantity * item.unitPrice,
              0,
            )}
          </div>
        </>
      ) : (
        <>
          <h1>Payment Processing...</h1>
          <p>Your payment hasn't been confirmed yet.</p>
        </>
      )}
    </>
  );
}
