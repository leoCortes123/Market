import type { LoginRequest, LoginResponse } from '@/types/authEntities';
import type { User } from '@/types/Entities';
import { baseApi } from './baseApi';

export const authApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    login: builder.mutation<LoginResponse, LoginRequest>({
      query: (credentials) => ({
        url: '/auth/login',
        method: 'POST',
        body: credentials,
      }),
      extraOptions: { maxRetries: 0 },
    }),
    register: builder.mutation<User, User>({
      query: (credentials) => ({
        url: '/auth/register',
        method: 'POST',
        body: credentials,
      }),
      extraOptions: { maxRetries: 0 },
    }),


  }),
  overrideExisting: false, // para evitar colisiones si reinyectas
});

export const {
  useLoginMutation,
  useRegisterMutation
} = authApi;
