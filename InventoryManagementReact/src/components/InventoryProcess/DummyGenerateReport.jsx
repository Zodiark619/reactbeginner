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
      {showResult && (
        <>
          <select
            value={selectedItemId}
            onChange={(e) => setSelectedItemId(Number(e.target.value))}
          >
            <option value={0}>Select an item</option>

            {data?.map((item) => (
              <option key={item.id} value={item.id}>
                {item.name}
              </option>
            ))}
          </select>
          <button onClick={handleGenerate} disabled={selectedItemId === 0}>
            Generate Dummy
          </button>
          <h1>{data.inventoryProcess.name}</h1>

          <p>
            Created: {new Date(data.inventoryProcess.created).toLocaleString()}
          </p>

          <p>Total: ${data.inventoryProcess.totalProcessedPrice}</p>

          <ul>
            {data.inventoryProcessDetails.map((detail) => (
              <li key={detail.id}>
                {detail.name} - Qty: {detail.processedQuantity} - Total: $
                {detail.totalPrice}
              </li>
            ))}
          </ul>
        </>
      )}
    </>
  );
}

export default DummyGenerateReport;
