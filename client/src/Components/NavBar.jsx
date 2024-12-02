import { Box, Flex, Button, Text } from '@chakra-ui/react';
import { useNavigate } from 'react-router-dom';
import { Link } from 'react-router-dom';

function NavBar() {
  const navigate = useNavigate();

  const handleSignUp = () => {
    navigate('/signup');
  };

  const handleLogin = () => {
    navigate('/login');
  };

  const handleCreatePost = () => {
    navigate('/create');
  };

  const handleSignOut = () => {
    localStorage.removeItem('authToken');
    navigate('/login');
  };

  const isLoggedIn = localStorage.getItem('authToken') ? true : false;

  return (
    <Flex
      justify="space-between"
      align="center"
      bg="gray.900"
      p={4}
      borderBottom="1px solid"
      borderColor="gray.700"
      boxShadow="md"
      position="fixed"
      top={0}
      left={0}
      right={0}
      zIndex={1000}
    >
      <Box fontSize="xl" fontWeight="bold" color="white">
        <Link to="/" style={{ textDecoration: 'none', color: 'white' }}>
          Codersquare
        </Link>
      </Box>

      <Flex gap={4}>
        {!isLoggedIn ? (
          <>
            <Button variant="outline" size="sm" onClick={handleSignUp}>
              Sign Up
            </Button>
            <Button variant="solid" size="sm" onClick={handleLogin}>
              Login
            </Button>
          </>
        ) : (
          <>
            <Button variant="outline" size="sm" onClick={handleCreatePost}>
              Create Post
            </Button>
            <Button variant="solid" size="sm" onClick={handleSignOut}>
              Sign Out
            </Button>
          </>
        )}
      </Flex>
    </Flex>
  );
}

export default NavBar;
