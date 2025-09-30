import {
  Box,
  Link as ChakraLink,
  Grid,
  Heading,
  Image,
  Stack,
  Text,
  VStack,
  useBreakpointValue,
  type HeadingProps
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

interface cardInfo {
  name: string;
  image: string;
  link: string;
}

const categorias: cardInfo[] = [
  { name: 'Frutas', image: 'categories/frutas.png', link: '/productos/frutas' },
  { name: 'Verduras', image: 'categories/verduras.png', link: '/productos/verduras' },
  { name: 'Tubérculos', image: 'categories/tuberculos.png', link: '/productos/tuberculos' },
  { name: 'Hierbas', image: 'categories/hierbas.png', link: '/productos/hierbas' },
  { name: 'Procesados', image: 'categories/procesados.png', link: '/productos/procesados' },
];

const canastas: cardInfo[] = [
  { name: 'Canasta básica', image: 'basket/caja1.jpg', link: '/canastas/1' },
  { name: 'Canasta saludable', image: 'basket/caja2.jpg', link: '/canastas/2' },
  { name: 'Canasta familiar', image: 'basket/caja3.jpg', link: '/canastas/3' },
  { name: 'Canasta premium', image: 'basket/caja4.jpg', link: '/canastas/4' },
];

const ofertas: cardInfo[] = [
  { name: 'Bulto de papa', image: 'offers/offer1.jpg', link: '/ofertas/papas' },
  { name: 'Guanabana', image: 'offers/offer2.jpg', link: '/ofertas/lechuga' },
  { name: 'Mango', image: 'offers/offer3.jpg', link: '/ofertas/lechuga' },
  { name: 'Arepas', image: 'offers/offer4.jpg', link: '/ofertas/lechuga' },
  { name: 'Queso', image: 'offers/offer5.jpg', link: '/ofertas/lechuga' }
];



const SectionTitle = ({ children }: { children: React.ReactNode; }) => {
  const fontSize = useBreakpointValue<HeadingProps["size"]>({
    base: "xl",
    md: "2xl",
  });

  return (
    <Heading
      as="h2"
      size={fontSize}
      my={6}
      px={4}
      textAlign="center"
      color="green.700"
    >
      {children}
    </Heading>
  );
};

const ProductCard = ({ item, isOffer = false }: { item: cardInfo, isOffer?: boolean; }) => {
  const cardHeight = useBreakpointValue({ base: 'auto', md: '100%' });
  const imageHeight = useBreakpointValue({ base: '120px', md: '150px' });

  return (
    <ChakraLink
      as={Link}
      href={item.link}
      _hover={{ textDecor: 'none', transform: 'scale(1.03)', transition: 'transform 0.2s' }}
      w="100%"
    >
      <VStack
        borderWidth="1px"
        borderRadius="md"
        p={3}
        gap={3}
        bg="white"
        boxShadow="md"
        h={cardHeight}
        w="100%"
        position="relative"
        overflow="hidden"
      >
        {isOffer && (
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
            zIndex="1"
          >
            OFERTA
          </Box>
        )}
        <Box w="100%" overflow="hidden" borderRadius="md">
          <Image
            src={item.image}
            alt={item.name}
            w="100%"
            h={imageHeight}
            objectFit="cover"
            transition="transform 0.3s"
            _hover={{ transform: 'scale(1.05)' }}
          />
        </Box>
        <Text
          fontWeight="semibold"
          color="green.800"
          textAlign="center"
          fontSize={{ base: 'sm', md: 'md' }}
        >
          {item.name}
        </Text>
      </VStack>
    </ChakraLink>
  );
};

const HomePage = () => {
  const bannerHeight = useBreakpointValue({ base: "50vh", md: "70vh" });
  const gridColumns = useBreakpointValue({ base: 2, md: 5 });
  const slideTextPosition = useBreakpointValue({ base: "center", md: "left" });
  const slideTextWidth = useBreakpointValue({ base: "90%", md: "50%" });

  return (
    <Stack
      id="home-page"
      direction="column"
      w="full"
      minH="90vh"
      bg="gray.50"
      gap={6}
    >
      {/* Swiper Banner */}
      <Box w="full" h={bannerHeight} mb={4}>
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
                  bottom={{ base: "10%", md: "20%" }}
                  left={slideTextPosition}
                  bg="blackAlpha.700"
                  color="white"
                  p={4}
                  borderRadius="md"
                  maxW={slideTextWidth}
                  textAlign={{ base: "center", md: "left" }}
                  mx={{ base: "auto", md: "10%" }}
                >
                  <Heading as="h3" size={{ base: "sm", md: "md" }} mb={2}>
                    {slide.title}
                  </Heading>
                  <Text fontSize={{ base: "xs", md: "sm" }}>
                    {slide.description}
                  </Text>
                </Box>
              </Box>
            </SwiperSlide>
          ))}
        </Swiper>
      </Box>

      <SectionTitle>
        Disfruta la frescura y calidad de los mejores productos de nuestra región.
      </SectionTitle>

      <Grid
        templateColumns={`repeat(${gridColumns}, 1fr)`}
        gap={{ base: 4, md: 6 }}
        px={{ base: 2, md: 4 }}
        w={{ base: "95%", md: "90%" }}
        mx="auto"
      >
        {categorias.map((cat, i) => (
          <ProductCard key={`cat-${i}`} item={cat} />
        ))}
      </Grid>

      {/* Canastas */}
      <SectionTitle>Canastas Disponibles</SectionTitle>
      <Grid
        templateColumns={`repeat(${gridColumns}, 1fr)`}
        gap={{ base: 4, md: 6 }}
        px={{ base: 2, md: 4 }}
        w={{ base: "95%", md: "90%" }}
        mx="auto"
      >
        {canastas.map((c, i) => (
          <ProductCard key={`canasta-${i}`} item={c} />
        ))}
      </Grid>

      {/* Ofertas */}
      <SectionTitle>Ofertas del Día</SectionTitle>
      <Grid
        templateColumns={`repeat(${gridColumns}, 1fr)`}
        gap={{ base: 4, md: 6 }}
        px={{ base: 2, md: 4 }}
        w={{ base: "95%", md: "90%" }}
        mx="auto"
        mb={8}
      >
        {ofertas.map((o, i) => (
          <ProductCard key={`oferta-${i}`} item={o} isOffer={true} />
        ))}
      </Grid>
    </Stack>
  );
};

export default HomePage;
