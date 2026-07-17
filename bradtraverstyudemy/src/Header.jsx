import { useState } from "react";
import { useCart } from "./CartContext";

const Header = () => {
  const { cart, removeCart, clearCart } = useCart();
  const [showDropdown, setShowDropdown] = useState(false);
  const itemCount = cart.reduce((acc, item) => acc + item.qty, 0);
  const total = cart
    .reduce((acc, item) => acc + item.price * item.qty, 0)
    .toFixed(2);
  return (
    <header className="flex">
      <h1>Bobo</h1>
      <div className="relative">
        <button onClick={() => setShowDropdown(!showDropdown)}>
          {itemCount > 0 && <span>{itemCount}</span>}
        </button>
        {showDropdown && (
          <div className="absolute">
            <div>
              <h2>Cart</h2>
              {cart.length === 0 ? (
                <p>no items</p>
              ) : (
                <>
                  <ul>
                    {cart.map((item) => (
                      <>
                        <li>
                          {item.name} - {item.qty}*${item.price}
                        </li>
                        <button onClick={() => removeCart(item.id)}>
                          rmove
                        </button>
                      </>
                    ))}
                  </ul>
                  <p>{total}</p>
                  <button onClick={clearCart}>clear cart</button>
                </>
              )}
            </div>
          </div>
        )}
      </div>
    </header>
  );
};

export default Header;
