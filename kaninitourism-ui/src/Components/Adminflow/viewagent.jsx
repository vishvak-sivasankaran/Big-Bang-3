import React,{useState,useEffect} from "react";
import axios from 'axios';
import { Button, Card, CardContent, CardMedia, Typography } from '@mui/material';


 
const Viewagent=()=>{
   
const [uploadedFileData, setUploadedFileData] = useState([]);


    const getFileData = async () => {
      try {
        const res = await axios.get("https://localhost:7046/api/User/getAgentByRole", {
          responseType: "json",
        });
        if (Array.isArray(res.data)) {
          setUploadedFileData(res.data);
        } else {
          console.log("Invalid data format received:", res.data);
        }
      } catch (ex) {
        console.log("Error fetching data:", ex);
      }
    };
 
    useEffect(()=>{
        getFileData();
    },[]);
    
return(
<div>



{uploadedFileData.length > 0 && (
        <div>
          <br></br>
          <h3>Agent Details</h3>
          <div style={{ display: "flex", flexWrap: "wrap", justifyContent: "center" }}>
            {uploadedFileData.map((item, index) => (
              <Card key={index} style={{ margin: '10px', maxWidth: '300px' }}>
                <CardContent>
                  <Typography variant="h5" component="h2">
                    {item.userName}
                  </Typography>
                  <Typography color="textSecondary">
                    {item.userEmail}
                  </Typography>
                  <Typography color="textSecondary">
                    {item.phone_Number}
                  </Typography>
                  <Typography color="textSecondary">
                    {item.agency_Name}
                  </Typography>
                </CardContent>
                
              </Card>
            ))}
          </div>
        </div>
      )}
</div>
);
};

export default Viewagent;