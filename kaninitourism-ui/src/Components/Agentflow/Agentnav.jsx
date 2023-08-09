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
            <Link to="/agenthome" className="logo">
          <FontAwesomeIcon icon={faMountainSun} /> Make Your Trip
          </Link>
                <Link to="/agenthome">Home</Link>
                <Link to="/postpackage">Package</Link>
                <Link to="/registerhotel">Hotels</Link>
                <Link to="/registerrestaurents">Restaurents</Link>
                <Link to="/registerspot">Spots</Link>
                <Link to="/">Logout</Link>
            </nav>
        </div>
    )
}

export default Agentnav