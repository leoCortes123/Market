import type { AuthState } from '@/types/Entities';
import { configureStore } from '@reduxjs/toolkit';
import { authApi } from './api/authApi';

export const store = configureStore({
  reducer: {
    auth: authApi.reducer,
    [authApi.reducerPath]: authApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(authApi.middleware),
});

export type RootState = ReturnType<typeof store.getState>
  & {
    auth: AuthState;
  };

export type AppDispatch = typeof store.dispatch;
