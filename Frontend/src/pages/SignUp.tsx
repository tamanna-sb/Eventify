import React from 'react';
import { Formik, Field, Form, ErrorMessage } from 'formik';
import * as Yup from 'yup';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const Signup: React.FC = () => {
  const navigate = useNavigate();

  const handleSubmit = async (values: { userName: string; email: string; password: string }) => {
    try {
      const response = await axios.post('http://localhost:5001/api/Auth/signup', values);
      console.log(response);
       if (/^[2]\d{2}$/.test(response.status.toString()))  {
        navigate('/signin'); // Redirect to sign-in after successful signup
      }
    } catch (error) {
      console.error("Error during signup:", error);
      // Optional: Display a user-friendly error message here
      alert('There was an error during signup. Please try again later.');
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-header">
        <img src="./assets/logo.webp" alt="Eventify Logo" />
        <h1>Eventify</h1>
        <p>Your personal event management tool. Stay organized, stay ahead.</p>
      </div>
      
      <h2>Create Account</h2>
      
      <Formik
        initialValues={{
          userName: '',
          email: '',
          password: '',
        }}
        validationSchema={Yup.object({
          userName: Yup.string().required('Username is required'),
          email: Yup.string().email('Invalid email format').required('Email is required'),
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
            <Field type="email" name="email" placeholder="Email" />
            <ErrorMessage name="email" component="div" className="error" />
          </div>

          <div className="form-group">
            <Field type="password" name="password" placeholder="Password" />
            <ErrorMessage name="password" component="div" className="error" />
          </div>

          <button type="submit" className="btn">Sign Up</button>
        </Form>
      </Formik>

      <div className="auth-footer">
        <p>Already have an account? <a href="/signin">Sign In</a></p>
      </div>
    </div>
  );
};

export default Signup;
