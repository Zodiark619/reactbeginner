import { Route, Routes } from "react-router-dom";
import Home from "./Home";
import Navbar from "./Navbar";
import WeatherForecastAPI from "./WeatherForecastAPI";
import FinanceTrackerAPI from "./FinanceTrackerAPI";
import FinanceTrackerAPI2 from "./FinanceTrackerAPI2";

function App() {
  return (
    <div>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/weatherForecastAPI" element={<WeatherForecastAPI />} />
        <Route path="/financeTrackerAPI" element={<FinanceTrackerAPI />} />
        <Route path="/financeTrackerAPI2" element={<FinanceTrackerAPI2 />} />
      </Routes>
    </div>
  );
}

export default App;
