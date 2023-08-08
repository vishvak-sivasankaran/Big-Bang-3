import React, { useState } from 'react';
import { TextField, Button, Stack } from '@mui/material';
import Viewspot from './viewspot';


const Spot = () => {
    const [spotName, setspotName] = useState('');
    const [description, setDescription] = useState('');
    const [location, setLocation] = useState('');
    const [spotImage, setImage] = useState(null);

    function handleImageChange(event) {
        setImage(event.target.files[0]);
    }

    function handleSubmit(event) {
        event.preventDefault();
        console.log(spotName, location, spotImage);

        const formData = new FormData();
        formData.append('spotName', spotName);
        formData.append('description', description);
        formData.append('location', location);
        formData.append('imageFile', spotImage);

        fetch('https://localhost:7046/api/spot', {
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
            <h2>Spot</h2>
            <form onSubmit={handleSubmit}>
               
                    <TextField
                        type="text"
                        variant='outlined'
                        color='secondary'
                        label="Spot"
                        onChange={e => setspotName(e.target.value)}
                        value={spotName}
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
                     <TextField
                        type="text"
                        variant='outlined'
                        color='secondary'
                        label="Description"
                        onChange={e => setDescription(e.target.value)}
                        value={description}
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
                    <Button variant="outlined" color="secondary" type="submit">Post Spot</Button>
                </div>
            </form>

        </React.Fragment>
        <br></br>
        <Viewspot/>
        </div>
    )
}

export default Spot;