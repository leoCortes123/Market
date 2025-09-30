export interface LoginRequest {
  email: string;
  password: string;
}

export interface RegisterRequest {
  username: string;
  email: string;
  password: string;
  fullName?: string;
  phone?: string;
  isFarmerDistributor: boolean;
  profilePicture?: string;
}

export interface AuthResponse {
  user: User;
  accessToken: string;
  refreshToken: string;
}

export interface User {
  id: number;
  username: string;
  email: string;
  fullName?: string;
  phone?: string;
  isFarmerDistributor: boolean;
  profilePicture?: string;
  registeredAt: string;
  isActive: boolean;
}
