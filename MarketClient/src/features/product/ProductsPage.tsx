import { useGetCategoriesQuery } from "@/store/api/productApi";
import { Box, Button, Flex, Group, HStack, Input, Text, VStack } from "@chakra-ui/react";
import { FaSearch } from "react-icons/fa";
import Products from "./Products";


const ProductPage = () => {
  const { data: Categories, isLoading } = useGetCategoriesQuery();

  return (
    <Flex
      id="productMainContainer"
      direction="column"
      w="100%"
      minH="100%"
      align="center"
      justify="start"
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

        <VStack
          id="leftSidebar"
          w="20%"
          h="100%"
          justify="start"
          align="start"
          gap={4}
          bg="gray.200"
        >
          <Group
            id="searchGroup"
            attached
            w="full"
            justifySelf="flex-end"
          >
            <Input
              flex="1"
              size="md"
              border={"1px solid"}
              borderColor="green.500"
              placeholder="Buscar..." />
            <Button
              bg="bg.subtle"
              variant="outline"
              border="1px solid"
              borderColor="green.500">
              <FaSearch />
            </Button>
          </Group>
          <Box
            id="categoriesList"
            w="full"
            p={4}
          >

            <Text fontSize="2xl" fontWeight="bold" mb={2}>Categorías</Text>
            {isLoading && <Text>Cargando categorías...</Text>}
            {!isLoading && Categories?.length === 0 && <Text>No hay categorías disponibles</Text>}
            {Categories?.map((category) => (
              <Text key={category.id} fontSize="lg" color="gray.700">
                {category.name}
              </Text>
            ))}

          </Box>

        </VStack>
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
