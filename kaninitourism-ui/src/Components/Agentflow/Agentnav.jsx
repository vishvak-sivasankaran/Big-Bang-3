import React from 'react';
import '../../App.css';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { faMountainSun } from '@fortawesome/free-solid-svg-icons';
const Agentnav = () => {
    return (
        <div>
            <nav className="navbar">
            <Link to="/homepage" className="logo">
          <FontAwesomeIcon icon={faMountainSun} /> Make Your Trip
          </Link>
                <Link to="/homepage">Home</Link>
                <Link to="/ourservices">Services</Link>
                <Link to="/viewpackage">packages</Link>
                <Link to="/about">About</Link>
                <Link to="/reviews">Reviews</Link>
                <Link to="/index">Logout</Link>
            </nav>
        </div>
    )
}

export default Agentnav