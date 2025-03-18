import React, { useState, useEffect } from 'react';
import EventList from '../components/eventList';
import { getAllEvents } from '../services/eventService';

const EventPage: React.FC = () => {
  const [events, setEvents] = useState<any[]>([]);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchEvents = async () => {
      try {
        const eventsData = await getAllEvents();
        setEvents(eventsData);
      } catch (err) {
        setError('Failed to load events');
      } finally {
        setLoading(false);
      }
    };

    fetchEvents();
  }, []);

  return (
    <div className="event-page" style={{ textAlign: 'center', padding: '20px' }}>
    <div style={{
      display: 'inline-block',
      color: 'Black',
      borderRadius: '25px',
      fontSize: '2em',
      fontWeight: '700',
      letterSpacing: '2px',
      textTransform: 'uppercase',
    }}>
      Eventify</div>
      {loading && <p>Loading events...</p>}
      {error && <p className="error-message">{error}</p>}
      <EventList events={events} />
    </div>
  );
};

export default EventPage;


