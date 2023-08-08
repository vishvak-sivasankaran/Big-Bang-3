import React from 'react';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { faMountainSun } from '@fortawesome/free-solid-svg-icons';
import { faMapMarkedAlt } from '@fortawesome/free-solid-svg-icons';
import '../App.css';

const Services = () => {
  return (
    <section className="services" id="services">
      <h1 className="heading">Our <span>services</span></h1>
      <div className="box-container">
        <div className="box">
        <FontAwesomeIcon icon="fa-solid fa-earth-americas" />
          <h3>world trips</h3>
          <p>At [Hospital Name], we offer a wide range of medical specialties and treatments,</p>
          <a href="#" className="btn">learn more <span className="fas fa-chevron-right"></span></a>
        </div>
        <div className="box">
        <FontAwesomeIcon icon="fa-solid fa-headset" />
          <h3>24/7 support</h3>
          <p>At [Hospital Name], we offer a wide range of medical specialties and treatments,</p>
          <a href="#" className="btn">learn more <span className="fas fa-chevron-right"></span></a>
        </div>
        <div className="box">
        <FontAwesomeIcon icon="fa-solid fa-money-check-dollar" />
          <h3>Easy EMI </h3>
          <p>At [Hospital Name], we offer a wide range of medical specialties and treatments,</p>
          <a href="#" className="btn">learn more <span className="fas fa-chevron-right"></span></a>
        </div>
        <div className="box">
        <FontAwesomeIcon icon={faMapMarkedAlt} />
          <h3>expert guide</h3>
          <p>At [Hospital Name], we offer a wide range of medical specialties and treatments,</p>
          <a href="#" className="btn">learn more <span className="fas fa-chevron-right"></span></a>
        </div>
        <div className="box">
        <FontAwesomeIcon icon="fa-solid fa-tent" />
          <h3>customised stay</h3>
          <p>At [Hospital Name], we offer a wide range of medical specialties and treatments,</p>
          <a href="#" className="btn">learn more <span className="fas fa-chevron-right"></span></a>
        </div>
        <div className="box">
        <FontAwesomeIcon icon="fa-solid fa-utensils" />
          <h3>restaurents</h3>
          <p>At [Hospital Name], we offer a wide range of medical specialties and treatments,</p>
          <a href="#" className="btn">learn more <span className="fas fa-chevron-right"></span></a>
        </div>
      </div>
    </section>
  );
};

export default Services;
