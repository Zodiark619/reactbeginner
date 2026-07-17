import { useState } from "react";
import "./style.css";

function RatingComponent() {
  const stars = Array.from({ length: 5 }, (_, index) => index + 1);
  const [rating, setRating] = useState(0);
  const [hover, setHover] = useState(0);
  const message = ["terrible", "bad", "okay", "good", "excellent"];
  return (
    <div className="rating-container">
      <h1>Rate your experience</h1>
      <div className="rating">
        {stars.map((star) => (
          <span
            onClick={() => setRating(star)}
            onMouseEnter={() => setHover(star)}
            onMouseLeave={() => setHover(0)}
            key={star}
            className={`star ${star <= (hover || rating) ? "active" : ""}`}
          >
            {"\u2605"}
          </span>
        ))}
      </div>
      {rating > 0 && <p className="feedback">{message[rating - 1]}</p>}
    </div>
  );
}

function BradTraversy() {
  return (
    <div>
      <RatingComponent />
    </div>
  );
}

export default BradTraversy;
