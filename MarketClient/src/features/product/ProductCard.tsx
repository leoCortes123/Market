import type { Product } from "@/types/Entities";
import { Box, Card, IconButton, Image, Stat, Text } from "@chakra-ui/react";
import { useState } from "react";
import { FaCartPlus, FaEye } from "react-icons/fa";
import { MdBookmarkAdd } from "react-icons/md";
import { TbBasketPlus } from "react-icons/tb";
import OptionDialog from "../components/OptionDialog";

export default function ProductCard(product: Product) {
  const [isCartDialogOpen, setIsCartDialogOpen] = useState(false);
  const [isBasketDialogOpen, setIsBasketDialogOpen] = useState(false);

  const [isFavoriteDialogOpen, setIsFavoriteDialogOpen] = useState(false);
  const [quantity, setQuantity] = useState(1);

  const handleCartConfirm = () => {
    console.log("Agregar al carrito");
    setIsCartDialogOpen(false);
  };

  const handleCartCancel = () => {
    console.log("Cancelar agregar al carrito");
    setIsCartDialogOpen(false);
  };

  const handleBasketConfirm = () => {
    console.log("Agregar al carrito");
    setIsBasketDialogOpen(false);
  };

  const handleBasketCancel = () => {
    console.log("Cancelar agregar al carrito");
    setIsBasketDialogOpen(false);
  };

  const handleFavoriteConfirm = () => {
    console.log("Agregar a favoritos");
    setIsFavoriteDialogOpen(false);
  };

  const handleFavoriteCancel = () => {
    console.log("Cancelar agregar a favoritos");
    setIsFavoriteDialogOpen(false);
  };

  return (
    <>
      <Card.Root
        className="group"
        size="sm"
        overflow="hidden"
        maxH="300px"
        key={product.id}
        id={`product-${product.id}`}
        position="relative"
        color="white"
        bg="gray.800"
        transition="transform 0.3s ease-in-out, scale 0.3s ease-in-out"
        _hover={{
          transform: "scale(1.05)",
        }}
        _active={{
          transform: "scale(1.05)",
        }}
      >
        <Image
          src={`productsImage/${product.imageUrl}`}
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
            bottom: "50%",
          }}
        >
          <Text
            fontSize="xx-large"
            fontWeight="bold"
            textAlign="initial"
            textShadow="1px 1px 2px rgba(0, 0, 0, 0.7)"
          >
            {product.name}
          </Text>

          <Stat.Root>
            <Stat.ValueText textShadow="1px 1px 2px rgba(0, 0, 0, 0.7)">
              $ {product.averagePrice ?? 0} X {product.defaultUnit}
            </Stat.ValueText>
          </Stat.Root>
        </Box>



        <Box
          position="absolute"
          w="100%"
          h="50%"
          bottom="0"
          right="-100%"
          bg="green.500"
          transition="right 0.3s ease-in-out"
          _groupHover={{
            right: "0",
          }}
        />

        <Box
          position="absolute"
          w="100%"
          h="50%"
          bottom="0"
          left="-100%"
          display={"flex"}
          direction="column"
          alignItems="center"
          justifyContent="start"
          px={3}
          py={1}
          boxShadow="lg"
          bg="rgba(0, 0, 0, 0.5)"
          transition="left 0.3s ease-in-out"
          _groupHover={{
            left: "0",
          }}
        >
          <Text
            fontSize="sm"
            textAlign="initial"
            color="white"
            w={"full"}
            h={"75%"}
            textShadow="1px 1px 2px rgba(0, 0, 0, 0.7)"
          >
            {product.description}
          </Text>


        </Box>
        <Box
          position="absolute"
          top="0"
          right="0"
          display="flex"
          direction="row"
          alignItems="center"
          justifyContent="flex-end"
          w="full"
          h="25%"
          gap={2}
          px={2}
          pb={2}
        >
          <IconButton
            onClick={() => setIsCartDialogOpen(true)}
            aria-label="Add to cart"
            rounded="full"
            size="xs"
            bg="green.500"
            _hover={{ bg: "green.900", scale: 1.3 }}
          >
            <FaCartPlus />
          </IconButton>

          <IconButton
            onClick={() => setIsBasketDialogOpen(true)}
            aria-label="Add to basket"
            rounded="full"
            size="xs"
            bg="green.500"
            _hover={{ bg: "green.900", scale: 1.3 }}
          >
            <TbBasketPlus />
          </IconButton>


          <IconButton
            onClick={() => setIsFavoriteDialogOpen(true)}
            aria-label="Add to favorites"
            rounded="full"
            size="xs"
            bg="green.500"
            _hover={{ bg: "green.900", scale: 1.3 }}
          >
            <MdBookmarkAdd />
          </IconButton>

          <IconButton
            aria-label="look details"
            rounded="full"
            size="xs"
            bg="green.500"
            _hover={{ bg: "green.900", scale: 1.3 }}
          >
            <FaEye />
          </IconButton>
        </Box>
      </Card.Root>

      <OptionDialog
        isOpen={isCartDialogOpen}
        onCancel={handleCartCancel}
        onConfirm={handleCartConfirm}
        title="Agregar al carrito"
        product={product}
        showQuantityInput
        quantity={quantity}
        setQuantity={setQuantity}
      >
        <Text>
          ¿Estás seguro de agregar <strong>{product.name}</strong> al carrito?
        </Text>
      </OptionDialog>

      <OptionDialog
        isOpen={isBasketDialogOpen}
        onCancel={handleBasketCancel}
        onConfirm={handleBasketConfirm}
        title="Agregar al mi canasta"
        product={product}
        showQuantityInput
        quantity={quantity}
        setQuantity={setQuantity}
      >
        <Text>
          ¿Estás seguro de agregar <strong>{product.name}</strong> al una canasta?
        </Text>
      </OptionDialog>

      <OptionDialog
        isOpen={isFavoriteDialogOpen}
        onCancel={handleFavoriteCancel}
        onConfirm={handleFavoriteConfirm}
        title="Agregar a favoritos"
        product={product}
      >
        <Text>
          ¿Estás seguro de agregar <strong>{product.name}</strong> a la lista de favoritos?
        </Text>
      </OptionDialog>
    </>
  );
}
