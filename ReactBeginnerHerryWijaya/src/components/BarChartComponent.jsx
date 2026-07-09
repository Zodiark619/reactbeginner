import {
  BarChart,
  Bar,
  XAxis,
  YAxis,
  Tooltip,
  Legend,
  ResponsiveContainer,
} from "recharts";
function BarChartComponent({ yearlyDashboard }) {
  const COLORS = ["#4CAF50", "#F44336"];
  return (
    <>
      <ResponsiveContainer width="100%" height={400}>
        <BarChart data={yearlyDashboard.monthlySummaries}>
          <XAxis dataKey="month" />
          <YAxis />
          <Tooltip />
          <Legend />

          <Bar dataKey="income" fill="#4CAF50" name="Income" />
          <Bar dataKey="expense" fill="#F44336" name="Expense" />
        </BarChart>
      </ResponsiveContainer>
    </>
  );
}

export default BarChartComponent;
