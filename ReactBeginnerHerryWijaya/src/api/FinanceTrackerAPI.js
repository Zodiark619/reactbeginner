import axios from "axios";

export const generateRandom = async (count) => {
  const response = await axios.post(
    `https://localhost:5000/api/Project1FinanceTracker/generateRandom?count=${count}`,
  );

  return response.data;
};
export const getFinances = async () => {
  const response = await axios.get(
    `https://localhost:5000/api/Project1FinanceTracker/`,
  );

  return response.data;
};
export const deleteFinance = async (id) => {
  const response = await axios.delete(
    `https://localhost:5000/api/Project1FinanceTracker/${id}`,
  );

  return response.data;
};
