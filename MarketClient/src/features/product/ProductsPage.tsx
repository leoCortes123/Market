import { Box, Button, Flex, Group, Heading, HStack, Input, VStack } from "@chakra-ui/react";
import { FaSearch } from "react-icons/fa";
import Products from "./Products";

const ProductPage = () => {
  return (
    <Flex
      id="productMainContainer"
      direction="column"
      w="100%"
      minH="100%"
      align="center"
      justify="start"
    >
      <Heading as="h1" size="xl" mb={2} mt={4} textAlign="center">
        Productos
      </Heading>
      <HStack
        id="headerProducts"
        direction="row"
        align="center"
        gap={4}
        w={"100%"}
        h={"10%"}
        paddingX={5}
        marginBottom={4}
      >
        <Group
          id="searchGroup"
          attached
          w="40%"
          justifySelf="flex-end"
        >
          <Input
            flex="1"
            size="md"
            placeholder="Papa criolla, Frijol, Cebolla cabezona...  " />
          <Button bg="bg.subtle" variant="outline">
            <FaSearch />
          </Button>
        </Group>
        <Group
          id="optionsButtons"
          attached
          w={"60%"}
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
        </Group>
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
          justify="center"
          align="center"
          gap={4}
          bg="gray.200"
        >
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
