import React from 'react';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Signin: React.FC = () => {
  const navigate = useNavigate();

  const handleSubmit = async (values: { userName: string; password: string }) => {
    try {
      const response = await axios.post('http://localhost:5001/api/Auth/signin', values);
      if (response.status === 200) {
        // Store token in localStorage (or a global state)
        localStorage.setItem('authToken', response.data.token);
        navigate('/events'); // Redirect to dashboard or home page
      }
    } catch (error) {
      console.error("Error during signin:", error);
      // Optional: Display a user-friendly error message here
      alert('There was an error during signin. Please check your credentials and try again.');
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-header">
        <img src="./assets/logo.webp" alt="Eventify Logo" />
        <h1>Eventify</h1>
        <p>Your personal event management tool. Stay organized, stay ahead.</p>
      </div>

      <h2>Sign In</h2>

      <Formik
        initialValues={{
          userName: '',
          password: '',
        }}
        validationSchema={Yup.object({
          userName: Yup.string().required('Username is required'),
          password: Yup.string().min(6, 'Password must be at least 6 characters').required('Password is required'),
        })}
        onSubmit={handleSubmit}
      >
        <Form>
          <div className="form-group">
            <Field type="text" name="userName" placeholder="Username" />
            <ErrorMessage name="userName" component="div" className="error" />
          </div>

          <div className="form-group">
            <Field type="password" name="password" placeholder="Password" />
            <ErrorMessage name="password" component="div" className="error" />
          </div>

          <button type="submit" className="btn">Sign In</button>
        </Form>
      </Formik>

      <div className="auth-footer">
        <p>Don't have an account? <a href="/signup">Sign Up</a></p>
      </div>
    </div>
  );
};

export default Signin;
