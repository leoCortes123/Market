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
  imageUrl?: string;
  averagePrice?: number;
}


export interface Combo {
  id: number;
  name: string;
  description?: string;
  price: number;
  imageUrl?: string;
}

