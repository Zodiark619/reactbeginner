import { createFileRoute } from "@tanstack/react-router";
import CartPage from "../components/stripe/CartPage";

export const Route = createFileRoute("/cart")({
  component: Cart,
});

function Cart() {
  return <CartPage />;
}
