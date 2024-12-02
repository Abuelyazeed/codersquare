import { Box, Flex, Button, Text } from '@chakra-ui/react';

function NavBar() {
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
        Codersquare
      </Box>

      <Flex gap={4}>
        <Button variant="outline" size="sm">
          Sign Up
        </Button>
        <Button variant="solid" size="sm">
          Sign In
        </Button>
      </Flex>
    </Flex>
  );
}

export default NavBar;
