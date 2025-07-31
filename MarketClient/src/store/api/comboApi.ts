import type { Combo } from '@/types/Entities';
import { baseApi } from './baseApi';

export const authApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    GetCombos: builder.query<Combo[], void>({
      query: () => ({
        url: '/products',
        method: 'GET',
      }),
      extraOptions: { maxRetries: 0 },
    }),
  }),
  overrideExisting: false,
});

export const {
  useGetCombosQuery
} = authApi;
