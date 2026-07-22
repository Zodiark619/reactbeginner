export async function GetUserCart() {
  // const token = localStorage.getItem("token");

  // const response = await fetch("https://localhost:5001/api/cart/my-cart", {
  //   headers: {
  //     Authorization: `Bearer ${token}`,
  //   },
  // });
  const response = await fetch(`https://localhost:5001/api/cart/my-cart`);

  if (!response.ok) {
    throw new Error("Failed to fetch items");
  }

  return response.json();
}

export async function AddToCart(addToCart) {
  const response = await fetch(`https://localhost:5001/api/cart/add`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      // Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(addToCart),
  });

  if (!response.ok) {
    throw new Error("Failed to fetch items");
  }

  return response.json();
}
export async function UpdateCart(UpdateCartResponseDTO) {
  const response = await fetch(`https://localhost:5001/api/cart/updateCart`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      // Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(UpdateCartResponseDTO),
  });

  if (!response.ok) {
    throw new Error("Failed to update cart");
  }
  //return response.json();
}
