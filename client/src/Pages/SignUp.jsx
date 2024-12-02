import { useState } from 'react';
import { useForm } from 'react-hook-form';
import './SignUp.css'; // Import your custom CSS file

function SignUp() {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setError,
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
          Object.keys(result.errors).forEach((field) => {
            // Assuming the error message is in an array
            setError(field, {
              type: 'manual',
              message: result.errors[field][0], // Use the first error message
            });
          });
          console.log(result.errors);
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
          <label htmlFor="FirstName">First Name</label>
          <input
            id="FirstName"
            name="FirstName"
            placeholder="First Name"
            {...register('FirstName', { required: 'First name is required' })}
          />
          {errors.FirstName && (
            <span className="error-message">{errors.FirstName.message}</span>
          )}
        </div>

        {/* Last Name */}
        <div className="form-group">
          <label htmlFor="LastName">Last Name</label>
          <input
            id="LastName"
            name="LastName"
            placeholder="Last Name"
            {...register('LastName', { required: 'Last name is required' })}
          />
          {errors.LastName && (
            <span className="error-message">{errors.LastName.message}</span>
          )}
        </div>

        {/* Email */}
        <div className="form-group">
          <label htmlFor="Email">Email</label>
          <input
            id="Email"
            type="Email"
            name="Email"
            placeholder="Email"
            {...register('Email', {
              required: 'Email is required',
              pattern: {
                value: /^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$/,
                message: 'Invalid email address',
              },
            })}
          />
          {errors.Email && (
            <span className="error-message">{errors.Email.message}</span>
          )}
        </div>

        {/* Username */}
        <div className="form-group">
          <label htmlFor="Username">Username</label>
          <input
            id="Username"
            name="Username"
            placeholder="Username"
            {...register('Username', { required: 'Username is required' })}
          />
          {errors.Username && (
            <span className="error-message">{errors.Username.message}</span>
          )}
        </div>

        {/* Password */}
        <div className="form-group">
          <label htmlFor="Password">Password</label>
          <input
            id="Password"
            type="Password"
            name="Password"
            placeholder="Password"
            {...register('Password', { required: 'Password is required' })}
          />
          {errors.Password && (
            <span className="error-message">{errors.Password.message}</span>
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
