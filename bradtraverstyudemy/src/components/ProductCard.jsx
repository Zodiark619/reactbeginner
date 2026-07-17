import { useCart } from "../CartContext";

const ProductCard = ({ product }) => {
  const { addToCart } = useCart();
  return (
    <div>
      <div>
        <h3>
          {product.name} - ${product.price.toFixed(2)}
        </h3>
        <img src={product.image}></img>
        <p>{product.description}</p>
        <button onClick={() => addToCart(product)}>Add to cart</button>
      </div>
    </div>
  );
};

export default ProductCard;
