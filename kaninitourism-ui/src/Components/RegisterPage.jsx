import React, { useState, useEffect } from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import TextField from '@mui/material/TextField';
import Box from '@mui/material/Box';
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import axios from 'axios';
import rimage from '../../src/assets/register.jpg';

const SignUp = () => {
  const [userName, setUserName] = useState('');
  const [userEmail, setUserEmail] = useState('');
  const [password, setPassword] = useState('');
  const [role, setRole] = useState('');
  const [phone_Number, setPhoneNumber] = useState('');
  const [isButtonEnabled, setIsButtonEnabled] = useState(false);
  const navigate = useNavigate();
  const [isSignupSuccess, setIsSignupSuccess] = useState(false);

  const handleUsernameChange = (event) => {
    setUserName(event.target.value);
  };

  const handleEmailChange = (event) => {
    setUserEmail(event.target.value);
  };

  const handleContactChange = (event) => {
    setPhoneNumber(event.target.value);
  };

  const handlePasswordChange = (event) => {
    setPassword(event.target.value);
  };

  const handleRoleChange = (event) => {
    setRole(event.target.value);
  };

  const handleSignUp = async (event) => {
    event.preventDefault();
    console.log("Signup button clicked!"); // Add this console log

    const userData = { userName, userEmail, password, role, phone_Number };

    try {
      console.log('Sending signup request with data:', userData); // Add this console log

      let response;
      if (role === 'Agent') {
        response = await axios.post('https://localhost:7046/api/Dummy', userData);
      } else {
        response = await axios.post('https://localhost:7046/api/User/register', userData);
      }

      console.log(response.data);
      setIsSignupSuccess(true);
    } catch (error) {
      console.error(error);
    }
  };

  useEffect(() => {
    const isFormFilled =
      userName &&
      userEmail &&
      password &&
      role &&
      phone_Number &&
      userName.length >= 3 &&
      userEmail.match(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i) &&
      phone_Number.length === 10 &&
      phone_Number.match(/[0-9]/) &&
      password.length >= 8 &&
      password.match(/[A-Z]/) &&
      password.match(/[0-9]/) &&
      password.match(/[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]+/);

    setIsButtonEnabled(isFormFilled);
  }, [userName, userEmail, password, role, phone_Number]);

  useEffect(() => {
    if (isSignupSuccess) {
      const timeoutId = setTimeout(() => {
        navigate('/login');
      }, 2000);

      return () => clearTimeout(timeoutId);
    }
  }, [isSignupSuccess, navigate]);

  return (
    <div className="auth-wrapper">
      <div className="image">
        <img src={rimage} />
      </div>
      <div className="auth-content">
        <div className="card">
          <div className="card-body text-center">
            <h3 className="mb-4">Sign up</h3>
            <div className="input-group mb-3">
              <TextField
                id="outlined-username-input"
                label="Username"
                type="text"
                autoComplete="current-username"
                style={{ width: '100%' }}
                value={userName}
                onChange={handleUsernameChange}
              />
            </div>
            <div className="input-group mb-3">
              <TextField
                id="outlined-email-input"
                label="Email"
                type="text"
                autoComplete="current-email"
                style={{ width: '100%' }}
                value={userEmail}
                onChange={handleEmailChange}
              />
            </div>
            <div className="input-group mb-3">
              <Box sx={{ minWidth: 310 }}>
                <FormControl fullWidth>
                  <InputLabel id="demo-simple-select-label">Role</InputLabel>
                  <Select
                    labelId="demo-simple-select-label"
                    id="demo-simple-select"
                    value={role}
                    label="Role"
                    onChange={handleRoleChange}
                  >
                    <MenuItem value="Agent">Agent</MenuItem>
                    <MenuItem value="User">User</MenuItem>
                  </Select>
                </FormControl>
              </Box>
            </div>
            <div className="input-group mb-3">
              <TextField
                id="outlined-phone-input"
                label="Phone Number"
                type="number"
                autoComplete="current-phone"
                style={{ width: '100%' }}
                value={phone_Number}
                onChange={handleContactChange}
              />
            </div>
            <div className="input-group mb-4" style={{ position: "relative" }}>
              <TextField
                id="outlined-password-input"
                label="Password"
                type="password"
                autoComplete="current-password"
                style={{ width: '100%' }}
                value={password}
                onChange={handlePasswordChange}
              />
            </div>
            <button
              className="btn btn-primary shadow-2 mb-4"
              type="button"
              onClick={handleSignUp}
              
            >
              Sign up
            </button>
            <p className="mb-0 text-muted">
              Already have an account? <NavLink to="/login">Login</NavLink>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
};

export default SignUp;
