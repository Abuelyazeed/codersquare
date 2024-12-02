import { useForm } from 'react-hook-form';
import './SignUp.css';
import { useState } from 'react';
import { useNavigate } from 'react-router-dom';

function Login() {
  const {
    register,
    handleSubmit,
    formState: { errors },
    setError,
  } = useForm();
  const [message, setMessage] = useState('');
  const navigate = useNavigate();

  const onSubmit = async (data) => {
    try {
      // Send the form data to the backend for login
      const response = await fetch('http://localhost:5220/api/login', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
      });

      if (response.ok) {
        const result = await response.json();
        const token = result.newUserDto.token;
        localStorage.setItem('authToken', token);
        navigate('/');
        setMessage('Login successful!');
      } else {
        const result = await response.json();
        if (result.errors) {
          // If there are validation errors from the server
          Object.keys(result.errors).forEach((field) => {
            setError(field, {
              type: 'manual',
              message: result.errors[field][0], // Use the first error message
            });
          });
          setMessage('There are validation errors. Please check your inputs.');
        } else {
          setMessage(result.message || 'An error occurred during login.');
        }
      }
    } catch (error) {
      console.error('Error during login:', error);
      setMessage('An error occurred during login.');
    }
  };

  return (
    <div className="signup-container">
      <h1>Login</h1>
      <form onSubmit={handleSubmit(onSubmit)}>
        <div className="form-group">
          <label htmlFor="emailOrUsername">Email or Username</label>
          <input
            id="emailOrUsername"
            name="emailOrUsername"
            placeholder="emailOrUsername"
            {...register('emailOrUsername', {
              required: 'Username is required',
            })}
          />
          {errors.emailOrUsername && (
            <span className="error-message">
              {errors.emailOrUsername.message}
            </span>
          )}
        </div>

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
          Login
        </button>
      </form>
      {message && <div className="message">{message}</div>}
    </div>
  );
}
export default Login;
