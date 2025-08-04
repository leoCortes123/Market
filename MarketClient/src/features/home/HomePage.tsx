import {
  Box,
  Link as ChakraLink,
  Grid,
  Heading,
  Image,
  Separator,
  Stack,
  Text,
  VStack
} from '@chakra-ui/react';
import { Link } from 'react-router-dom';
import { Autoplay, Navigation, Pagination } from 'swiper/modules';
import { Swiper, SwiperSlide } from 'swiper/react';

import 'swiper/css';
import 'swiper/css/autoplay';
import 'swiper/css/navigation';
import 'swiper/css/pagination';

const slides = [
  {
    id: 1,
    title: 'Slide 1',
    description: 'Descripción del primer slide',
    imageUrl: '/banner/banner1.jpg',
  },
  {
    id: 2,
    title: 'Slide 2',
    description: 'Descripción del segundo slide',
    imageUrl: '/banner/banner2.jpg',
  },
  {
    id: 3,
    title: 'Slide 3',
    description: 'Descripción del tercer slide',
    imageUrl: '/banner/banner3.jpg',
  }
];

const categorias = [
  { name: 'Frutas', image: 'categories/frutas.png', link: '/productos/frutas' },
  { name: 'Verduras', image: 'categories/verduras.png', link: '/productos/verduras' },
  { name: 'Tubérculos', image: 'categories/tuberculos.png', link: '/productos/tuberculos' },
  { name: 'Hierbas', image: 'categories/hierbas.png', link: '/productos/hierbas' },
  { name: 'Procesados', image: 'categories/procesados.png', link: '/productos/procesados' },
];

const canastas = [
  { name: 'Canasta básica', image: 'basket/caja1.jpg', link: '/canastas/1' },
  { name: 'Canasta saludable', image: 'basket/caja2.jpg', link: '/canastas/2' },
  { name: 'Canasta familiar', image: 'basket/caja3.jpg', link: '/canastas/3' },
  { name: 'Canasta premium', image: 'basket/caja4.jpg', link: '/canastas/4' },
];

const ofertas = [
  { name: 'Bulto de papa', image: 'offers/offer1.jpg', link: '/ofertas/papas' },
  { name: 'Guanabana', image: 'offers/offer2.jpg', link: '/ofertas/lechuga' },
  { name: 'Mango', image: 'offers/offer3.jpg', link: '/ofertas/lechuga' },
  { name: 'Arepas', image: 'offers/offer4.jpg', link: '/ofertas/lechuga' },
  { name: 'Queso', image: 'offers/offer5.jpg', link: '/ofertas/lechuga' }
];

const SectionTitle = ({ children }: { children: React.ReactNode; }) => (
  <Heading as="h2" size="lg" my={6} px={4} textAlign="center" color="green.700">
    {children}
  </Heading>
);

