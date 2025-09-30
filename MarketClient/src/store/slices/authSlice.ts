// store/slices/authSlice.ts
import type { AuthState, User } from '@/types/Entities';
import { createSlice, type PayloadAction } from '@reduxjs/toolkit';

const initialState: AuthState = {
  user: null,
  accessToken: null,
  refreshToken: null,
  isAuthenticated: false,
};

const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    setCredentials: (state, action: PayloadAction<{
      user: User;
      accessToken: string;
      refreshToken: string;
    }>) => {
      state.user = action.payload.user;
      state.accessToken = action.payload.accessToken;
      state.refreshToken = action.payload.refreshToken;
      state.isAuthenticated = true;
    },
    logout: (state) => {
      state.user = null;
      state.accessToken = null;
      state.refreshToken = null;
      state.isAuthenticated = false;
    },
    updateTokens: (state, action: PayloadAction<{
      accessToken: string;
      refreshToken?: string;
    }>) => {
      state.accessToken = action.payload.accessToken;
      if (action.payload.refreshToken) {
        state.refreshToken = action.payload.refreshToken;
      }
    },
  },
});

export const { setCredentials, logout, updateTokens } = authSlice.actions;
export default authSlice.reducer;
