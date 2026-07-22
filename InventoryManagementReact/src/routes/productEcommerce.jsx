import { createFileRoute } from "@tanstack/react-router";
import ProductPage from "../components/stripe/ProductPage";

export const Route = createFileRoute("/productEcommerce")({
  component: ProduceEcommerce,
});

function ProduceEcommerce() {
  return (
    <>
      <ProductPage />
    </>
  );
}
