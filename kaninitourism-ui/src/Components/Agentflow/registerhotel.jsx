import React, { useState } from 'react';
import { TextField, Button, Stack } from '@mui/material';
import Viewhotel from './viewhotel';


const Hotel = () => {
    const [hotelName, setHotelName] = useState('');
    const [location, setLocation] = useState('');
    const [hotelImage, setImage] = useState(null);

    function handleImageChange(event) {
        setImage(event.target.files[0]);
    }

    function handleSubmit(event) {
        event.preventDefault();
        console.log(hotelName, location, hotelImage);

        const formData = new FormData();
        formData.append('hotelName', hotelName);
        formData.append('location', location);
        formData.append('imageFile', hotelImage);

        fetch('https://localhost:7046/api/Hotel', {
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
            <h2>Hotel</h2>
            <form onSubmit={handleSubmit}>
               
                    <TextField
                        type="text"
                        variant='outlined'
                        color='secondary'
                        label="HotelName"
                        onChange={e => setHotelName(e.target.value)}
                        value={hotelName}
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
                 <div style={{ display: 'flex', justifyContent: 'center' }}>
                    <Button variant="outlined" color="secondary" type="submit">Post Hotel</Button>
                </div>
            </form>

        </React.Fragment>
         <br></br>
         <Viewhotel/>
         </div>
    )
}

export default Hotel;