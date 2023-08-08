import React from "react";
import '../App.css';
import aimage from '../../src/assets/about.jpg'

const About = () => {
  return (
    <section className="about" id="about">
      <h1 className="heading"><span>about </span>us</h1>
      <div className="row">
        <div className="content">
          <h3>we take care of your travel life</h3>
          <p>[Hospital Name], we offer a wide range of medical specialties and treatments,
              [Hospital Name], we offer a wide range of medical specialties and treatments,</p>
          <p>[Hospital Name], we offer a wide range of medical specialties and treatments,
              [Hospital Name], we offer a wide range of medical specialties and treatments,</p>
          <a href="#about" className="btn">learn more<span className="fas fa-chevron-right"></span></a>
        </div>
        <div className="image">
          <img src={aimage} alt="About Us" />
        </div>
      </div>

    </section>
    
  )
}

export default About