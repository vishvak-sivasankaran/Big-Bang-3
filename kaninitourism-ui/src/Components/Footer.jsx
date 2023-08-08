import React from 'react';
import '../App.css';

const Footer = () => {
  return (
    <section className="footer">
      <div className="box-container">
        <div className="box">
          <h3>quick link</h3>
          <a href="/homepage"><i className="fas fa-chevron-right"></i> home</a>
          <a href="/ourservices"><i className="fas fa-chevron-right"></i> service</a>
          <a href="/about"><i className="fas fa-chevron-right"></i> about</a>
          <a href="/doctors"><i className="fas fa-chevron-right"></i> package</a>
          <a href="/reviews"><i className="fas fa-chevron-right"></i> reviews</a>
        </div>

        <div className="box">
          <h3>our services</h3>
          <a href="/ourservices"><i className="fas fa-chevron-right"></i> wolrd tour</a>
          <a href="/ourservices"><i className="fas fa-chevron-right"></i> agents</a>
          <a href="/ourservices"><i className="fas fa-chevron-right"></i> spots</a>
          <a href="/ourservices"><i className="fas fa-chevron-right"></i> hotels</a>
          <a href="/ourservices"><i className="fas fa-chevron-right"></i> restaurents</a>
        </div>

        <div className="box">
          <h3>contact info</h3>
          <a href="#"><i className="fas fa-phone"></i> +123-456-7890</a>
          <a href="#"><i className="fas fa-phone"></i> +098-765-4321</a>
          <a href="#"><i className="fas fa-envelope"></i> makeurtrip@gmail.com</a>
          <a href="#"><i className="fas fa-map-marker-alt"></i> chennai, in - 6000097</a>
        </div>

        <div className="box">
          <h3>follow us</h3>
          <a href="https://www.facebook.com/"><i className="fab fa-facebook"></i> facebook</a>
          <a href="https://www.twitter.com/"><i className="fab fa-twitter"></i> twitter</a>
          <a href="https://www.instagram.com/"><i className="fab fa-instagram"></i> instagram</a>
          <a href="https://www.linkedin.com/"><i className="fab fa-linkedin-in"></i> linkedin</a>
          <a href="https://www.pinterest.com/"><i className="fab fa-pinterest"></i> pinterest</a>
        </div>
      </div>
      <div className="credit">created by<span> MakeYourTrip tech team</span> | all rights reserved</div>
    </section>
  )
}

export default Footer