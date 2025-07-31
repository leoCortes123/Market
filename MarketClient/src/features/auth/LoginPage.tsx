import {
  Button,
  Flex,
  Link,
  Text
} from '@chakra-ui/react';
import { useState } from 'react';
import { RiArrowLeftLine, RiArrowRightLine } from 'react-icons/ri';
import Login from './Login';
import Register from './Register';

const LoginPage = () => {
  const [isLogin, setIsLogin] = useState(true);

  return (
    <Flex
      direction="row"
      width="100%"
      height="100%"
      align="center"
      justify="center"
    >
      <Flex
        direction="row"
        width="80%"
        height="95%"
        align="center"
        justify="space-between"
        bg="gray.100"
        rounded="3rem"
        overflow="hidden"
        position="relative"
        boxShadow="lg"

      >
        <Flex
          width={"50%"}
          height="100%"
          justify="center"
          align="center"
        >
          {isLogin && <Login />}
        </Flex>

        <Flex
          width={"50%"}
          height="100%"
          justify="center"
          align="center"
        >
          {!isLogin && <Register />}
        </Flex>

        <Flex
          position="absolute"
          top="0"
          left="50%"
          direction="column"
          align="center"
          justify="center"
          boxShadow="lg"
          p={8}
          width="50%"
          height="100%"
          bg="blue.900"
          borderLeftRadius={isLogin ? '10rem' : '3rem'}
          borderRightRadius={isLogin ? '3rem' : '10rem'}
          transform={isLogin ? 'translateX(0)' : 'translateX(-100%)'}
          transition="all 0.5s ease-in-out"
        >

          <Text mt={6} fontSize="4xl" textAlign="center" color="white">
            ¡HOLA!
          </Text>
          <Text mt={6} mb="5%" textAlign="center" color="white" fontSize="lg">
            {isLogin
              ? 'Si no tienes una cuenta, puedes registrarte'
              : 'Si ya tienes una cuenta, puedes iniciar sesión'
            }
          </Text>
          <Button asChild>
            <Link color="white" onClick={() => setIsLogin(!isLogin)}>
              {!isLogin && <RiArrowLeftLine />}
              ¡AQUI!
              {isLogin && <RiArrowRightLine />}
            </Link>
          </Button>
        </Flex>
      </Flex>
    </Flex>
  );
};

export default LoginPage;
