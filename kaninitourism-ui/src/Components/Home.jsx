import React from 'react';
import himage from '../assets/home.jpg';
import './Landingpage.css';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { faMountainSun } from '@fortawesome/free-solid-svg-icons';

import About from './About';
import Services from './OurService';
import Footer from './Footer';
import Reviews from './reviews';
import Usernav from './Userflow/Usernav';

const Home = () => {
    return (

        <div>
            <Usernav />
            <section className="home">
                <div className="image">
                    <img src={himage} />
                </div>
                <div className="content">
                    <h3>Explore, Dream, Discover: Your Next Journey Awaits</h3>
                    <p>
                        Unveil the world's wonders with us! Explore captivating destinations, immerse in diverse cultures, and create cherished memories that last a lifetime. Let the journey of a lifetime begin!
                    </p>
                    <a className="btn">Travel with us <span className="fas fa-chevron-right"></span></a>
                </div></section>
                <section className="icons-container">
                    <div className="icons">
                        <FontAwesomeIcon icon="fa-solid fa-earth-africa" />
                        <h3>40+</h3>
                        <p>partnered agents</p>
                    </div>
                    <div className="icons">
                        <FontAwesomeIcon icon="fa-solid fa-people-group" />
                        <h3>1040+</h3>
                        <p>satisfied travellers</p>
                    </div>
                    <div className="icons">
                        <FontAwesomeIcon icon="fa-solid fa-utensils" />
                        <h3>500+</h3>
                        <p>restaurent partner</p>
                    </div>
                    <div className="icons">
                        <FontAwesomeIcon icon="fa-solid fa-hotel" />
                        <h3>80+</h3>
                        <p>hotel partners</p>
                    </div>
                </section>
                <Services />
      <About />
      <Reviews />
      <Footer />
                </div>
    )
}

export default Home