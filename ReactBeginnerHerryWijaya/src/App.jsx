import { Route, Routes } from "react-router-dom";
import Home from "./Home";
import Navbar from "./Navbar";
import WeatherForecastAPI from "./WeatherForecastAPI";

function App() {
  return (
    <div>
      <Navbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/weatherForecastAPI" element={<WeatherForecastAPI />} />
      </Routes>
    </div>
  );
}

export default App;
