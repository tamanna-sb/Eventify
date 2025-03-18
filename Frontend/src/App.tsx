import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import Signup from "./pages/SignUp";
import Signin from "./pages/SignIn";
import EventPage from './pages/EventPage';
    

import './styles/auth.css';

function App() {
    return (
        <Router>
            <Routes>
                <Route path="/signup" element={<Signup />} />
                <Route path="/signin" element={<Signin />} />
                <Route path="/events" element={<EventPage />} />
            </Routes>
        </Router>
    );
}

export default App;


