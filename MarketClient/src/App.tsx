import { Box, Link as ChakraLink, Flex, Heading, Icon, Text, VStack } from '@chakra-ui/react';
import { type ComponentProps } from 'react';
import { FiHome, FiLogIn, FiUserPlus } from 'react-icons/fi';
import { Outlet } from 'react-router-dom';

function App() {
  return (
    <Flex height="100vh" bg="gray.50">
      {/* Barra de navegación lateral */}
      <Box
        flex={1}
        bg="white"
        borderRight="1px"
        borderColor="gray.200"
        h="100vh"
        p={4}
        boxShadow="sm"
      >
        <Heading size="md" mb={8} pl={2} color="teal.600">
          Mi Aplicación
        </Heading>

        <VStack align="flex-start" >
          <CustomNavLink to="home" icon={FiHome}>
            Home
          </CustomNavLink>
          <CustomNavLink to="login" icon={FiLogIn}>
            Iniciar Sesión
          </CustomNavLink>
          <CustomNavLink to="register" icon={FiUserPlus}>
            Registrarse
          </CustomNavLink>
        </VStack>
      </Box>

      {/* Contenido principal */}
      <Box flex="9">
        <Outlet />
      </Box>
    </Flex>
  );
}

type CustomNavLinkProps = {
  to: string;
  icon: ComponentProps<typeof Icon>['as'];
  children: React.ReactNode;
};

function CustomNavLink({ to, icon, children }: CustomNavLinkProps) {
  return (
    <ChakraLink
      href={to}
      w="full"
      p={3}
      borderRadius="md"
      _hover={{
        bg: 'teal.50',
        color: 'teal.600',
      }}
      display="flex"
      alignItems="center"
    >
      <Icon as={icon} mr={3} />
      <Text>{children}</Text>
    </ChakraLink>
  );
}

export default App;
