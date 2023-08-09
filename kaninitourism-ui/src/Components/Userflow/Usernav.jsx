import React from 'react'
import '../Landingpage.css';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { faMountainSun } from '@fortawesome/free-solid-svg-icons';


const Usernav = () => {
  return (
    <div>
        <div>
            <nav className="navbar">
            <Link to="/home" className="logo">
          <FontAwesomeIcon icon={faMountainSun} /> Make Your Trip
          </Link>
                <Link to="/home">Home</Link>
                <Link to="/searchpage">Book</Link>
                <Link to="/gallery">Gallery</Link>
                <Link to="/">Logout</Link>
            </nav>
        </div>
    </div>
  )
}

export default Usernav