import type { AuthState } from '@/types/Entities';
import { configureStore } from '@reduxjs/toolkit';
import { baseApi } from './api/baseApi';
import authReducer from './slices/authSlice'; // Si tienes un slice para auth

export const store = configureStore({
  reducer: {
    // Slice tradicional para el estado de autenticación (si lo necesitas)
    auth: authReducer,

    // API slices - solo necesitas registrar baseApi una vez
    [baseApi.reducerPath]: baseApi.reducer,
  },
  middleware: (getDefaultMiddleware) =>
    getDefaultMiddleware().concat(baseApi.middleware),
});

export type RootState = ReturnType<typeof store.getState> & {
  auth: AuthState;
};

export type AppDispatch = typeof store.dispatch;
