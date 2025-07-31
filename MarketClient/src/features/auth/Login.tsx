import { PasswordInput } from '@/features/components/ui/password-input';
import { useLoginMutation } from '@/store/api/authApi';
import type { LoginRequest } from '@/types/authEntities';
import {
  Box,
  Button,
  Checkbox,
  Field,
  Fieldset,
  Flex,
  Heading,
  Icon,
  Input,
  Link,
  Text,
} from '@chakra-ui/react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import { FaFacebook, FaGoogle } from 'react-icons/fa';
import { Form, useNavigate } from 'react-router-dom';

const Login = () => {

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<LoginRequest>();

  const [login] = useLoginMutation();
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  const onSubmit = async (data: LoginRequest) => {
    try {
      setError(null);
      await login(data).unwrap();
      navigate('/home');
    } catch (err) {
      setError(`Credenciales inválidas. Por favor, inténtalo de nuevo.' ${err}`);
      console.error('Login failed:', error);
    }
  };

  return (
    <Flex
      direction='column'
      p={5}
      width="100%"
      height="100%"
      justify="center"
      align="center"
      animationName=" slide-from-left-full"
      animationDuration="0.5s"
      animationTimingFunction="ease-in-out"
    >
      <Heading as="h2" size="xl" mb={6} textAlign="center">
        Iniciar Sesión
      </Heading>

      <Form onSubmit={handleSubmit(onSubmit)} style={{ width: '90%', height: '100%' }}>
        <Fieldset.Root id="login-form">
          <Field.Root id="email" mb={4}>
            <Field.Label>Email</Field.Label>
            <Field.RequiredIndicator />
            <Input
              color={"black"}
              type="email"
              placeholder="mail@email.com"
              _placeholder={{ color: "gray.400" }}
              {...register("email", { required: true })}
            />
            {errors.email && <Text color="red.500">El email es requerido</Text>}
          </Field.Root>

          <Field.Root id="password" mb={4}>
            <Field.Label>Contraseña</Field.Label>
            <Field.RequiredIndicator />
            <PasswordInput
              color={"black"}
              _placeholder={{ color: "gray.400" }}
              placeholder="********"
              {...register("password", { required: true })}
            />
            {errors.password && <Text color="red.500">La contraseña es requerida</Text>}
          </Field.Root>

          <Flex justify="space-between" mb={6}>
            <Checkbox.Root>
              <Checkbox.HiddenInput />
              <Checkbox.Control>
                <Checkbox.Indicator />
                <Checkbox.Label>Recordarme</Checkbox.Label>
              </Checkbox.Control>
            </Checkbox.Root>
            <Link color="blue.500" href="#">
              ¿Olvidaste tu contraseña?
            </Link>
          </Flex>

          <Button
            type="submit"
            colorScheme="blue"
            size="lg"
            fontSize="md"
            w="full"
            loadingText="Iniciando sesión..."
          >
            Iniciar Sesión
          </Button>
        </Fieldset.Root>
      </Form>

      <Flex align="center" my={6}>
        <Box flex={1} h="1px" bg="gray.300" />
        <Text mx={4} color="gray.500" fontSize="sm">
          O continúa con
        </Text>
        <Box flex={1} h="1px" bg="gray.300" />
      </Flex>

      <Flex justify="center" gap={4}>
        <Button
          variant="outline"
          colorScheme="red"
          flex={1}
        >
          <Icon as={FaGoogle} /> Google
        </Button>
        <Button
          variant="outline"
          colorScheme="facebook"
          flex={1}
        >
          <Icon as={FaFacebook} /> Facebook
        </Button>
      </Flex>
    </Flex>
  );
};

export default Login;
