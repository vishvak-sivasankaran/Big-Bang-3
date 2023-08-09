import React, { useState,useEffect } from 'react';
import { NavLink, useNavigate } from 'react-router-dom';
import TextField from '@mui/material/TextField';
import axios from 'axios';
import '../App.css';
import limage from '../../src/assets/login.jpg';
import jwt_decode from 'jwt-decode';
const Login = () => {
  const [userEmail, setUserEmail] = useState("");
  const [password, setPassword] = useState("");
  const [isButtonEnabled, setIsButtonEnabled] = useState(false);
  const [emailError, setEmailError] = useState("");
  const [passwordError, setPasswordError] = useState("");
  const navigate = useNavigate();

  const handleEmailChange = (event) => {
    const emailValue = event.target.value;
    setUserEmail(emailValue);
    updateButtonStatus(emailValue, password);
  };

  const handlePasswordChange = (event) => {
    const passwordValue = event.target.value;
    setPassword(passwordValue);
    updateButtonStatus(userEmail, passwordValue);
  };
  const validateEmail = (email) => {
    if (!email.match(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i)) {
      setEmailError("Invalid email address");
      return false;
    }
    setEmailError("");
    return true;
  };

  const validatePassword = (pass) => {
    if (
      pass.length < 8 ||
      !pass.match(/[A-Z]/) ||
      !pass.match(/[0-9]/) ||
      !pass.match(/[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]+/)
    ) {
      setPasswordError(
        "Password must be at least 8 characters and contain uppercase, lowercase, number, and special character."
      );
      return false;
    }
    setPasswordError("");
    return true;
  };

  useEffect(() => {
    const isEmailValid = validateEmail(userEmail);
    const isPasswordValid = validatePassword(password);

    setIsButtonEnabled(isEmailValid && isPasswordValid);
  }, [userEmail, password]);



  const handleLogin = async (e) => {
    e.preventDefault();
    console.log("Login button clicked!");

    try {
      const response = await axios.post('https://localhost:7046/api/User/login', { userEmail, password });

      if (response.status === 200) {
        const token = response.data;
        localStorage.setItem('token', token);

        const decodedToken = jwt_decode(token);
        console.log('Token:', token);
        console.log('Name:', userEmail);
        console.log('Role:', decodedToken.role);
        console.log('nameid', decodedToken.nameid);
        localStorage.setItem('nameid', decodedToken.nameid);
        if (decodedToken.role === "Admin") {

          navigate("/adminhome");
        }
        else if (decodedToken.role === "Agent") {

          navigate("/agenthome");
        }
        else if (decodedToken.role === "User") {

          navigate("/home");
        }
        else {
          //toast.danger('Invalid Credentials');
        }


      } else {
        console.error('Error during login:', response);

      }
    } catch (error) {
      console.error('Error during login:', error);

    }
  };



  const updateButtonStatus = (email, pass) => {
    const isFormFilled =
      email &&
      pass &&
      email.match(/^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i) &&
      pass.length >= 8 &&
      pass.match(/[A-Z]/) &&
      pass.match(/[0-9]/) &&
      pass.match(/[!@#$%^&*()_+\-=[\]{};':"\\|,.<>/?]+/);

    setIsButtonEnabled(isFormFilled);
  };



  return (

    <div className="auth-wrapper">
      <div className="image">
          <img src={limage} />
        </div>
      <div className="auth-content">
        <div className="auth-bg">
          <span className="r" />
          <span className="r s" />
          <span className="r s" />
          <span className="r" />
        </div>
        <div className="card">
          <div className="card-body text-center">
            <div className="mb-4">
              {/* <img src={Logo} alt="Logo" style={{ width: '100px', height: '100px' }} /> */}
            </div>
            <h3 className="mb-4">Login</h3>
            <div className="input-group mb-3">
              <TextField
                id="outlined-email-input"
                label="Email"
                type="text"
                autoComplete="current-email"
                style={{ width: '100%' }}
                value={userEmail}
                onChange={handleEmailChange}
                error={!!emailError}
                helperText={emailError}
             
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
                error={!!passwordError}
                helperText={passwordError}  
           
              />

            </div>
            <button
              className="btn"
              onClick={handleLogin}
              disabled={!isButtonEnabled}
            >Login</button>
            <p className="mb-0 text-muted">Don’t have an account? <NavLink to="/register">Signup</NavLink></p>
          </div>
        </div>
      </div>

    </div>

  );
};

export default Login;
