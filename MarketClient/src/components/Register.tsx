import { PasswordInput } from '@/components/ui/password-input';
import { useRegisterMutation } from '@/store/api/authApi';
import type { User } from '@/types/Entities';
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
import { FaFacebook, FaGithub, FaGoogle } from 'react-icons/fa';
import { useNavigate } from 'react-router-dom';



const Register = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<User>();

  const [registerMutation] = useRegisterMutation();
  const navigate = useNavigate();
  const [error, setError] = useState<string | null>(null);

  const onSubmit = async (data: User) => {
    try {
      setError(null);
      await registerMutation(data).unwrap();
      console.log('Datos enviados:', data);
      navigate('/home');
    } catch (err) {
      setError('Error al registrar. Intenta de nuevo.');
      console.error('Registration error:', err);
    }
  };

  return (
    <Flex direction="column" width="100%" height="100%" align="center" justify="center" p={0} bg="gray.50">
      <Box rounded="lg" bg="white" boxShadow="lg" p={8} width="50%">
        <Heading as="h2" size="xl" mb={6} textAlign="center">
          Registro de Usuario
        </Heading>

        <form onSubmit={handleSubmit(onSubmit)}>
          <Fieldset.Root id="register-form">
            <Field.Root id="username" mb={4}>
              <Field.Label>Nombre de usuario</Field.Label>
              <Field.RequiredIndicator />
              <Input
                color="black"
                placeholder="usuario123"
                _placeholder={{ color: 'gray.400' }}
                {...register('username', { required: true })}
              />
              {errors.username && <Text color="red.500">El nombre de usuario es requerido</Text>}
            </Field.Root>

            <Field.Root id="email" mb={4}>
              <Field.Label>Email</Field.Label>
              <Field.RequiredIndicator />
              <Input
                color="black"
                type="email"
                placeholder="mail@email.com"
                _placeholder={{ color: 'gray.400' }}
                {...register('email', { required: true })}
              />
              {errors.email && <Text color="red.500">El email es requerido</Text>}
            </Field.Root>

            <Field.Root id="password" mb={4}>
              <Field.Label>Contraseña</Field.Label>
              <Field.RequiredIndicator />
              <PasswordInput
                color="black"
                _placeholder={{ color: 'gray.400' }}
                placeholder="********"
                {...register('password', { required: true })}
              />
              {errors.password && <Text color="red.500">La contraseña es requerida</Text>}
            </Field.Root>

            <Field.Root id="fullName" mb={4}>
              <Field.Label>Nombre completo</Field.Label>
              <Input
                color="black"
                placeholder="Juan Pérez"
                _placeholder={{ color: 'gray.400' }}
                {...register('fullName')}
              />
            </Field.Root>

            <Field.Root id="phone" mb={4}>
              <Field.Label>Teléfono</Field.Label>
              <Input
                color="black"
                placeholder="1234567890"
                _placeholder={{ color: 'gray.400' }}
                {...register('phone')}
              />
            </Field.Root>

            <Checkbox.Root
              colorScheme="blue"
              mb={6}
              {...register('isFarmerDistributor')}
            >
              <Checkbox.HiddenInput />
              <Checkbox.Control />
              <Checkbox.Label>¿Eres agricultor?</Checkbox.Label>
            </Checkbox.Root>

            <Button
              type="submit"
              colorScheme="blue"
              size="lg"
              fontSize="md"
              w="full"
              loadingText="Registrando..."
            >
              Registrarse
            </Button>
          </Fieldset.Root>
        </form>

        {error && <Text color="red.500" mt={4}>{error}</Text>}

        <Flex align="center" my={6}>
          <Box flex={1} h="1px" bg="gray.300" />
          <Text mx={4} color="gray.500" fontSize="sm">
            O continúa con
          </Text>
          <Box flex={1} h="1px" bg="gray.300" />
        </Flex>

        <Flex justify="center" gap={4}>
          <Button variant="outline" colorScheme="red" flex={1}>
            <Icon as={FaGoogle} /> Google
          </Button>
          <Button variant="outline" colorScheme="facebook" flex={1}>
            <Icon as={FaFacebook} /> Facebook
          </Button>
          <Button variant="outline" flex={1}>
            <Icon as={FaGithub} /> GitHub
          </Button>
        </Flex>

        <Text mt={6} textAlign="center">
          ¿Ya tienes una cuenta?{' '}
          <Link color="blue.500" href="/login">
            Inicia sesión
          </Link>
        </Text>
      </Box>
    </Flex>
  );
};

export default Register;
