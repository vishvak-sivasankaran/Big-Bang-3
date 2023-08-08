import React, { useState, useEffect ,useRef} from "react";
import { useLocation, useNavigate } from "react-router-dom";
import TextField from '@mui/material/TextField';
import axios from 'axios';
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import { toast, ToastContainer } from 'react-toastify';
import Button from '@mui/material/Button';
import MenuItem from '@mui/material/MenuItem';
import Select from '@mui/material/Select';

import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import MenuIcon from '@mui/icons-material/Menu';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import dayjs from 'dayjs';
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
import { DatePicker } from '@mui/x-date-pickers/DatePicker';
import {Avatar, FormControl, InputLabel, ListItemAvatar } from "@mui/material";
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import emailjs from 'emailjs-com';



import jsPDF from 'jspdf';
import 'jspdf-autotable';
import '../../Components/Userflow/booking.css';

const Booking = () => {
    const location = useLocation();
    const navigate = useNavigate();
    const packageDetails = location.state?.packageDetails || null;
    const [hotelData, setHotelData] = useState([]);
    const [restaurantData, setRestaurantData] = useState([]);
    const [spotsData, setSpotsData] = useState([]);
    const [hotelId, setHotelId] = useState("");
    const [restaurentId, setSelectedRestaurentId] = useState("");
    const searchParams = new URLSearchParams(location.search);

    const selectedHotels = searchParams.get('hotels')?.split(',') || [];
    const selectedRestaurants = searchParams.get('restaurants')?.split(',') || [];
    const [selectedHotelId, setSelectedHotelId] = useState("");
    const [selectedRestaurantId, setSelectedRestaurantId] = useState("");

    const [isPopupOpen, setIsPopupOpen] = useState(false);
    const [pdfUrl, setPdfUrl] = useState("");
    const formDataRef = useRef(null);
    const [otpVerified, setOtpVerified] = useState(false);





    const [formData, setFormData] = useState({
        name: '',
        email: '',
        phone_Number: '',
        startDate: '',
        totalPrice: '',
        adult: '',
        child: '',
    });

    const handleNavClick = () => {
        navigate("/sidenav");
    };

    const handleChange = (event) => {
      const { name, value } = event.target;
    
      if (name === 'startDate') {
        const formattedDate = dayjs(value).format('YYYY-MM-DD');
        setFormData((prevData) => ({
          ...prevData,
          startDate: formattedDate,
        }));
      } else if (name === 'hotel') {
        setSelectedHotelId(value);
      } else if (name === 'restaurant') {
        setSelectedRestaurantId(value);
      } else if (name === 'adult' || name === 'child') {
        setFormData((prevData) => ({
          ...prevData,
          [name]: value,
          totalPrice: calculateTotalPrice().toFixed(2), 
        }));
      } else {
        setFormData((prevData) => ({
          ...prevData,
          [name]: value,
        }));
      }
    };
    


    const calculateTotalPrice = () => {
        const adultCount = parseInt(formData.adult) || 0;
        const childCount = parseInt(formData.child) || 0;
        const adultPrice = parseFloat(packageDetails.priceForAdult) || 0;
        const childPrice = parseFloat(packageDetails.priceForChild) || 0;
        return (adultCount * adultPrice) + (childCount * childPrice);
    };

    const findSelectedHotel = (selectedHotelId) => {
        return hotelData.find((hotel) => hotel.hotelId === selectedHotelId);
      };

      const findSelectedRestaurent= (selectedRestaurantId)=>{
        return restaurantData.find((restaurent)=>restaurent.restaurentId === selectedRestaurantId);
      }
     
    const handleSignUp = async (event) => {
        event.preventDefault();
        const { name, value } = event.target;

        console.log(name, value);

        const hotel = hotelData.find((hotel) => hotel.hotelId === selectedHotelId);
        const selectedHotel = hotel ? hotel.hotelName : null;

        const restaurant = restaurantData.find((restaurant) => restaurant.restaurentId === selectedRestaurantId);
        const selectedRestaurant = restaurant ? restaurant.restaurentName : null;

        const formDataCopy = { ...formData, hotelId: selectedHotelId, restaurentId: selectedRestaurantId };
        formDataRef.current = formDataCopy;

        setIsPopupOpen(true);
        try {
            const doc = new jsPDF();
            doc.text('Booking Details', 10, 10);
            // doc.text(`Name: ${formData.name}`, 10, 20);
            // doc.text(`Email: ${formData.email}`, 10, 30);
            // doc.text(`Phone Number: ${formData.phone_Number}`, 10, 40);
            // doc.text(`Adult: ${formData.adult}`, 10, 50);
            // doc.text(`Child: ${formData.child}`, 10, 60);
            // const totalPrice = calculateTotalPrice().toFixed(2);
            // doc.text(`Total Price: ${totalPrice}`, 10, 70);

            const bookingDetailsTable = [
              ['Traveller Details', ''],
              ['Name', formData.name],
              ['Email', formData.email],
              ['Phone Number', formData.phone_Number],
              ['Adult', formData.adult],
              ['Child', formData.child],
              ['Total Price', calculateTotalPrice().toFixed(2)],
            ];
      
            doc.autoTable({
              startY: 30,
              head: [bookingDetailsTable[0]],
              body: bookingDetailsTable.slice(1),
              theme: 'grid', 
            });

            // doc.text(`Package Name: ${packageDetails.packageName}`, 10, 90);
            // doc.text(`Location: ${packageDetails.destination}`, 10, 100);
            // doc.text(`Duration: ${packageDetails.duration}`, 10, 110);
            // doc.text(`Price for Adult: ${packageDetails.priceForAdult}`, 10, 120);
            // doc.text(`Price for Child: ${packageDetails.priceForChild}`, 10, 130);

            // doc.text(`Hotel Name: ${selectedHotel ? selectedHotel : 'N/A'}`, 10, 150);
            // doc.text(`Restaurant Name: ${selectedRestaurant ? selectedRestaurant : 'N/A'}`, 10, 160);

            const tableData = [
              ['Package Name', packageDetails.packageName],
              ['Location', packageDetails.destination],
              ['Duration', packageDetails.duration],
              ['Price for Adult', packageDetails.priceForAdult],
              ['Price for Child', packageDetails.priceForChild],
              ['Hotel Name', selectedHotel ? selectedHotel : 'N/A'],
              ['Restaurant Name', selectedRestaurant ? selectedRestaurant : 'N/A'],
            ];
            doc.autoTable({
              startY: 100,
              head: [['Package Details', '']],
              body: tableData,
            });
            


            //doc.save('booking_details.pdf');
            const pdfBlob = doc.output('blob');

            const pdfUrl = URL.createObjectURL(pdfBlob);
            setPdfUrl(pdfUrl); 

            //window.open(pdfUrl);


            const bookingData = {
                ...formData,
                packageId: packageDetails.packageId,
                hotelId: selectedHotelId,
                restaurentId: selectedRestaurantId,
                user: "2",
                totalPrice: calculateTotalPrice()

            };


            await axios.post('https://localhost:7046/api/Book', bookingData);


            setFormData({
                name: '',
                email: '',
                phone_Number: '',
                startDate: '',
                totalPrice: '',
                adult: '',
                child: '',

            });
            
     

        } catch (error) {
            console.error('Error submitting form data:', error);
            alert('Failed to submit booking. Please try again later.');
        }

    };


    
   

      const [email, setEmail] = useState('');
    
    
   
 
 
    
    
    const handleClosePopup = () => {
      setIsPopupOpen(false);
      // const formData = formDataRef.current;
      // sendEmail(formData);
    };
    
    const handleDownloadPDF = () => {

      const a = document.createElement("a");
      a.href = pdfUrl;
      a.download = "booking_details.pdf";

      a.click();
    };

    useEffect(() => {

        async function fetchHotelData() {
            try {
                const res = await axios.get(`https://localhost:7046/api/Hotel/Location?location=${packageDetails.destination}`, {
                    responseType: "json",
                });
                if (Array.isArray(res.data)) {
                    setHotelData(res.data);
                } else {
                    console.log("Invalid", res.data);
                }
            } catch (ex) {
                console.log("Error:", ex);
            }
        }

        async function fetchRestaurantData() {
            try {
                const res = await axios.get(`https://localhost:7046/api/Restaurent/Location?location=${packageDetails.destination}`, {
                    responseType: "json",
                });
                if (Array.isArray(res.data)) {
                    setRestaurantData(res.data);
                } else {
                    console.log("Invalid", res.data);
                }
            } catch (ex) {
                console.log("Error:", ex);
            }
        }

        const getSpotData = async () => {

            const res = await axios.get(`https://localhost:7046/api/spot/Location?location=${packageDetails.destination}`, {
                responseType: "json",
            });
            if (Array.isArray(res.data)) {
                setSpotsData(res.data.slice(0, packageDetails.duration));
            } else {
                console.log("Invalid data format received:", res.data);
            }

        };

        if (packageDetails) {
            fetchHotelData();
            fetchRestaurantData();
            getSpotData();
        }
    }, [packageDetails]);



    if (!packageDetails) {
        return <p>No package details found.</p>;
    }

    return (
        <div>
            <Box sx={{ flexGrow: 1 }} >
                <AppBar position="static" style={{ backgroundColor: '#e7afb7' }}>
                    <Toolbar>
                        <ArrowBackIcon
                            size="large"
                            edge="start"
                            color="inherit"
                            aria-label="menu"
                            onClick={handleNavClick}
                            sx={{ mr: 2 }}
                        >
                            <MenuIcon />
                        </ArrowBackIcon>
                        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                            Make Your Trip
                        </Typography>
                    </Toolbar>
                </AppBar>
            </Box>
            <br></br>
            <div style={{ display: "flex", justifyContent: "center", alignItems: "center" }}>

                <Card key={packageDetails.userId} style={{ margin: '10px', maxWidth: '850px' }} >
                    <CardContent style={{ display: 'flex', alignItems: 'center' }}>
                        <div style={{ flex: '0 0 30%', marginRight: '20px' }}>
                            {packageDetails.packImage && (
                                <img
                                    src={`data:image/jpeg;base64,${packageDetails.packImage}`}
                                    alt="Image"
                                    style={{ width: '100%', height: '150px', objectFit: 'cover' }}
                                />
                            )}
                        </div>

                        <div style={{ flex: '1' }}>
                            <Typography variant="h5" component="h2">
                                {packageDetails.packageName}
                            </Typography>
                            <Typography color="textSecondary">
                                {packageDetails.destination}
                            </Typography>
                            <Typography color="textSecondary">
                                {packageDetails.duration} days
                            </Typography>
                            <Typography color="textSecondary">
                                {packageDetails.priceForAdult}
                            </Typography>
                            <Typography color="textSecondary">
                                {packageDetails.priceForChild}
                            </Typography>
                        </div>
                    </CardContent>
                </Card>

            </div>
            <br></br>
            <div style={{ display: "flex", justifyContent: "center", alignItems: "center" }}>
                <div style={{ backgroundColor: '#f7e9ea', width: 1000, flexWrap: "wrap", display: "flex", flexDirection: "column", alignItems: "center" }}>
                    <div style={{ backgroundColor: '#e7afb7', width: 1000, height: 50, display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                        <h5>Day Planner</h5>
                    </div>
                    <br></br>
                    {spotsData.length > 0 && (

                        <div style={{ display: "flex", flexWrap: "wrap", justifyContent: "center" }}>

                            {packageDetails.duration > 0 && (
                                <div >
                                    {spotsData.map((spot, index) => {
                                        if (index === 0) {
                                            return (
                                                <div key={index}>
                                                    <h5>Day 1</h5>
                                                    <Card key={index} style={{ margin: '10px', maxWidth: '300px' }} className="spot-card">
                                                        <CardContent>
                                                            <Typography variant="h5" component="h2">
                                                                {spot.spotName}
                                                            </Typography>
                                                            <Typography color="textSecondary" component="h1">
                                                                {spot.location}
                                                            </Typography>
                                                            {spot.spotImage && (
                                                                <img
                                                                    src={`data:image/jpeg;base64,${spot.spotImage}`}
                                                                    alt={`Image ${index + 1}`}
                                                                    style={{ width: '100%', height: '150px', objectFit: 'cover' }}
                                                                />
                                                            )}
                                                        </CardContent>
                                                    </Card>
                                                </div>
                                            );
                                        }
                                        return null;
                                    })}
                                </div>
                            )}

                            {Array.from({ length: packageDetails.duration - 1 }).map((_, dayIndex) => (
                                <div key={dayIndex + 2}>
                                    <h5>Day {dayIndex + 2}</h5>
                                    {spotsData.map((spot, index) => {
                                        if (index === dayIndex + 1) {
                                            return (
                                                <Card key={index} style={{ margin: '10px', maxWidth: '300px' }} className="spot-card">
                                                    <CardContent>
                                                        <Typography variant="h5" component="h2">
                                                            {spot.spotName}
                                                        </Typography>
                                                        <Typography color="textSecondary" component="h1">
                                                            {spot.location}
                                                        </Typography>
                                                        {spot.spotImage && (
                                                            <img
                                                                src={`data:image/jpeg;base64,${spot.spotImage}`}
                                                                alt={`Image ${index + 1}`}
                                                                style={{ width: '100%', height: '150px', objectFit: 'cover' }}
                                                            />
                                                        )}
                                                    </CardContent>
                                                </Card>
                                            );
                                        }
                                        return null;
                                    })}
                                </div>
                            ))}
                        </div>
                    )}
                    <br></br>
                </div>
            </div>
            <br></br><br></br>  <br></br>  <br></br>  <br></br>

            <br></br>  <br></br>  <br></br>  <br></br>  <br></br>  <br></br>  <br></br>  <br></br>  <br></br>  <br></br>
            <div style={{ display: "flex", justifyContent: "center", alignItems: "center", height: "100vh" }}>

                <div className="card" style={{ width: 600, display: "flex", justifyContent: "center" }}>
                    <div className="card-body text-center" >

                        <h3 className="mb-4">Book Your Trip</h3>
                        <div className="input-group mb-3">
                            <TextField
                                label="Name"
                                variant="outlined"
                                name="name"
                                value={formData.name}
                                onChange={handleChange}
                                fullWidth
                                margin="normal"
                            />

                        </div>
                        <div className="input-group mb-3">
                            <TextField
                                label="Email"
                                name="email"
                                variant="outlined"
                                value={formData.email}
                                onChange={handleChange}
                                fullWidth
                                margin="normal"
                            />

                        </div>


                        <div className="input-group mb-3">
                            <TextField
                                label="Total Price"
                                name="totalPrice"
                                variant="outlined"
                                value={calculateTotalPrice().toFixed(2)}
                                disabled
                                fullWidth
                                margin="normal"
                            />
                        </div>
                        <div className="input-group mb-4" style={{ position: "relative" }}>
                            <TextField

                                label="Adult"
                                variant="outlined"

                                name="adult"
                                value={formData.adult}
                                onChange={handleChange}
                                fullWidth
                                margin="normal"
                            />

                        </div>

                        <div className="input-group mb-4" style={{ position: "relative" }}>
                            <TextField
                                label="Child"
                                variant="outlined"
                                name="child"
                                value={formData.child}
                                onChange={handleChange}
                                fullWidth
                                margin="normal"
                            />

                        </div>
                        <div className="input-group mb-4" style={{ position: "relative" }}>
                            <TextField
                                label="Phone Number"
                                name="phone_Number"
                                variant="outlined"
                                value={formData.phone_Number}
                                onChange={handleChange}
                                fullWidth
                                margin="normal"
                            />
                        </div>

                        <div className="input-group mb-4" style={{ position: "relative" }}>
                            <LocalizationProvider dateAdapter={AdapterDayjs}>
                                <DatePicker
                                    fullWidth
                                    label="Start Date"
                                    name="startDate"
                                    value={formData.startDate}
                                    onChange={(date) => handleChange({ target: { name: "startDate", value: date } })}
                                    disablePast
                                    views={['year', 'month', 'day']}
                                   
                                />
                            </LocalizationProvider>

                        </div>

                        <div className="input-group mb-4" style={{ position: "relative" }}>
                        <FormControl fullWidth variant="outlined">
                        <InputLabel htmlFor="restaurant-select">Choose Hotel</InputLabel>
                            <Select
                                name="hotel"
                                value={selectedHotelId}
                                onChange={handleChange}
                                renderValue={()=>{
                                    const selectedHotel = findSelectedHotel(selectedHotelId);
                                    return (
                                      <>
                                        {selectedHotel && selectedHotel.hotelImage && (
                                          <Avatar
                                            alt={selectedHotel.hotelName}
                                            src={`data:image/jpeg;base64,${selectedHotel.hotelImage}`}
                                            style={{ width: 80, height: 80 }}
                                          />
                                        )}
                                        {selectedHotel?.hotelName || "Select Hotel"}
                                      </>
                                    );
                                  }}
                                >
                                {hotelData.map((hotel, index) => (
                                    <MenuItem key={index} value={hotel.hotelId}>
                                        <ListItemAvatar>
                                            <Avatar src={`data:image/jpeg;base64,${hotel.hotelImage}`}
                                            style={{width:300,height:300}}
                                            />
                                        </ListItemAvatar>
                                       <h4> &nbsp;&nbsp;&nbsp;&nbsp;{hotel.hotelName}</h4>
                                    </MenuItem>
                                ))}
                            </Select>
                            </FormControl>
                        </div>

                        <br></br>
                        <div className="input-group mb-4" style={{ position: "relative" }}>
                        <FormControl fullWidth variant="outlined">
                        <InputLabel htmlFor="restaurant-select">Choose Restaurant</InputLabel>
                            
                            <Select
                                name="restaurant"
                                value={selectedRestaurantId}
                                onChange={handleChange}
                                renderValue={()=>{
                                    const selectedRestaurant = findSelectedRestaurent(selectedRestaurantId);
                                    return (
                                      <>
                                        {selectedRestaurant && selectedRestaurant.restaurentImage && (
                                          <Avatar
                                            alt={selectedRestaurant.restaurentName}
                                            src={`data:image/jpeg;base64,${selectedRestaurant.restaurentImage}`}
                                            style={{ width: 80, height: 80 }}
                                          />
                                        )}
                                        {selectedRestaurant?.restaurentName || "Select Restaurent"}
                                      </>
                                    );
                                  }}
                                >
                                {restaurantData.map((restaurant, index) => (
                                    <MenuItem key={index} value={restaurant.restaurentId}>
                                         <ListItemAvatar>
                                            <Avatar src={`data:image/jpeg;base64,${restaurant.restaurentImage}`}
                                            style={{width:300,height:300}}
                                            />
                                        </ListItemAvatar>
                                        <h4> &nbsp;&nbsp;&nbsp;&nbsp; {restaurant.restaurentName}</h4>
                                    </MenuItem>
                                ))}
                            </Select>
                            </FormControl>
                        </div>
                      
                        <Typography variant="h6" component="h2">
                            Total Price: {calculateTotalPrice().toFixed(2)}
                        </Typography>

                        <input type="hidden" name="packageId" value={packageDetails.packageId} />
                        {selectedHotels.map((hotelId) => (
                            <input key={hotelId} type="hidden" name="selectedHotels" value={hotelId} />
                        ))}

                        {selectedRestaurants.map((restaurentId) => (
                            <input key={restaurentId} type="hidden" name="selectedRestaurents" value={restaurentId} />
                        ))}
                        <button className="btn btn-primary shadow-2 mb-4" type="button" onClick={handleSignUp}>Book</button>
                    </div>
                </div>
            </div>
            <div>   
            <Dialog open={isPopupOpen} onClose={handleClosePopup} style={{width:'1500px',height:'850px',marginTop:'-57px'}}>
           

    <DialogTitle>Booking Successful!</DialogTitle>
   
  
        
    <DialogActions style={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
   

      
      <div style={{display:'flex',flexDirection:'row'}}>
        <Button onClick={() => window.open(pdfUrl)}>Open PDF</Button>
        <Button onClick={handleDownloadPDF}>Download PDF</Button>
       
        <Button onClick={handleClosePopup}>Close</Button>
        </div>
    
    
     
    </DialogActions>
  
  </Dialog>
  </div>
        </div>
    );
};

export default Booking;