import {
  PieChart,
  Pie,
  Tooltip,
  Legend,
  ResponsiveContainer,
  Cell,
} from "recharts";
function PieChartComponent({ data }) {
  const pieData = [
    {
      name: "Income",
      value: data.totalIncome,
    },
    {
      name: "Expense",
      value: data.totalExpense,
    },
  ];
  const COLORS = ["#4CAF50", "#F44336"];
  return (
    <>
      <ResponsiveContainer width="100%" height={300}>
        <PieChart>
          <Pie data={pieData} dataKey="value" nameKey="name" outerRadius={100}>
            {pieData.map((entry, index) => (
              <Cell key={entry.name} fill={COLORS[index]} />
            ))}
          </Pie>
          <Tooltip />
          <Legend />
        </PieChart>
      </ResponsiveContainer>
    </>
  );
}

export default PieChartComponent;
