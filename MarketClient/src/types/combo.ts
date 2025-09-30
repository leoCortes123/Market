import type { ComboProductItem } from "./comboProduct";

export interface ComboProductDetail {
  productId: number;
  productName: string;
  unitId: number;
  unitName: string;
  quantity: number;
}

export interface Combo {
  id: number;
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
  isActive: boolean;
  comboProducts: ComboProductDetail[];
}

export interface CreateComboRequest {
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
  comboProducts: ComboProductItem[];
}

export interface UpdateComboRequest {
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
  isActive: boolean;
  comboProducts: ComboProductItem[];
}
