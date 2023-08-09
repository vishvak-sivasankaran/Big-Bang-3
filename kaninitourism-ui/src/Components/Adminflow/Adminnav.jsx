import React from 'react';
import '../../App.css';
import { Link } from 'react-router-dom';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { library } from '@fortawesome/fontawesome-svg-core';
import { fas } from '@fortawesome/free-solid-svg-icons';
import { faMountainSun } from '@fortawesome/free-solid-svg-icons';

const Adminnav = () => {
    return (
        <div>
            <nav className="navbar">
            <Link to="/adminhome" className="logo">
          <FontAwesomeIcon icon={faMountainSun} /> Make Your Trip
          </Link>
                <Link to="/adminhome">Home</Link>
                <Link to="/admin">Approval</Link>
                <Link to="/gallerypost">Gallery</Link>
                <Link to="/">Logout</Link>
            </nav>
        </div>
    )
}

export default Adminnav