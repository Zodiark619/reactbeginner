// api/items.js

export async function getItemsAPI(page, pageSize) {
  const response = await fetch(
    `https://localhost:5001/api/items?page=${page}&pageSize=${pageSize}`,
  );

  if (!response.ok) {
    throw new Error("Failed to fetch items");
  }

  return response.json();
}

export async function createItemAPI(item) {
  const response = await fetch(`https://localhost:5001/api/items`, {
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
export async function deleteItemAPI(id) {
  const response = await fetch(`https://localhost:5001/api/items/${id}`, {
    method: "DELETE",
    // headers: {
    //   "Content-Type": "application/json",
    // },
  });

  if (!response.ok) {
    throw new Error("Failed to delete item");
  }

  return;
}
export async function updateItemAPI(item) {
  const response = await fetch(`https://localhost:5001/api/items/${item.id}`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(item),
  });

  if (!response.ok) {
    throw new Error("Failed to update item");
  }

  return;
}
