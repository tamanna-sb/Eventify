import React, { useState } from 'react';
import { Calendar, MapPin, Info } from 'lucide-react';
import axios from 'axios'; // Add axios import to make HTTP requests

interface EventCardProps {
  event: {
    id: string;
    name: string;
    description: string;
    location: string;
    startDate: string;
    endDate: string;
  };
}

const EventCard: React.FC<EventCardProps> = ({ event }) => {
  const [isRegistered, setIsRegistered] = useState(false); // Track registration status

  // Register user for event
  const handleRegister = async () => {
    try {
      const token = localStorage.getItem('authToken'); // Assuming the token is stored in localStorage
      if (!token) {
        alert('Please sign in to register.');
        return;
      }

      const response = await axios.post(
        `http://localhost:5181/api/events/${event.id}/register`,
        {},
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      
      if (response.status === 200) {
        setIsRegistered(true); // Update the state to reflect that the user is registered
        alert('Successfully registered for the event!');
      } else {
        alert('Registration failed. Try again later.');
      }
    } catch (error) {
      console.error('Error registering for the event:', error);
      alert('An error occurred while registering.');
    }
  };

  // Deregister user from event
  const handleDeregister = async () => {
    try {
      const token = localStorage.getItem('authToken'); // Assuming the token is stored in localStorage
      if (!token) {
        alert('Please sign in to deregister.');
        return;
      }

      const response = await axios.post(
        `http://localhost:5181/api/events/${event.id}/deregister`,
        {},
        {
          headers: {
            Authorization: `Bearer ${token}`,
          },
        }
      );
      
      if (response.status === 200) {
        setIsRegistered(false); // Update the state to reflect that the user is deregistered
        alert('Successfully deregistered from the event.');
      } else {
        alert('Deregistration failed. Try again later.');
      }
    } catch (error) {
      console.error('Error deregistering from the event:', error);
      alert('An error occurred while deregistering.');
    }
  };

  return (
    <div
      className="event-card"
      style={{
        border: '1px solid #ddd',
        borderRadius: '12px',
        padding: '25px',
        margin: '20px',
        boxShadow: '0 8px 16px rgba(0, 0, 0, 0.15)',
        background: 'linear-gradient(135deg, #e0f2fe, #d1c4e9)',
        transition: 'transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out',
        display: 'flex',
        flexDirection: 'column',
        minHeight: '300px', // Ensuring the card has enough height
        position: 'relative',
        overflow: 'hidden',
      }}
      onMouseOver={(e) => {
        e.currentTarget.style.transform = 'scale(1.03)';
        e.currentTarget.style.boxShadow = '0 12px 24px rgba(0, 0, 0, 0.2)';
      }}
      onMouseOut={(e) => {
        e.currentTarget.style.transform = 'scale(1)';
        e.currentTarget.style.boxShadow = '0 8px 16px rgba(0, 0, 0, 0.15)';
      }}
    >
      {/* Gradient Bar on Top */}
      <div
        style={{
          width: '100%',
          height: '8px',
          background: 'linear-gradient(90deg, #64b5f6, #9575cd)',
          borderTopLeftRadius: '12px',
          borderTopRightRadius: '12px',
          marginBottom: '16px', // Added space between gradient bar and content
        }}
      />
      <div
        style={{
          display: 'flex',
          flexDirection: 'column',
          justifyContent: 'space-between',
          height: '100%',
        }}
      >
        {/* Event Title */}
        <h3
          style={{
            color: '#37474f',
            marginBottom: '12px',
            fontWeight: '600',
            letterSpacing: '0.5px',
            display: 'flex',
            alignItems: 'center',
          }}
        >
          <Info style={{ marginRight: '8px', width: '20px', height: '20px' }} />
          {event.name}
        </h3>
        
        {/* Event Description */}
        <p
          style={{
            color: '#546e7a',
            marginBottom: '20px',
            lineHeight: '1.6',
          }}
        >
          {event.description}
        </p>
        
        {/* Event Dates */}
        <p style={{ color: '#455a64', fontWeight: '500', display: 'flex', alignItems: 'center' }}>
          <Calendar style={{ marginRight: '8px', width: '16px', height: '16px' }} />
          <strong>Date:</strong> {new Date(event.startDate).toLocaleDateString()} -{' '}
          {new Date(event.endDate).toLocaleDateString()}
        </p>
        
        {/* Event Location */}
        <p style={{ color: '#455a64', fontWeight: '500', display: 'flex', alignItems: 'center' }}>
          <MapPin style={{ marginRight: '8px', width: '16px', height: '16px' }} />
          <strong>Location:</strong> {event.location}
        </p>

        {/* Join Now Button */}
        <div
          style={{
            marginTop: 'auto', // Push button to the bottom of the card
            display: 'flex',
            justifyContent: 'flex-end',
          }}
        >
          <button
            onClick={isRegistered ? handleDeregister : handleRegister}
            style={{
              padding: '10px 20px',
              backgroundColor: isRegistered ? '#f44336' : '#4caf50',
              color: '#fff',
              border: 'none',
              borderRadius: '8px',
              cursor: 'pointer',
              transition: 'background-color 0.3s ease',
            }}
          >
            {isRegistered ? 'Deregister' : 'Join Now'}
          </button>
        </div>
      </div>
    </div>
  );
};

export default EventCard;
