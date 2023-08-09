import React from 'react';
import '../App.css';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faStar, faStarHalfAlt } from '@fortawesome/free-solid-svg-icons';
import rimage from '../../src/assets/review.jpg';

const Reviews = () => {
  return (
    <div>
      <section className="review" id="review">
        <h1 className="heading">
          <span>client'S </span>review
        </h1>

        <div className="box-container">
          <div className="box">
            <img src={rimage} alt="Client" />
            <h3>Dhee</h3>
            <div className="stars">
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStarHalfAlt} />
            </div>
            <p className="text">
            Unveil the world's wonders with us! Explore captivating destinations, immerse in diverse cultures, and create cherished memories that last a lifetime. Let the journey of a lifetime begin!
                    
            </p>
          </div>
          <div className="box">
            <img src={rimage} alt="Client" />
            <h3>Adhi</h3>
            <div className="stars">
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStarHalfAlt} />
            </div>
            <p className="text">
            Unveil the world's wonders with us! Explore captivating destinations, immerse in diverse cultures, and create cherished memories that last a lifetime. Let the journey of a lifetime begin!
                    
            </p>
          </div>
          <div className="box">
            <img src={rimage} alt="Client" />
            <h3>Hari</h3>
            <div className="stars">
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStar} />
              <FontAwesomeIcon icon={faStarHalfAlt} />
            </div>
            <p className="text">
            Unveil the world's wonders with us! Explore captivating destinations, immerse in diverse cultures, and create cherished memories that last a lifetime. Let the journey of a lifetime begin!
                
            </p>
          </div>
        </div>
      </section>
    </div>
  );
};

export default Reviews;
