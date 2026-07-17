import { Route, Routes } from "react-router-dom";
import Home from "./Home";
import Navbar from "./Navbar";
import WeatherForecastAPI from "./WeatherForecastAPI";
import FinanceTrackerAPI from "./FinanceTrackerAPI";
import FinanceTrackerAPI2 from "./FinanceTrackerAPI2";
import ProtectedRoute from "./ProtectedRoute";
import Login from "./Login";
import Register from "./Register";
import BradTraversy from "./bradtraversy/Rating";

function App() {
  return (
    <div>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/weatherForecastAPI" element={<WeatherForecastAPI />} />
        <Route path="/financeTrackerAPI" element={<FinanceTrackerAPI />} />
        <Route path="/bradtraversy" element={<BradTraversy />} />

        <Route
          path="financeTrackerAPI2"
          element={
            <ProtectedRoute>
              <FinanceTrackerAPI2 />
            </ProtectedRoute>
          }
        />

        <Route path="/login" element={<Login />} />
        <Route path="/register" element={<Register />} />
      </Routes>
    </div>
  );
}

export default App;
