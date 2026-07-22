export async function GetProducts() {
  const response = await fetch(`https://localhost:5001/api/products`);

  if (!response.ok) {
    throw new Error("Failed to fetch items");
  }

  return response.json();
}
