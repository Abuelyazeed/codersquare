import { useState, useEffect } from 'react';
import SignUp from './Components/SignUp';

function App() {
  const [posts, setPosts] = useState([]);
  const [user, setUser] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchPosts = async () => {
      try {
        const response = await fetch(`http://localhost:5220/api/posts`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        setPosts(data);
      } catch (error) {
        console.error('Error fetching posts:', error);
        setError('Failed to fetch posts. Please try again later.');
      }
    };

    fetchPosts();
  }, []);

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
    <div>
      {/* <h1>All Posts</h1>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      {posts.length > 0 ? (
        <ul>
          {posts.map((post) => (
            <li key={post.id}>
              <h2>{post.title}</h2>
              <p>
                <a href={post.url} target="_blank" rel="noopener noreferrer">
                  {post.url}
                </a>
              </p>
              <p>
                <strong>Posted At:</strong>
                {new Date(post.postedAt).toLocaleString()}
              </p>
              <p>
                <strong>Comments:</strong>
                {post.comments.length}
              </p>
              <p>
                <strong>Likes:</strong> {post.likes.length}
              </p>
            </li>
          ))}
        </ul>
      ) : (
        <p>Loading posts...</p>
      )} */}
      <SignUp />
    </div>
  );
}

export default App;
