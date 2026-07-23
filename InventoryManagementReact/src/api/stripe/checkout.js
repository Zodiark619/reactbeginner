export async function Checkout(buyRequest) {
  const response = await fetch(`https://localhost:5001/api/checkout/create`, {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
      // Authorization: `Bearer ${token}`,
    },
    body: JSON.stringify(buyRequest),
  });

  if (!response.ok) {
    throw new Error("Failed to confirm payments");
  }

  return response.json();
}

export async function GetOrderBySession(sessionId) {
  const response = await fetch(
    `https://localhost:5001/api/checkout/by-session/${sessionId}`,
    {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        // Authorization: `Bearer ${token}`,
      },
    },
  );

  if (!response.ok) {
    throw new Error("Failed to load order");
  }

  return response.json();
}
