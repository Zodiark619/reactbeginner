import axios from "axios";

export const getWeather = async () => {
  const response = await axios.get("https://localhost:5000/weatherforecast");

  return response.data;
};
