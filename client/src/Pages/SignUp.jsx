import { useState } from 'react';
import { useForm } from 'react-hook-form';
import './SignUp.css'; // Import your custom CSS file

function SignUp() {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm();
  const [message, setMessage] = useState('');

  const onSubmit = async (data) => {
    try {
      // Send the form data to the backend
      const response = await fetch('http://localhost:5220/api/signup', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });

      if (response.ok) {
        setMessage('Sign-up successful! Please log in.');
      } else {
        const result = await response.json();
        if (result.errors) {
          // If there are validation errors from the server
          const errorMessages = {};
          for (const [key, value] of Object.entries(result.errors)) {
            errorMessages[key] = value.join(' '); // Combine multiple error messages for a field
          }
          setMessage('There are validation errors. Please check your inputs.');
        } else {
          setMessage(result.message || 'An error occurred during sign-up.');
        }
      }
    } catch (error) {
      console.error('Error during sign-up:', error);
      setMessage('An error occurred during sign-up.');
    }
  };

  return (
    <div className="signup-container">
      <h1>Sign Up</h1>
      <form onSubmit={handleSubmit(onSubmit)} className="signup-form">
        {/* First Name */}
        <div className="form-group">
          <label htmlFor="firstName">First Name</label>
          <input
            id="firstName"
            name="firstName"
            placeholder="First Name"
            {...register('firstName', { required: 'First name is required' })}
          />
          {errors.firstName && (
            <span className="error-message">{errors.firstName.message}</span>
          )}
        </div>

        {/* Last Name */}
        <div className="form-group">
          <label htmlFor="lastName">Last Name</label>
          <input
            id="lastName"
            name="lastName"
            placeholder="Last Name"
            {...register('lastName', { required: 'Last name is required' })}
          />
          {errors.lastName && (
            <span className="error-message">{errors.lastName.message}</span>
          )}
        </div>

        {/* Email */}
        <div className="form-group">
          <label htmlFor="email">Email</label>
          <input
            id="email"
            type="email"
            name="email"
            placeholder="Email"
            {...register('email', {
              required: 'Email is required',
              pattern: {
                value: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
                message: 'Invalid email address',
              },
            })}
          />
          {errors.email && (
            <span className="error-message">{errors.email.message}</span>
          )}
        </div>

        {/* Username */}
        <div className="form-group">
          <label htmlFor="username">Username</label>
          <input
            id="username"
            name="username"
            placeholder="Username"
            {...register('username', { required: 'Username is required' })}
          />
          {errors.username && (
            <span className="error-message">{errors.username.message}</span>
          )}
        </div>

        {/* Password */}
        <div className="form-group">
          <label htmlFor="password">Password</label>
          <input
            id="password"
            type="password"
            name="password"
            placeholder="Password"
            {...register('password', { required: 'Password is required' })}
          />
          {errors.password && (
            <span className="error-message">{errors.password.message}</span>
          )}
        </div>

        <button type="submit" className="submit-button">
          Sign Up
        </button>
      </form>

      {message && <div className="message">{message}</div>}
    </div>
  );
}

export default SignUp;
