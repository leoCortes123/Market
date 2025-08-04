import type { Product } from "@/types/Entities";
import {
  Box,
  Button,
  CloseButton,
  Dialog,
  Image,
  Portal,
  Text,
  type ButtonProps,
} from "@chakra-ui/react";
import type { ReactNode } from "react";

interface OptionDialogProps {
  isOpen: boolean;
  onCancel: () => void;
  onConfirm: () => void;
  title?: string;
  children?: ReactNode;
  confirmLabel?: string;
  cancelLabel?: string;
  confirmButtonProps?: ButtonProps;
  cancelButtonProps?: ButtonProps;
  product?: Product;
  showQuantityInput?: boolean;
  quantity?: number;
  setQuantity?: (value: number) => void;
}


export default function OptionDialog({
  isOpen,
  onCancel,
  onConfirm,
  title = "Confirm",
  children,
  confirmLabel = "Aceptar",
  cancelLabel = "Cancelar",
  confirmButtonProps,
  cancelButtonProps,
  product,
  showQuantityInput = false,
  quantity,
  setQuantity,
}: OptionDialogProps) {
  return (
    <Dialog.Root open={isOpen}>
      <Portal>
        <Dialog.Backdrop />
        <Dialog.Positioner>
          <Dialog.Content maxW="md">
            <Dialog.Header>
              <Dialog.Title>{title}</Dialog.Title>
            </Dialog.Header>

            <Dialog.Body display="flex" flexDirection="column" alignItems="center" gap={4}>
              {product && (
                <Image
                  src={`/productsImage/${product.imageUrl}`}
                  alt={product.name}
                  objectFit="cover"
                  borderRadius="md"
                  maxH="200px"
                />
              )}

              {children}

              {showQuantityInput && typeof quantity === 'number' && setQuantity && (
                <Box display="flex" alignItems="center" gap={2}>
                  <Text>Cantidad:</Text>
                  <input
                    type="number"
                    value={quantity}
                    min={1}
                    style={{ padding: "4px 8px", borderRadius: "4px", border: "1px solid #ccc" }}
                    onChange={(e) => setQuantity(parseInt(e.target.value))}
                  />
                </Box>
              )}
            </Dialog.Body>

            <Dialog.Footer>
              <Button variant="outline" onClick={onCancel} {...cancelButtonProps}>
                {cancelLabel}
              </Button>
              <Button colorScheme="blue" onClick={onConfirm} {...confirmButtonProps}>
                {confirmLabel}
              </Button>
            </Dialog.Footer>

            <Dialog.CloseTrigger asChild>
              <CloseButton size="sm" />
            </Dialog.CloseTrigger>
          </Dialog.Content>
        </Dialog.Positioner>
      </Portal>
    </Dialog.Root>
  );
}
