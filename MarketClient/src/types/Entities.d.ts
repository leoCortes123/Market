import type { User } from './auth';

export interface AuthState {
  user: User | null;
  accessToken: string | null;
  refreshToken: string | null;
  isAuthenticated: boolean;
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

export interface Calendar {
  id: number;
  date: string;
}

export interface Event {
  id: number;
  place: string;
  address: string;
  description?: string;
  timeStart: string;
  timeEnd: string;
  calendarId: number;
}
