import { useGetProductsQuery } from "@/store/api/productApi";
import type { Product } from "@/types/Entities";
import { Grid, Text } from "@chakra-ui/react";
import ProductCard from "./ProductCard";

const Products = () => {
  const { data: products, isLoading } = useGetProductsQuery();

  return (
    <Grid
      id="productsGrid"
      templateColumns="repeat(auto-fill, minmax(300px, 1fr))"
      w="100%"
      gap={5}
      p={5}
      bg="green.100"
    >
      {isLoading && <Text>Loading...</Text>}
      {!products?.length && !isLoading && <Text>No hay productos disponibles</Text>}
      {products?.map((product: Product) => (
        <ProductCard key={product.id} {...product} />
      ))}
    </Grid>
  );
};

export default Products;
