import axios from "axios";
import { useEffect, useState } from "react";

function WeatherForecastAPI() {
  const [weather, setWeather] = useState([]);
  const getWeather = async () => {
    try {
      const response = await axios.get(
        "https://localhost:5000/weatherforecast",
      );

      setWeather(response.data);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    getWeather();
  }, []);

  const result = weather.map((item, index) => {
    return (
      <div className="card-group" key={index}>
        <div className="card">
          <div className="card-body">
            <h5 className="card-title">{item.summary}</h5>
            <p className="card-text">
              Celsius: {item.temperatureC}
              <br />
              Fahrenheit: {item.temperatureF}
            </p>

            <p className="card-text">
              <small className="text-body-secondary">{item.date}</small>
            </p>
          </div>
        </div>
      </div>
    );
  });
  return (
    <div>
      <div className=" d-flex flex-wrap justify-content-center gap-3">
        {result}
      </div>
      <div className="d-flex justify-content-center mt-3">
        <button className="btn btn-primary" onClick={getWeather}>
          Refresh Weather
        </button>
      </div>
    </div>
  );
}

export default WeatherForecastAPI;
