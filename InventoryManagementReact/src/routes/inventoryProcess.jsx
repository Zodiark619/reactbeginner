import { createFileRoute } from "@tanstack/react-router";
import DummyGenerateReport from "../components/InventoryProcess/DummyGenerateReport";

export const Route = createFileRoute("/inventoryProcess")({
  component: InventoryProcess,
});

function InventoryProcess() {
  return (
    <>
      <DummyGenerateReport />
    </>
  );
}
