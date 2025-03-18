import React, { useState, useEffect } from 'react';
import EventCard from './EventCard';
import { useNavigate } from 'react-router-dom';

interface EventListProps {
  events: any[];
}

const EventList: React.FC<EventListProps> = ({ events }) => {
  const [scrollPosition, setScrollPosition] = useState(0);
  const navigate = useNavigate();

  // Handle scroll event for smooth scroll effect
  useEffect(() => {
    const handleScroll = () => {
      setScrollPosition(window.scrollY);
    };

    window.addEventListener('scroll', handleScroll);
    return () => {
      window.removeEventListener('scroll', handleScroll);
    };
  }, []);

  // Logout handler
  const handleLogout = () => {
    // Clear the token from localStorage or global state
    localStorage.removeItem('authToken');
    // Redirect to login page or home page
    navigate('/signin');
  };

  return (
    <div style={{ position: 'relative' }}>
      {/* Logout button at the top-right */}
      <button
        onClick={handleLogout}
        style={{
          position: 'absolute',
          top: '20px',
          right: '20px',
          padding: '10px 20px',
          backgroundColor: '#ff4081',
          color: '#fff',
          border: 'none',
          borderRadius: '5px',
          cursor: 'pointer',
          fontSize: '16px',
          transition: 'background-color 0.3s ease',
          zIndex: 1000, // Ensure it's on top of other elements
        }}
        onMouseEnter={(e) => (e.currentTarget.style.backgroundColor = '#f50057')}
        onMouseLeave={(e) => (e.currentTarget.style.backgroundColor = '#ff4081')}
      >
        Logout
      </button>

      <div
        style={{
          maxHeight: '700px',
          overflow: 'auto',
          transform: `translateY(${scrollPosition * 0.1}px)`,
          transition: 'transform 0.5s ease',
        }}
        className="event-list"
      >
        {events.length === 0 ? (
          <div
            style={{
              display: 'flex',
              justifyContent: 'center',
              alignItems: 'center',
              height: '100%',
              padding: '20px',
              color: '#555',
              fontSize: '1.5em',
              fontWeight: '600',
              letterSpacing: '1px',
              textTransform: 'uppercase',
              background: 'linear-gradient(135deg, #f0f0f0, #e8e8e8)',
              borderRadius: '10px',
              boxShadow: '0 4px 8px rgba(0, 0, 0, 0.1)',
            }}
          >
            Eventify
          </div>
        ) : (
          events.map((event) => <EventCard key={event.id} event={event} />)
        )}
      </div>
    </div>
  );
};

export default EventList;
