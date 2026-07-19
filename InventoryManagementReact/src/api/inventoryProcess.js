export async function generateDummyAPI(itemId) {
  const response = await fetch(
    `https://localhost:5001/api/InventoryProcess/generate-dummy/${itemId}`,
    {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
    },
  );

  if (!response.ok) {
    throw new Error("Failed to generate inventory processes");
  }

  return response.json();
}
