import { Flex } from '@chakra-ui/react';
import { Outlet } from 'react-router-dom';
import Navbar from './features/components/NavBar';

function App() {
  return (
    <>
      <Navbar />
      <Flex
        direction="column"
        align="center"
        justify="center"
        width="100vw"
        height="93vh"
        bg="white"
        colorPalette="green">
        <Outlet />
      </Flex>
    </>
  );
}

export default App;
