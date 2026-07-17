// api/items.js

export async function getItems(page, pageSize) {
  const response = await fetch(
    `https://localhost:5001/api/items?page=${page}&pageSize=${pageSize}`,
  );

  if (!response.ok) {
    throw new Error("Failed to fetch items");
  }

  return response.json();
}

export async function createItem(item) {
  const response = await fetch("/api/items", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  });

  if (!response.ok) {
    throw new Error("Failed to create item");
  }

  return response.json();
}
