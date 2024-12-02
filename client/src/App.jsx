import { useState, useEffect } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import SignUp from './Components/SignUp';
import ListPosts from './Pages/ListPosts';
import ViewPost from './Pages/ViewPost';

function App() {
  const [user, setUser] = useState(null);

  const fetchUserById = async () => {
    const userId = '1219E8BD-F269-4EB3-B24B-9A10151C1EFB';
    try {
      const response = await fetch(`http://localhost:5220/api/users/${userId}`);

      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();
      setUser(data);
    } catch {
      console.error('Error fetching user:', err);
    }
  };

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<ListPosts />} />
        <Route path="/p/:id" element={<ViewPost />} />
      </Routes>
    </BrowserRouter>
  );
}

export default App;
