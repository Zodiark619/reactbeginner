import { useContext } from "react";
import ProductCard from "./ProductCard";
import { ProductContext, useProduct } from "../ProductContext";

const ProductList = () => {
  const { products, error, loading } = useProduct();
  return (
    <div>
      <div className="grid">
        {products.map((product) => (
          <ProductCard key={product.id} product={product} />
        ))}
      </div>
    </div>
  );
};

export default ProductList;
