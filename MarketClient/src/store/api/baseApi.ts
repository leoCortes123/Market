import { createApi, fetchBaseQuery } from '@reduxjs/toolkit/query/react';
import type { RootState } from '../store';

// const baseUrl = import.meta.env.VITE_MARKETAPI_BASE_URL || 'http://localhost:3001/api';
const baseUrl = 'http://localhost:3001/';

const baseQuery = fetchBaseQuery({
  baseUrl: baseUrl,

  prepareHeaders: (headers, { getState }) => {
    const state = getState() as RootState;
    const token = state.auth?.refreshToken;
    if (token) {
      headers.set('authorization', `Bearer ${token}`);
    }
    return headers;
  },
});

export const baseApi = createApi({
  reducerPath: 'api',
  baseQuery,
  tagTypes: ['Auth', 'User'],
  endpoints: () => ({}),
});
