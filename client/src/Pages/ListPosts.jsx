import { useState, useEffect } from 'react';
import { Box, Text, Button, Stack } from '@chakra-ui/react';
import PostCard from '../Components/PostCard';

function ListPosts() {
  const [posts, setPosts] = useState([]);
  const [loading, setLoading] = useState(true);
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
      } finally {
        setLoading(false);
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
    <Box w="100%" minH="100vh" px={6}>
      {/* Error Message */}
      {error && (
        <Box bg="red.700" p={4} borderRadius="md" mb={4}>
          <Text color="white" mb={2}>
            {error}
          </Text>
          <Button onClick={handleRetry} colorScheme="blue" size="sm">
            Retry
          </Button>
        </Box>
      )}

      {/* Loading State */}
      {!error && loading && (
        <Text color="gray.400" mb={4}>
          Loading posts...
        </Text>
      )}

      {/* Posts Display */}
      {!error && posts.length > 0 ? (
        <Stack spacing={4}>
          {posts.map((post) => (
            <PostCard key={post.id} post={post} />
          ))}
        </Stack>
      ) : (
        !error && !loading && <Text color="gray.500">No posts available.</Text>
      )}
    </Box>
  );
}

export default ListPosts;
