export interface LoginResponse {
  accessToken: string;
  refreshToken: string;
}

export interface RefreshTokenResponse {
  AccessToken: string;
  RefreshToken: string;
}

export interface LoginRequest {
  email: string;
  password: string;
}

export interface RefreshTokenRequest {
  userId: number;
  refreshToken: string;
}

