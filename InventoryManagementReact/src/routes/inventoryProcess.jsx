import { createFileRoute } from "@tanstack/react-router";

export const Route = createFileRoute("/InventoryProcess")({
  component: InventoryProcess,
});

function InventoryProcess() {
  return <div className="p-2">Hello from About!</div>;
}
