import { Flex } from '@chakra-ui/react';
import { Outlet } from 'react-router-dom';
import Navbar from './features/components/NavBar';

function App() {
  return (
    <>
      <Navbar />
      <Flex
        id="app-container"
        direction="column"
        align="center"
        justify="center"
        width="full"
        height="90vh"
        bg="white"
        colorPalette="green">
        <Outlet />
      </Flex>
    </>
  );
}

export default App;
