import axios from "axios";

export const generateRandom = async (count) => {
  const response = await axios.post(
    `https://localhost:5000/api/Project1FinanceTracker/generateRandom?count=${count}`,
  );

  return response.data;
};
export const getFinances = async (page, pageSize) => {
  const response = await axios.get(
    `https://localhost:5000/api/Project1FinanceTracker?page=${page}&pageSize=${pageSize}`,
  );

  return response.data;
};
export const getFinancesQueryAsync = async (financeQuery) => {
  const response = await axios.get(
    `https://localhost:5000/api/Project1FinanceTracker/financeQuery`,
    { params: financeQuery },
  );

  return response.data;
};
export const deleteFinance = async (id) => {
  const response = await axios.delete(
    `https://localhost:5000/api/Project1FinanceTracker/${id}`,
  );

  return response.data;
};
export const getMonthlyDashboardFinance = async (year, month) => {
  const response = await axios.get(
    `https://localhost:5000/api/Project1FinanceTracker/dashboard/monthly?year=${year}&month=${month}`,
  );

  return response.data;
};
export const getYearlyDashboardFinance = async (year) => {
  const response = await axios.get(
    `https://localhost:5000/api/Project1FinanceTracker/dashboard/yearly?year=${year}`,
  );

  return response.data;
};

export const login = async (loginRequestDto) => {
  const response = await axios.post(
    `https://localhost:5000/api/Auth/login`,
    loginRequestDto,
  );
  return response.data;
};
export const register = async (registerRequestDto) => {
  const response = await axios.post(
    `https://localhost:5000/api/Auth/register`,
    registerRequestDto,
  );
  return response.data;
};
