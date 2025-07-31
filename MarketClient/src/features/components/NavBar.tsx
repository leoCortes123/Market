// src/Navbar.tsx
import {
  Box,
  Link as ChakraLink,
  Flex,
  Icon,
  Spacer,
  Text,
} from '@chakra-ui/react';
import type { ComponentProps } from 'react';
import { FaStoreAlt } from 'react-icons/fa';
import { FiLogIn } from 'react-icons/fi';

type CustomNavLinkProps = {
  to: string;
  icon: ComponentProps<typeof Icon>['as'];
  children: React.ReactNode;
};

function CustomNavLink({ to, icon, children }: CustomNavLinkProps) {
  return (
    <ChakraLink
      href={to}
      px={4}
      py={3}
      borderRadius="md"
      _hover={{
        bg: 'teal.50',
        color: 'teal.600',
        textDecoration: 'none',
      }}
      display="flex"
      alignItems="center"
      fontSize="sm"
      whiteSpace="nowrap"
    >
      <Icon as={icon} mr={2} />
      <Text>{children}</Text>
    </ChakraLink>
  );
}

export default function Navbar() {
  return (
    <Flex
      as="nav"
      bg="green.500"
      borderBottom="1px"
      borderColor="gray.200"
      px={6}
      py={3}
      align="center"
      position="fixed"
      top="0"
      left="0"
      right="0"
      zIndex="1000"
      boxShadow="sm"
      width="100vw"
      height="7vh"
    >
      <Box>
        <ChakraLink
          href="/"
          fontWeight="bold"
          fontSize="lg"
          _hover={{ textDecoration: 'none', color: 'teal.600' }}
        >
          Mi Aplicación
        </ChakraLink>
      </Box>

      <Spacer />

      <Flex gap={2} wrap="nowrap">
        <CustomNavLink to="/products" icon={FaStoreAlt}>
          Productos
        </CustomNavLink>
        <CustomNavLink to="/login" icon={FiLogIn}>
          Iniciar Sesión
        </CustomNavLink>
      </Flex>
    </Flex>
  );
}
