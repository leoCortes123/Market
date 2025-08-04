export interface User {
  id: number;
  username: string;
  email: string;
  password: string;
  fullName?: string;
  phone?: string;
  isFarmerDistributor?: boolean;
  profilePicture?: string;
  registeredAt?: Date;
  isActive?: boolean;
}


export interface AuthState {
  refreshToken: string | null;
  accessToken: string | null;
  user: User | null;
}

export interface Product {
  id: number;
  name: string;
  description?: string;
  defaultUnitId: number;
  defaultUnit: string;
  imageUrl?: string;
  averagePrice?: number;
}


export interface Box {
  id: number;
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
}

export interface ProductsBox {
  boxId: number;
  productId: number;
  quantity: number;
}

export interface Categories {
  id: number;
  name: string;
  description?: string;
}

