import { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import { Box, Text, Button, Stack, Badge, Flex } from '@chakra-ui/react';

function ViewPost() {
  const { id } = useParams();
  console.log(id);
  const [post, setPost] = useState(null);
  const [error, setError] = useState(null);

  useEffect(() => {
    const fetchPost = async () => {
      try {
        const response = await fetch(`http://localhost:5220/api/posts/${id}`);
        if (!response.ok) {
          throw new Error(`HTTP error! status: ${response.status}`);
        }

        const data = await response.json();
        setPost(data);
      } catch (error) {
        console.error('Error fetching post:', error);
        setError('Failed to fetch post');
      }
    };

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
        <Box>
          <Button size="sm" colorScheme="blue" mr={2}>
            Like
          </Button>
          <Button size="sm" colorScheme="blue">
            Comment
          </Button>
        </Box>
        <Button size="sm" colorScheme="red">
          Share
        </Button>
      </Flex>
    </Box>
  );
}
export default ViewPost;
