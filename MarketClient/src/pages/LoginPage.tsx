import { Flex } from '@chakra-ui/react';
import Login from '../components/Login';
import Register from '../components/Register';

const LoginPage = () => {


  return (
    <Flex direction="row" width="100%" height="100%" align="center" justify="center" p={0} >
      <Login />
      <Register />
    </Flex>
  );
};

export default LoginPage;
