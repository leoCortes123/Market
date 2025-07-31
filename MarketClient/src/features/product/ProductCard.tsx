import type { Product } from "@/types/Entities";
import { Box, Button, Card, Image, Stat, Text } from "@chakra-ui/react";

export default function ProductCard(product: Product) {


  return (
    <Card.Root
      className="group"
      size="sm"
      overflow="hidden"
      maxH="300px"
      key={product.id}
      position="relative"
      cursor={"pointer"}
      color="white"
      bg="gray.800"
      transition="transform 0.3s ease-in-out, scale 0.3s ease-in-out"
      _hover={{
        transform: "scale(1.05)",
        transition: "transform 0.3s ease-in-out , scale 0.3s ease-in-out",
      }}
    >
      <Image
        src={`/productsImage/${product.imageUrl}`}
        alt={product.name}
        objectFit="cover"
        w="full"
        h="full"
      />

      <Box
        position="absolute"
        bottom="0"
        left="0"
        w="full"
        h="30%"
        pl={5}
        transition="bottom 0.3s ease-in-out"
        _groupHover={{
          bottom: "30%",
          transition: "bottom 0.3s ease-in-out",
        }}
      >
        <Text
          fontSize="lg"
          fontWeight="bold"
          pl={2}
          pt={2}
          textAlign="initial"
          textShadow="1px 1px 2px rgba(0, 0, 0, 0.7), 1px 1px 2px rgba(0, 0, 0, 0.7), 1px 1px 2px rgba(0, 0, 0, 0.7), 1px 1px 2px rgba(0, 0, 0, 0.7)"
        >
          {product.name}
        </Text>

        <Text
          fontSize="sm"
          p={2}
          textAlign="initial"
          textShadow="1px 1px 2px rgba(0, 0, 0, 0.7), 1px 1px 2px rgba(0, 0, 0, 0.7)"
        >
          {product.description}
        </Text>
      </Box>
      <Box
        position="absolute"
        w="100%"
        h="30%"
        bottom="0"
        right="-100%"
        bg="green.500"
        transition="right 0.3s ease-in-out"
        _groupHover={{
          right: "0",
          transition: "right 0.3s ease-in-out"
        }}

      />

      <Box
        position="absolute"
        w="100%"
        h="30%"
        bottom="0"
        left="-100%"
        direction="column"
        px={4}
        py={1}
        boxShadow="lg"
        bg="rgba(0, 0, 0, 0.5)"
        transition="left 0.3s ease-in-out"
        _groupHover={{

          left: "0",
          transition: "left 0.3s ease-in-out"
        }}
      >
        <Card.Body gap="2">
          <Stat.Root>
            <Stat.Label color="white" fontWeight="bold" >Precio promedio:</Stat.Label>
            <Stat.ValueText>
              {product.averagePrice ?? 0} $
            </Stat.ValueText>
          </Stat.Root>
        </Card.Body>

        <Card.Footer gap="2">
          <Button variant="solid" colorScheme="blue">
            Buy now
          </Button>
        </Card.Footer>
      </Box>

    </Card.Root>
  );
}
