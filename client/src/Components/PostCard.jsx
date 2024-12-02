import { Box, Flex, Text, Stack, Button, Badge } from '@chakra-ui/react';

function PostCard({ post }) {
  return (
    <Box
      w="100%" // Full width of the container
      border="1px solid"
      borderColor="gray.700"
      borderRadius="md"
      bg="gray.800"
      p={4}
      mb={4}
      boxShadow="md"
    >
      <Flex direction="column">
        {/* Post Title */}
        <Text fontSize="lg" fontWeight="bold" color="white" mb={2}>
          {post.title}
        </Text>

        {/* Post URL */}
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

        {/* Posted At */}
        <Text fontSize="sm" color="gray.500" mb={2}>
          <strong>Posted At:</strong> {new Date(post.postedAt).toLocaleString()}
        </Text>

        {/* Comments and Likes */}
        <Stack direction="row" spacing={4} mb={4}>
          <Badge colorScheme="green">{post.comments.length} Comments</Badge>
          <Badge colorScheme="teal">{post.likes.length} Likes</Badge>
        </Stack>

        {/* Action Buttons */}
        <Flex justify="space-between">
          <Button size="sm" colorScheme="blue">
            Read More
          </Button>
          <Button size="sm" colorScheme="red">
            Share
          </Button>
        </Flex>
      </Flex>
    </Box>
  );
}

export default PostCard;
