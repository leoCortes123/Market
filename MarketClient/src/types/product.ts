export interface Product {
  id: number;
  name: string;
  description?: string;
  measurementUnitId: number;
  imageUrl?: string;
  isActive: boolean;
  categoryId: number;
  categoryName?: string;
  measurementUnitName?: string;
}

export interface CreateProductRequest {
  name: string;
  description?: string;
  measurementUnitId: number;
  imageUrl?: string;
  categoryId: number;
}

export interface UpdateProductRequest {
  name: string;
  description?: string;
  measurementUnitId: number;
  imageUrl?: string;
  isActive: boolean;
  categoryId: number;
}
