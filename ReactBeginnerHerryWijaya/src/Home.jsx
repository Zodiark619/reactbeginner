import { Link } from "react-router-dom";
function Home() {
  return (
    <div>
      <h2>
        <Link to="/weatherForecastAPI">Weather Forecast API</Link>
        <Link to="/financeTrackerAPI">Finance Tracker API</Link>
      </h2>
    </div>
  );
}

export default Home;
