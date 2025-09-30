export interface ComboProductItem {
  productId: number;
  unitId: number;
  quantity: number;
}

export interface ComboProduct {
  comboId: number;
  productId: number;
  unitId: number;
  quantity: number;
  comboName: string;
  productName: string;
  unitName: string;
}
