import { Box, Button, Flex, Group, HStack, Text, useBreakpointValue, } from "@chakra-ui/react";
import FilterBar from "./FilterBar";
import Products from "./Products";


const ProductPage = () => {
  const isMobile = useBreakpointValue({ base: true, sm: false });

  return (
    <Flex
      id="productMainContainer"
      direction="column"
      w="100%"
      minH="100%"
      align="center"
      justify="start"
      position={"relative"}
    >
      <HStack
        id="headerContainer"
        direction="row"
        w="full"
        h="20%"
        align="center"
        justify="center"
        mb={2}
        mt={4}
      >
        {!isMobile && (
          <Text
            textStyle="5xl"
            fontWeight="bold"
            color="green.500"
            w="30%"
            pl={5}
            textShadow="
          1px 1px 2px rgba(0, 0, 0, 0.7),
          1px 1px 2px rgba(0, 0, 0, 0.7),
          1px 1px 2px rgba(0, 0, 0, 0.7),
          1px 1px 2px rgba(0, 0, 0, 0.7)"
          >
            Productos
          </Text>
        )}
        <HStack
          id="headerOptions"
          direction="row"
          align="center"
          justify="center"
          gap={4}
          w={"70%"}
          h={"full"}
        >

          <Group
            id="optionsButtons"
            w="full"
            justifyContent="center"
          >
            <Button
              colorPalette={"green"}
              rounded="lg"
            >
              Productos
            </Button>
            <Button
              colorPalette={"yellow"}
              rounded="lg"
            >
              Cajas
            </Button>
            <Button
              colorPalette={"blue"}
              rounded="lg"
            >
              De temporada
            </Button>
            <Button
              colorPalette={"purple"}
              rounded="lg"
            >
              Suscripciones
            </Button>
            <Button
              colorPalette={"cyan"}
              rounded="lg"
            >
              Recetas
            </Button>
          </Group>
        </HStack>
      </HStack>


      <HStack
        id="productsContainer"
        direction="row"
        align="start"
        w="100%"
        h="90%"
        justifyContent="center"
        alignItems="flex-start"
      >
        <FilterBar />
        <Box
          id="productsList"
          w="80%"
          minH="100%"
          bg="gray.100"
          gap={4}
        >
          <Products />
        </Box>
      </HStack>
    </Flex>
  );
};
export default ProductPage;
