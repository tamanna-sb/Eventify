import axios from 'axios';

export const getAllEvents = async () => {
  try {
    const token = localStorage.getItem('authToken'); // Get stored token
    if (!token) {
      console.error("No token found, user might not be authenticated.");
      return;
    }

    const response = await axios.get('http://localhost:5181/api/events', {
      headers: { Authorization: `Bearer ${token}` }, // Attach token
    });
    return response.data;
  } catch (error) {
    throw new Error('Failed to fetch events');
  }
};

