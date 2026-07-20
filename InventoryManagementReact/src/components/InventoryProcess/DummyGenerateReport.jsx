import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import { generateDummyAPI } from "../../api/inventoryProcess";
import { useState } from "react";
import { getItemsWithoutPaginationAPI } from "../../api/items";

function DummyGenerateReport() {
  const [selectedItemId, setSelectedItemId] = useState(0);
  const [report, setReport] = useState(null);
  const queryClient = useQueryClient();
  const { data: items, isLoading } = useQuery({
    queryKey: ["items"],
    queryFn: () => getItemsWithoutPaginationAPI(),
  });
  const generateDummyMutation = useMutation({
    mutationFn: generateDummyAPI,

    onSuccess: (data) => {
      setReport(data);
    },
  });
  const handleGenerate = () => {
    if (selectedItemId === 0) {
      alert("Please select an item.");
      return;
    }

    generateDummyMutation.mutate(selectedItemId);
  };
  return (
    <>
      <div>
        <select
          value={selectedItemId}
          onChange={(e) => setSelectedItemId(Number(e.target.value))}
        >
          <option value={0}>Select an item</option>

          {items?.map((item) => (
            <option key={item.id} value={item.id}>
              {item.name}
            </option>
          ))}
        </select>
        <br></br>
        <button
          onClick={handleGenerate}
          disabled={selectedItemId === 0}
          className="mt-2 mb-2 px-4 py-2 bg-blue-500 text-white rounded hover:bg-blue-600"
        >
          Generate Dummy
        </button>
      </div>
      {report && (
        <>
          <h1>{report.inventoryProcess.name} </h1>

          <p>
            Created:{" "}
            {new Date(report.inventoryProcess.created).toLocaleString()}
          </p>

          <p>Total Stock In: ${report.inventoryProcess.totalStockInPrice}</p>
          <p>Total Stock Out: ${report.inventoryProcess.totalStockOutPrice}</p>
          <p>Final Quantity: {report.inventoryProcess.finalQuantity}</p>
          <hr></hr>
          <ul>
            {report.inventoryProcess.inventoryProcessDetails.map((detail) => (
              <li key={detail.id}>
                {detail.name} ({detail.itemName} {detail.processType}) - Qty:{" "}
                {detail.processedQuantity} Price: ${detail.itemPrice} (Total: $
                {detail.totalPrice})
              </li>
            ))}
          </ul>
        </>
      )}
    </>
  );
}

export default DummyGenerateReport;
