import { Route, Routes } from 'react-router-dom';
import './App.css';
import About from './Components/About';
import Admin from './Components/Adminflow/Admin';
import Feedback from './Components/Userflow/Feedback';
import Home from './Components/Home';
import LoginPage from './Components/LoginPage';
import RegisterPage from './Components/RegisterPage';
import ProtectedAdmin from './Components/ProtectedRouting/ProtectedAdminPage';
import LandingPage from './Components/LandingPage';
import SignUp from './Components/RegisterPage';
import Manage from './Components/Adminflow/Admin';
import Footer from './Components/Footer';
import Services from './Components/OurService';
import Package from './Components/Agentflow/Postpackage';
import Viewpackage from './Components/Agentflow/Viewpackage';
import Viewhotel from './Components/Agentflow/viewhotel';
import Hotel from './Components/Agentflow/registerhotel';
import Viewrestaurent from './Components/Agentflow/viewrestaurent';
import Restaurent from './Components/Agentflow/registerrestaurent';
import Spot from './Components/Agentflow/registerspot';
import Viewspot from './Components/Agentflow/viewspot';
import Viewagent from './Components/Adminflow/viewagent';
import ImageGalleryPost from './Components/Adminflow/gallerypost';
import Viewusers from './Components/Adminflow/viewusers';
import Gallery from './Components/Userflow/gallery';
import Booking from './Components/Userflow/booking';
import Searchpage from './Components/Userflow/searchpage';
import Viewpage from './Components/Userflow/viewpage';
import Reviews from './Components/reviews';
import Agentnav from './Components/Agentflow/Agentnav';
import Adminhome from './Components/Adminflow/Adminhome';
import Agenthome from './Components/Agentflow/Agenthome';
import Adminnav from './Components/Adminflow/Adminnav';
import Usernav from './Components/Userflow/Usernav';


function App() {
  var token;
  return (
    <div className="App">
   
      <Routes>
        <Route path="/" element={<LandingPage />} />
        <Route path="/home" element={<Home />} />
        <Route path="/about" element={<About />} />
        <Route path="/feedback" element={<Feedback />} />
        <Route path="/admin" element={<Admin />} />
        <Route path="/login" element={<LoginPage />} />
        <Route path="/register" element={<SignUp />} />
        <Route path="/admin" element={<Manage />} />
        <Route path="/ourservices" element={<Services />} />
        <Route path="/booking" element={<Booking />} />
        <Route path="/gallerypost" element={<ImageGalleryPost />} />
        <Route path="/viewagents" element={<Viewagent />} />
        <Route path="/viewuser" element={<Viewusers />} />
        <Route path="/postpackage" element={<Package />} />
        <Route path="/registerhotel" element={<Hotel />} />
        <Route path="/registerrestaurents" element={<Restaurent/>} />
        <Route path="/registerspot" element={<Spot />}/>
        <Route path="/viewhotel" element={<Viewhotel />} />
        <Route path="/viewpackage" elements={<Viewpackage />}/>
        <Route path="/viewrestaurent" element={<Viewrestaurent />}/>
        <Route path="/viewspot" element={<Viewspot />}/>
        <Route path="/registerspot" element={<Spot />}/>
        <Route path="/footer"element={<Footer />}/>
        <Route path="/usernav" element={<Usernav />}/>
        <Route path="/gallery" element={<Gallery />} />
       <Route path="/searchpage" element={<Searchpage/>}/>
       <Route path="/viewpage" element={<Viewpage/>}/>
       <Route path="/reviews" element={<Reviews />}/>
       <Route path="/agentnav" element={<Agentnav />}/>

       <Route path="/adminhome" element={
       <ProtectedAdmin token={token}>
       <Adminhome />
       </ProtectedAdmin>
       }/>
       <Route path="/agenthome" element={<Agenthome />}/>
       <Route path="/adminnav" element={<Adminnav />}/>
       
      </Routes>
    </div>
  );
}

export default App;
