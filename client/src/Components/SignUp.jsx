import { useState } from 'react';

function SignUp() {
  const [signupData, setSignupData] = useState({
    firstName: '',
    lastName: '',
    email: '',
    username: '',
    password: '',
  });
  const [message, setMessage] = useState('');

  const handleChange = (e) => {
    const { name, value } = e.target;
    setSignupData((prevData) => ({
      ...prevData,
      [name]: value,
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await fetch(`http://localhost:5220/api/signup`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(signupData),
      });

      if (response.ok) {
        setMessage('Sign-up successful! Please log in.');
      } else {
        const data = await response.json();
        setMessage(`Error: ${data.message}`);
      }
    } catch (error) {
      console.error('Error during sign-up:', error);
      setMessage('An error occurred during sign-up.');
    }
  };

  return (
    <div>
      <h2>Sign up</h2>
      <form onSubmit={handleSubmit}>
        <label>
          First Name:
          <input
            type="text"
            name="firstName"
            value={signupData.firstName}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label>
          Last Name:
          <input
            type="text"
            name="lastName"
            value={signupData.lastName}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label>
          Email
          <input
            type="email"
            name="email"
            value={signupData.email}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label>
          Username:
          <input
            type="text"
            name="username"
            value={signupData.username}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <label>
          Password:
          <input
            type="password"
            name="password"
            value={signupData.password}
            onChange={handleChange}
            required
          />
        </label>
        <br />
        <button type="submit">Sign Up</button>
      </form>
      {message && <p>{message}</p>}
    </div>
  );
}
export default SignUp;