const HomePage = () => {
  return (
    <Stack
      id="home-page"
      direction="column"
      w="full"
      minH="90vh"
      bg="gray.50"
    >
      {/* Swiper Banner */}
      <Box w="full" h={{ base: "50vh", md: "70vh" }} mb={8}>
        <Swiper
          modules={[Navigation, Pagination, Autoplay]}
          navigation
          pagination={{ clickable: true }}
          autoplay={{ delay: 5000 }}
          loop={true}
          style={{ width: '100%', height: '100%' }}
        >
          {slides.map((slide) => (
            <SwiperSlide key={slide.id}>
              <Box
                w="full"
                h="full"
                position="relative"
                display="flex"
                alignItems="center"
                justifyContent="center"
              >
                <Image
                  src={slide.imageUrl}
                  alt={slide.title}
                  w="full"
                  h="full"
                  objectFit="cover"
                />
                <Box
                  position="absolute"
                  bottom="20%"
                  left="10%"
                  bg="blackAlpha.700"
                  color="white"
                  p={4}
                  borderRadius="md"
                  maxW="80%"
                >
                  <Heading as="h3" size="md" mb={2}>{slide.title}</Heading>
                  <Text>{slide.description}</Text>
                </Box>
              </Box>
            </SwiperSlide>
          ))}
        </Swiper>
      </Box>

      <Separator orientation="horizontal" my={4} />

      <SectionTitle
      >
        Disfruta la frescura y calidad de los mejores productos de nuestra región.
      </SectionTitle>

      <Grid
        templateColumns={{ base: 'repeat(2, 1fr)', md: 'repeat(5, 1fr)' }}
        gap={6}
        px={4}
        w="90%"
        mx="auto"
      >
        {categorias.map((cat, i) => (
          <ChakraLink
            as={Link}
            href={cat.link}
            key={i}
            _hover={{ textDecor: 'none', transform: 'scale(1.03)', transition: 'transform 0.2s' }}
          >
            <VStack
              borderWidth="1px"
              borderRadius="md"
              p={3}
              gap={3}
              bg="white"
              boxShadow="md"
              h="full"
              w="full"
            >
              <Image
                src={cat.image}
                alt={cat.name}
                w="100%"
                h="150px"
                objectFit="cover"
                borderRadius="md"
              />
              <Text fontWeight="semibold" color="green.800">{cat.name}</Text>
            </VStack>
          </ChakraLink>
        ))}
      </Grid>

      <Separator orientation="horizontal" my={4} />


      {/* Canastas */}
      <SectionTitle>Canastas Disponibles</SectionTitle>
      <Grid
        templateColumns={{ base: 'repeat(2, 1fr)', md: 'repeat(5, 1fr)' }}
        gap={6}
        px={4}
        w="90%"
        mx="auto"
      >
        {canastas.map((c, i) => (
          <ChakraLink
            as={Link}
            href={c.link}
            key={i}
            w="100%"
            _hover={{
              textDecor: 'none',
              transform: 'scale(1.03)',
              transition: 'all 0.3s ease',
            }}
          >
            <VStack
              borderWidth="1px"
              borderRadius="lg"
              bg="white"
              boxShadow="md"
              h="100%"
              w="100%"
            >
              <Box w="100%" overflow="hidden">
                <Image
                  src={c.image}
                  alt={c.name}
                  w="100%"
                  h="100%"
                  objectFit="cover"
                  borderRadius="md"
                  transition="transform 0.3s"
                  _hover={{ transform: 'scale(1.05)' }}
                />
              </Box>
              <Text
                fontWeight="semibold"
                color="green.800"
                textAlign="center"
                fontSize="lg"
              >
                {c.name}
              </Text>
            </VStack>
          </ChakraLink>
        ))}
      </Grid>

      <Separator orientation="horizontal" my={4} />


      {/* Ofertas */}
      <SectionTitle>Ofertas del Día</SectionTitle>
      <Grid
        templateColumns={{ base: 'repeat(2, 1fr)', md: 'repeat(5, 1fr)' }}
        gap={6}
        px={4}
        w="90%"
        mx="auto"
      >
        {ofertas.map((o, i) => (
          <ChakraLink
            as={Link}
            href={o.link}
            key={i}
            _hover={{ textDecor: 'none', transform: 'scale(1.03)', transition: 'transform 0.2s' }}
          >
            <VStack
              borderWidth="1px"
              borderRadius="md"
              p={3}
              gap={3}
              bg="white"
              boxShadow="md"
              position="relative"
            >
              <Box
                position="absolute"
                top={2}
                right={2}
                bg="red.500"
                color="white"
                px={2}
                py={1}
                borderRadius="md"
                fontSize="sm"
                fontWeight="bold"
              >
                OFERTA
              </Box>
              <Image
                src={o.image}
                alt={o.name}
                w="100%"
                h="150px"
                objectFit="cover"
                borderRadius="md"
              />
              <Text fontWeight="semibold" color="green.800">{o.name}</Text>
            </VStack>
          </ChakraLink>
        ))}
      </Grid>
    </Stack>
  );
};

export default HomePage;
