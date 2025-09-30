import { useGetProductsQuery } from "@/store/api/productApi";
import type { Product } from "@/types/Entities";
import { Flex, Text } from "@chakra-ui/react";
import ProductCard from "./ProductCard";

const Products = () => {
  const { data: products, isLoading } = useGetProductsQuery();

  return (
    <Flex
      id="productsMainContainer"
      direction="row"
      wrap="wrap"
      w="100%"
      minH="100%"
      align="stretch"
      justify="start"
      mb={10}
      gap={4}
      p={8}
      bg="green.100"
    >
      {isLoading && <Text>Loading...</Text>}
      {!products?.length && !isLoading && <Text>No hay productos disponibles</Text>}
      {products?.map((product: Product) => (
        <ProductCard key={product.id} {...product} />
      ))}
    </Flex>
  );
};

export default Products;
