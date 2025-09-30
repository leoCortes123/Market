// src/Navbar.tsx
import {
  Box,
  Button,
  Link as ChakraLink,
  CloseButton,
  Drawer,
  Flex,
  Icon,
  IconButton,
  Menu,
  Portal,
  Spacer,
  Text,
  useBreakpointValue
} from '@chakra-ui/react';
import type { ComponentProps } from 'react';
import { FaHome, FaStoreAlt } from 'react-icons/fa';
import { FaCalendarDays } from 'react-icons/fa6';
import { FiLogIn } from 'react-icons/fi';
import { GiHamburgerMenu } from "react-icons/gi";
import { MdOutlineBusiness } from 'react-icons/md';

type CustomNavLinkProps = {
  to: string;
  icon: ComponentProps<typeof Icon>['as'];
  children: React.ReactNode;
  onClick?: () => void;
};

function CustomNavLink({ to, icon, children, onClick }: CustomNavLinkProps) {
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
      fontSize={{ base: 'md', md: 'sm' }}
      whiteSpace="nowrap"
      onClick={onClick}
    >
      <Icon as={icon} mr={2} />
      <Text>{children}</Text>
    </ChakraLink>
  );
}

export default function Navbar() {
  const isMobile = useBreakpointValue({ base: true, sm: false });

  return (
    <Flex
      as="nav"
      bg="green.500"
      borderBottom="1px"
      borderColor="gray.200"
      px={{ base: 4, md: 6 }}
      py={3}
      align="center"
      position="fixed"
      top="0"
      left="0"
      right="0"
      zIndex="1000"
      boxShadow="sm"
      width="full"
      height={{ base: '12vh', md: '10vh' }}
    >
      <Box>
        <ChakraLink
          href="/"
          fontWeight="bold"
          fontSize={{ base: 'md', md: 'lg' }}
          _hover={{ textDecoration: 'none', color: 'teal.600' }}
        >
          Mercados Campesinos
        </ChakraLink>
      </Box>

      <Spacer />

      {!isMobile ? (
        <Flex gap={2} wrap="nowrap">
          <CustomNavLink to="/home" icon={FaHome}>
            Pagina principal
          </CustomNavLink>

          <Menu.Root>
            <Menu.Trigger asChild>
              <Button size="sm" variant="outline">
                Catalogo
              </Button>
            </Menu.Trigger>
            <Portal>
              <Menu.Positioner>
                <Menu.Content>
                  <Menu.Item key={"productosNav"} asChild value={"productosNav"}>
                    <CustomNavLink to="/products" icon={FaStoreAlt}>
                      Productos
                    </CustomNavLink>
                  </Menu.Item>
                  <Menu.Item key={"cajasNav"} asChild value={"cajasNav"}>
                    <CustomNavLink to="/products" icon={FaStoreAlt}>
                      Cajas
                    </CustomNavLink>
                  </Menu.Item>
                  <Menu.Item key={"deTemporadaNav"} asChild value={"deTemporadaNav"}>
                    <CustomNavLink to="/products" icon={FaStoreAlt}>
                      De Temporada
                    </CustomNavLink>
                  </Menu.Item>
                  <Menu.Item key={"suscripcionesNav"} asChild value={"suscripcionesNav"}>
                    <CustomNavLink to="/products" icon={FaStoreAlt}>
                      Suscripciones
                    </CustomNavLink>
                  </Menu.Item>
                  <Menu.Item key={"recetasNav"} asChild value={"recetasNav"}>
                    <CustomNavLink to="/products" icon={FaStoreAlt}>
                      Recetas
                    </CustomNavLink>
                  </Menu.Item>
                </Menu.Content>
              </Menu.Positioner>
            </Portal>
          </Menu.Root>

          <CustomNavLink to="/calendar" icon={FaCalendarDays}>
            Calendario
          </CustomNavLink>
          <CustomNavLink to="/aboutUs" icon={MdOutlineBusiness}>
            Razon social
          </CustomNavLink>
          <CustomNavLink to="/login" icon={FiLogIn}>
            Iniciar Sesión
          </CustomNavLink>
        </Flex>
      ) : (
        <Drawer.Root >
          <Drawer.Trigger asChild>
            <IconButton variant="outline" size="sm">
              <GiHamburgerMenu />
            </IconButton >
          </Drawer.Trigger>
          <Portal>
            <Drawer.Backdrop />
            <Drawer.Positioner>
              <Drawer.Content>
                <Drawer.Body pt={20} >
                  <CustomNavLink to="/home" icon={FaHome}>
                    Pagina principal
                  </CustomNavLink>
                  <CustomNavLink to="/products" icon={FaStoreAlt}>
                    Catalogo
                  </CustomNavLink>
                  <CustomNavLink to="/calendar" icon={FaCalendarDays}>
                    Calendario
                  </CustomNavLink>
                  <CustomNavLink to="/aboutUs" icon={MdOutlineBusiness}>
                    Razon social
                  </CustomNavLink>
                  <CustomNavLink to="/login" icon={FiLogIn}>
                    Iniciar Sesión
                  </CustomNavLink>
                </Drawer.Body>
                <Drawer.CloseTrigger asChild>
                  <CloseButton size="sm" />
                </Drawer.CloseTrigger>
              </Drawer.Content>
            </Drawer.Positioner>
          </Portal>
        </Drawer.Root>
      )}


    </Flex>
  );
}
