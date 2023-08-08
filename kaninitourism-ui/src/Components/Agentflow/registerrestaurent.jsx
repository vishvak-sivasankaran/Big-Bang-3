import React, { useState } from 'react';
import { TextField, Button, Stack } from '@mui/material';
import Viewrestaurent from './viewrestaurent';


const Restaurent = () => {
    const [restaurentName, setrestaurentName] = useState('');
    const [location, setLocation] = useState('');
    const [restaurentImage, setImage] = useState(null);

    function handleImageChange(event) {
        setImage(event.target.files[0]);
    }

    function handleSubmit(event) {
        event.preventDefault();
        console.log(restaurentName, location, restaurentImage);

        const formData = new FormData();
        formData.append('restaurentName', restaurentName);
        formData.append('location', location);
        formData.append('imageFile', restaurentImage);

        fetch('https://localhost:7046/api/restaurent', {
            method: 'POST',
            body: formData,
        })
        .then(response => response.json())
        .then(data => {
           
            console.log(data);
        })
        .catch(error => {
            console.error('Error:', error);
        });
    }

    return (
        <div>
        <React.Fragment>
            <h2>Restaurant</h2>
            <form onSubmit={handleSubmit}>
                
                    <TextField
                        type="text"
                        variant='outlined'
                        color='secondary'
                        label="Restaurant Name"
                        onChange={e => setrestaurentName(e.target.value)}
                        value={restaurentName}
                        fullWidth
                        required
                    />
                    <br></br>
                    <br></br>
                    <TextField
                        type="text"
                        variant='outlined'
                        color='secondary'
                        label="Location"
                        onChange={e => setLocation(e.target.value)}
                        value={location}
                        fullWidth
                        required
                    />
                <br></br>
                    <br></br>
            
                <input
                    type="file"
                    onChange={handleImageChange}
                    accept=".jpg,.jpeg,.png"
                    required
                />
                  <br></br>
                    <br></br>
                    <div style={{ display: 'flex', justifyContent: 'center' }}>
                    <Button variant="outlined" color="secondary" type="submit">Post Restaurant</Button>
                </div>
            </form>

        </React.Fragment>
        <br></br>
        <Viewrestaurent/>
        </div>
    )
}

export default Restaurent;