import { Link } from "react-router-dom";
function Home() {
  return (
    <div>
      <h2>
        <div>
          <p>
            <Link to="/weatherForecastAPI">Weather Forecast API</Link>
          </p>
          <p>
            <Link to="/financeTrackerAPI">Finance Tracker API</Link>
          </p>
          <p>
            <Link to="/financeTrackerAPI2">Finance Tracker API 2</Link>
          </p>
        </div>
      </h2>
    </div>
  );
}

export default Home;
