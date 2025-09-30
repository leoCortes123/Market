// store/api/baseApi.ts
import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { RootState } from '../store';

const baseUrl = import.meta.env.VITE_MARKETAPI_BASE_URL || 'http://localhost:5001/api';

const baseQuery = fetchBaseQuery({
  baseUrl: baseUrl,
  prepareHeaders: (headers, { getState }) => {
    // Accede al estado de auth del store
    const state = getState() as RootState;
    const token = state.auth?.accessToken; // Usa accessToken para requests
    if (token) {
      headers.set('authorization', `Bearer ${token}`);
    }
    headers.set('Content-Type', 'application/json');
    return headers;
  },
});

export const baseApi = createApi({
  reducerPath: 'api',
  baseQuery,
  tagTypes: ['Auth', 'User', 'Product', 'Combo', 'ComboProduct', 'Category', 'Supplier', 'Calendar', 'Event'],
  endpoints: () => ({}),
});
