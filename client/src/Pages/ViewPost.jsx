import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Box, Text, Button, Stack, Badge, Flex } from '@chakra-ui/react';
import { jwtDecode } from 'jwt-decode';

function ViewPost() {
  const { id } = useParams();

  const [post, setPost] = useState(null);
  const [error, setError] = useState(null);
  const [isLiked, setIsLiked] = useState(false);
  const [comment, setComment] = useState('');

  // Extract userId from the JWT token stored in localStorage
  const getUserIdFromToken = () => {
    const token = localStorage.getItem('authToken');
    if (token) {
      try {
        const decoded = jwtDecode(token);
        return decoded.nameid; // Assuming the token contains userId
      } catch (error) {
        console.error('Error decoding token:', error);
        return null;
      }
    }
    return null;
  };

  const currentUserId = getUserIdFromToken();

  //LIKE
  const handleLike = async () => {
    if (!currentUserId) {
      alert('You must be logged in to like a post');
      return;
    }

    try {
      const response = await fetch(`http://localhost:5220/api/likes/${id}`, {
        method: isLiked ? 'DELETE' : 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('authToken')}`, // Send token if needed
        },
      });
      if (response.ok) {
        setIsLiked(!isLiked);
        fetchPost();
      }
    } catch (error) {
      console.error('Error liking post:', error);
    }
  };

  //Comment
  const handleComment = async () => {
    if (!currentUserId) {
      alert('You must be logged in to comment on a post');
      return;
    }

    if (!comment.trim()) {
      alert('Comment cannot be empty');
      return;
    }

    try {
      const response = await fetch(`http://localhost:5220/api/comments/${id}`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
          Authorization: `Bearer ${localStorage.getItem('authToken')}`,
        },
        body: JSON.stringify({
          content: comment,
        }),
      });
      if (response.ok) {
        setComment('');
        fetchPost();
      } else {
        console.error('Failed to add comment:', response.status);
      }
    } catch (error) {
      console.error('Error commenting on post:', error);
    }
  };

  const fetchPost = async () => {
    try {
      const response = await fetch(`http://localhost:5220/api/posts/${id}`);
      if (!response.ok) {
        throw new Error(`HTTP error! status: ${response.status}`);
      }

      const data = await response.json();
      setPost(data);

      const liked = data.likes.some((like) => like.userId === currentUserId);
      setIsLiked(liked);
    } catch (error) {
      console.error('Error fetching post:', error);
      setError('Failed to fetch post');
    }
  };

  useEffect(() => {
    fetchPost();
  }, [id]);

  if (!post) {
    return <Text>Loading...</Text>;
  }

  if (error) {
    return (
      <Box bg="red.700" p={4} borderRadius="md">
        <Text color="white">{error}</Text>
      </Box>
    );
  }

  return (
    <Box p={4} w="100%" maxW="900px" mx="auto">
      {/* Post Details */}
      <Box
        mb={6}
        bg="gray.800"
        p={6}
        borderRadius="md"
        border="1px solid"
        borderColor="gray.700"
      >
        <Text fontSize="2xl" fontWeight="bold" color="white" mb={2}>
          {post.title}
        </Text>
        <Text fontSize="md" color="gray.400" mb={2}>
          <strong>URL:</strong>{' '}
          <a
            href={post.url}
            target="_blank"
            rel="noopener noreferrer"
            style={{ color: '#3182CE' }}
          >
            {post.url}
          </a>
        </Text>
        <Text fontSize="sm" color="gray.500" mb={4}>
          <strong>Posted At:</strong> {new Date(post.postedAt).toLocaleString()}
        </Text>

        {/* Likes and Comments */}
        <Stack direction="row" spacing={4} mb={4}>
          <Badge colorScheme="green">{post.comments.length} Comments</Badge>
          <Badge colorScheme="teal">{post.likes.length} Likes</Badge>
        </Stack>
      </Box>

      {/* Comments Section */}
      {post.comments.length > 0 ? (
        <Box
          bg="gray.700"
          p={4}
          borderRadius="md"
          border="1px solid"
          borderColor="gray.600"
        >
          <Text fontSize="xl" fontWeight="bold" color="white" mb={4}>
            Comments:
          </Text>
          <Stack spacing={4}>
            {post.comments.map((comment) => (
              <Box key={comment.id} p={4} bg="gray.800" borderRadius="md">
                <Text color="white">{comment.content}</Text>
                <Text fontSize="sm" color="gray.400">
                  Posted at: {new Date(comment.postedAt).toLocaleString()}
                </Text>
              </Box>
            ))}
          </Stack>
        </Box>
      ) : (
        <Text color="gray.400" mb={4}>
          No comments yet.
        </Text>
      )}

      {/* Action Button */}
      <Flex justify="space-between" mt={4}>
        <Flex align="center">
          <Box mr={2}>
            <Button size="sm" colorScheme="blue" onClick={handleLike}>
              {isLiked ? 'Unlike' : 'Like'}
            </Button>
          </Box>
          <Flex align="center">
            <Button size="sm" colorScheme="blue" onClick={handleComment}>
              Comment
            </Button>
            <input
              type="text"
              placeholder="Write a comment..."
              value={comment}
              onChange={(e) => setComment(e.target.value)}
              style={{
                padding: '6px',
                borderRadius: '4px',
                border: '1px solid #ccc',
                marginLeft: '8px',
              }}
            />
          </Flex>
        </Flex>
        <Button size="sm" colorScheme="red">
          Share
        </Button>
      </Flex>
    </Box>
  );
}
export default ViewPost;
