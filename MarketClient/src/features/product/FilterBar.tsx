import { useGetCategoriesQuery } from "@/store/api/productApi";
import { Box, Button, CloseButton, Drawer, Group, IconButton, Input, Portal, Text, useBreakpointValue, VStack } from "@chakra-ui/react";
import { FaSearch } from "react-icons/fa";
import { GiHamburgerMenu } from "react-icons/gi";

export default function FilterBar() {
  const { data: Categories, isLoading } = useGetCategoriesQuery();
  const isMobile = useBreakpointValue({ base: true, md: false });

  return (
    <>
      {!isMobile ? (
        <VStack
          id="leftSidebar"
          w={["full", "20%"]} // Ancho completo en móvil si se muestra
          h="100%"
          justify="start"
          align="start"
          gap={4}
          bg="gray.200"
          p={4} // Padding general
          display={["none", "flex"]} // Oculto en móvil
        >
          <Group
            id="searchGroup"
            w="full"
            flexDirection={["column", "row"]} // Columna en móvil si se muestra
          >
            <Input
              flex="1"
              size="md"
              border="1px solid"
              borderColor="green.500"
              placeholder="Buscar..."
              mb={[2, 0]} // Margen inferior solo en móvil
            />
            <Button
              colorScheme="green"
              variant="outline"
              size="md"
              w={["full", "auto"]} // Ancho completo en móvil
            >
              <FaSearch />
            </Button>
          </Group>

          <Box id="categoriesList" w="full">
            <Text fontSize={["xl", "2xl"]} fontWeight="bold" mb={2}>
              Categorías
            </Text>
            {isLoading && <Text>Cargando categorías...</Text>}
            {!isLoading && Categories?.length === 0 && (
              <Text>No hay categorías disponibles</Text>
            )}
            <VStack align="start" gap={2}>
              {Categories?.map((category) => (
                <Button
                  key={category.id}
                  variant="ghost"
                  w="full"
                  justifyContent="start"
                  fontSize="lg"
                >
                  {category.name}
                </Button>
              ))}
            </VStack>
          </Box>
        </VStack>
      ) : (
        <Drawer.Root placement={"start"}>
          <Drawer.Trigger asChild>
            <IconButton
              position="fixed"
              top={["16%", "20%"]} // Posición ajustada
              left={2}
              zIndex={1100}
              colorScheme="green"
              aria-label="Open menu"
              size="sm"
            >
              Filtros <GiHamburgerMenu />
            </IconButton>
          </Drawer.Trigger>
          <Portal>
            <Drawer.Backdrop />
            <Drawer.Positioner>
              <Drawer.Content maxW="80vw"> // Ancho máximo del drawer
                <Drawer.Header borderBottomWidth="1px">
                  <Text fontSize="xl">Filtros</Text>
                  <Drawer.CloseTrigger asChild>
                    <CloseButton size="sm" position="absolute" right={2} top={2} />
                  </Drawer.CloseTrigger>
                </Drawer.Header>
                <Drawer.Body pt={4}>
                  <VStack gap={4}>
                    <Group w="full">
                      <Input
                        flex="1"
                        size="md"
                        border="1px solid"
                        borderColor="green.500"
                        placeholder="Buscar..."
                      />
                      <Button colorScheme="green" size="md">
                        <FaSearch />
                      </Button>
                    </Group>

                    <Box w="full">
                      <Text fontSize="xl" fontWeight="bold" mb={2}>
                        Categorías
                      </Text>
                      {isLoading && <Text>Cargando categorías...</Text>}
                      {!isLoading && Categories?.length === 0 && (
                        <Text>No hay categorías disponibles</Text>
                      )}
                      <VStack align="start" gap={2}>
                        {Categories?.map((category) => (
                          <Button
                            key={category.id}
                            variant="ghost"
                            w="full"
                            justifyContent="start"
                          >
                            {category.name}
                          </Button>
                        ))}
                      </VStack>
                    </Box>
                  </VStack>
                </Drawer.Body>
              </Drawer.Content>
            </Drawer.Positioner>
          </Portal>
        </Drawer.Root>
      )}
    </>
  );
}

