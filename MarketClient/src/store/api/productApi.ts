import type { Product } from '@/types/Entities';
import { baseApi } from './baseApi';

export const productApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    GetProducts: builder.query<Product[], void>({
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
  useGetProductsQuery
} = productApi;
