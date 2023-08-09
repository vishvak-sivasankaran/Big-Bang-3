import React, { useState } from "react";
import { useLocation, useNavigate } from "react-router-dom";
import Card from '@mui/material/Card';
import CardContent from '@mui/material/CardContent';
import Typography from '@mui/material/Typography';
import AppBar from '@mui/material/AppBar';
import Box from '@mui/material/Box';
import Toolbar from '@mui/material/Toolbar';
import MenuIcon from '@mui/icons-material/Menu';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';
import Button from '@mui/material/Button';
import bgImage from "../../assets/home.jpg";
import Checkbox from '@mui/material/Checkbox';
import Usernav from "./Usernav";

const label = { inputProps: { 'aria-label': 'Checkbox demo' } };


const Viewpage = () => {

  const location = useLocation();
  // const uploadedFileData = location.state?.uploadedFileData || []; 
  const selectedOption = location.state?.selectedOption || "";
  const [sortingOrder, setSortingOrder] = useState("ascending");
  const [uploadedFileData, setUploadedFileData] = useState(location.state?.uploadedFileData || []);
  const [selectedDurations, setSelectedDurations] = useState([]);





  const navigate = useNavigate();

  const handleApproveClick = (item) => {
    navigate("/booking", { state: { packageDetails: item } });
  };
  const handleSideNavClick = () => {
    navigate("/sidenav");
  };



  if (!uploadedFileData || uploadedFileData.length === 0) {
    return <p>No data </p>;
  }


  return (
    <div>
     <Usernav />
      <Box position="relative" textAlign="center" style={{ width: "100%", height: 200, paddingTop: "1%" }}>
        <Box position="absolute" top="30%" left="30%" transform="translate(-50%, -50%)" color="white" fontSize={{ xs: 20, sm: 50 }} fontWeight={600}>
          <p className='Head'> {selectedOption}</p>
        </Box>

      </Box>
      <table >

        <td style={{ width: 400 , }}>


          <div >

            {uploadedFileData.map((item) => (
              <Card key={item.userId} style={{ margin: '10px', maxWidth: '600px' }} onClick={() => handleApproveClick(item)}>
                <CardContent style={{ display: 'flex', alignItems: 'center' }}>
                  <div style={{ flex: '0 0 30%', marginRight: '20px' }}>
                    {item.packImage && (
                      <img
                        src={`data:image/jpeg;base64,${item.packImage}`}
                        alt="Image"
                        style={{ width: '100%', height: '150px', objectFit: 'cover' }}
                      />
                    )}
                  </div>

                  <div style={{ flex: '1' }}>
                    <Typography variant="h5" component="h2">
                      {item.packageName}
                    </Typography>
                    <Typography color="textSecondary">
                      {item.destination}
                    </Typography>
                    <Typography color="textSecondary">
                      {item.duration} days
                    </Typography>
                    <Typography color="textSecondary">
                      {item.priceForAdult}
                    </Typography>
                    <Typography color="textSecondary">
                      {item.priceForChild}
                    </Typography>
                  </div>
                </CardContent>
              </Card>
            ))}
          </div>



        </td>
      </table>


    </div>
  );
};

export default Viewpage;
