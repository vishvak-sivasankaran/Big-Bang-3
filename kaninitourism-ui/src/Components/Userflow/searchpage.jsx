import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import InputLabel from '@mui/material/InputLabel';
import MenuItem from '@mui/material/MenuItem';
import ListSubheader from '@mui/material/ListSubheader';
import FormControl from '@mui/material/FormControl';
import Select from '@mui/material/Select';
import Button from '@mui/material/Button';
import '../Userflow/search.css';
import axios from "axios";
import NearMeIcon from '@mui/icons-material/NearMe';
import Usernav from "./Usernav";
import Footer from "../Footer";



const Searchpage =()=>{

    const [selectedOption, setSelectedOption] = useState("");
    const [uploadedFileData, setUploadedFileData] = useState([]);
    const navigate= useNavigate();

    const getFileData = async () => {
        try {
            const res = await axios.get(`https://localhost:7046/api/Tour/Location?destination=${selectedOption}`, {
                responseType: "json",
            });
            if (Array.isArray(res.data)) {
                setUploadedFileData(res.data);
                navigate("/viewpage", { state: { uploadedFileData: res.data, selectedOption } }); 
     
            } else {
                console.log("Invalid", res.data);
            }
        } catch (ex) {
            console.log("Error:", ex); 
        }
    };

    const handleSelectChange = (event) => {
        setSelectedOption(event.target.value);
    };

    return (
        <div>
         
        <div style={{textAlign:"center"}}>
            <Usernav />
            <br></br><br></br><br></br>
            <FormControl sx={{ m: 1, minWidth: 700}}>
                <InputLabel htmlFor="grouped-select"> search here</InputLabel>
                <Select value={selectedOption} onChange={handleSelectChange} defaultValue="" id="grouped-select" label="Grouping">
                    <ListSubheader>Kerala</ListSubheader>
                    <MenuItem value="ooty">Ooty</MenuItem>
                    <MenuItem value="Munnar">Munnar</MenuItem>
                    <ListSubheader>Delhi</ListSubheader>
                    <MenuItem value="Dubai">Dubai</MenuItem>
                    <MenuItem value="Taj Mahal">Taj Mahal</MenuItem>
                </Select>
            </FormControl>

           
            <Button variant="contained"style={{backgroundColor:"#16a085",width:100,height:50,top:10}} onClick={getFileData}>Search</Button>
        </div>
        <br></br>
        <br></br>
        <Footer />
        </div>
    );
};

export default Searchpage;
