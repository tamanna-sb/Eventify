import axios from "axios";


const API_URL = "http://localhost:5001/api/auth"; // Ensure this matches backend

export const signup = async (userData: { userName: string; email: string; password: string }) => {
    try {

        console.log("Received userData:", userData);
        return await axios.post(`${API_URL}/signup`, userData);
    } catch (error) {
        console.error("Signup error:", error);
        throw error;
    }
};


export const signin = async (credentials: { userName: string; password: string }) => {
    return await axios.post(`${API_URL}/signin`, credentials);
};
