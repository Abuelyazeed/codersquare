import { useState, useEffect } from 'react';

function ListPosts() {
  const [posts, setPosts] = useState([]);
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

  const handleRetry = () => {
    setError(null); // Clear the previous error
    setPosts([]); // Optionally clear the previous posts
    fetchPosts(); // Retry fetching the posts
  };

  return (
    <div>
      <h1>All Posts</h1>
      {error && (
        <div>
          <p style={{ color: 'red' }}>{error}</p>
          <button onClick={handleRetry}>Retry</button> {/* Retry button */}
        </div>
      )}
      {!error && posts.length === 0 && <p>Loading posts...</p>}
      {!error && posts.length > 0 ? (
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
        !error && <p>No posts available.</p>
      )}
    </div>
  );
}
export default ListPosts;
