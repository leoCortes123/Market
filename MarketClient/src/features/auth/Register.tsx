import { useRegisterMutation } from '@/store/api/authApi';
import type { RegisterRequest } from '@/types/auth';
import {
  Box,
  Button,
  defineStyle,
  Field,
  Fieldset,
  Flex,
  Heading,
  Icon,
  Input,
  RadioGroup,

  Text
} from '@chakra-ui/react';
import { useState } from 'react';
import { useForm } from 'react-hook-form';
import { FaFacebook, FaGithub, FaGoogle } from 'react-icons/fa';
import { Form, useNavigate } from 'react-router-dom';

const Register = () => {
  const [error, setError] = useState<string | null>(null);
  const [isBuyer, setIsBuyer] = useState<string | null>("1");
  const [registerMutation] = useRegisterMutation();
  const navigate = useNavigate();

  const {
    register,
    handleSubmit,
    formState: { errors },
  } = useForm<RegisterRequest>();


  const onSubmit = async (data: RegisterRequest) => {
    try {
      setError(null);
      const userData: RegisterRequest = {
        ...data,
        isFarmerDistributor: isBuyer === '2'
      };

      await registerMutation(userData).unwrap();
      console.log('Datos enviados:', userData);
      navigate('/home');
    } catch (err) {
      setError('Error al registrar. Intenta de nuevo.');
      console.error('Registration error:', err);
    }
  };

  const floatingStyles = defineStyle({
    pos: "absolute",
    bg: "gray.100",
    px: "0.5",
    top: "-3",
    insetStart: "2",
    fontWeight: "normal",
    pointerEvents: "none",
    transition: "position",
    _peerPlaceholderShown: {
      color: "fg.muted",
      top: "1.5",
      insetStart: "3",
    },
    _peerFocusVisible: {
      color: "fg",
      top: "-3",
      insetStart: "2",
    },
  });


  return (
    <Flex
      direction='column'
      p={5}
      width="100%"
      height="100%"
      justify="center"
      align="center"
      animationName="slide-from-right-full"
      animationDuration="0.5s"
      animationTimingFunction="ease-in-out"
    >
      <Heading as="h2" size="xl" mb={6} textAlign="center">
        Registro de Usuario
      </Heading>

      <Form onSubmit={handleSubmit(onSubmit)} style={{ width: '90%', height: '100%' }}>
        <Fieldset.Root width="100" id="register-form" >
          <Field.Root id="email">
            <Input
              className="peer"
              placeholder=""
              size="xs"
              color="black"
              type="email"
              {...register('email', { required: true })}
            />
            <Field.Label css={floatingStyles}>Email</Field.Label>
            {errors.email && <Text color="red.500">El email es requerido</Text>}
          </Field.Root>

          <Field.Root id="password">
            <Input
              className="peer"
              placeholder=""
              size="xs"
              color="black"
              type='password'
              {...register('password', { required: true })}
            />
            <Field.Label css={floatingStyles}>Contraseña</Field.Label>
            {errors.password && <Text color="red.500">La contraseña es requerida</Text>}
          </Field.Root>


          <Field.Root id="fullName" orientation="horizontal">
            <Input
              className="peer"
              placeholder=""
              size="xs"
              color="black"
              _placeholder={{ color: 'gray.400' }}
              {...register('fullName')}
            />
            <Field.Label css={floatingStyles}>Nombre completo</Field.Label>
          </Field.Root>

          <Field.Root id="phone" orientation="horizontal">
            <Input
              className="peer"
              placeholder=""
              size="xs"
              color="black"
              _placeholder={{ color: 'gray.400' }}
              {...register('phone')}
            />
            <Field.Label css={floatingStyles}>Teléfono</Field.Label>
          </Field.Root>

          <Flex align="center" gap={4} mt={4} mb={2}>
            <Text fontSize="md">Quieres:</Text>
            <RadioGroup.Root
              colorPalette="green"
              value={isBuyer ?? '1'}
              onValueChange={(details) => setIsBuyer(details.value)}
              spaceX="8"
            >
              {[{ value: '1', label: 'Comprar' }, { value: '2', label: 'Vender' }].map((item) => (
                <RadioGroup.Item key={item.value} value={item.value}>
                  <RadioGroup.ItemHiddenInput />
                  <RadioGroup.ItemIndicator />
                  <RadioGroup.ItemText>{item.label}</RadioGroup.ItemText>
                </RadioGroup.Item>
              ))}
            </RadioGroup.Root>
          </Flex>

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
      </Form>

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
    </Flex>
  );
};

export default Register;
