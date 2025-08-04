import type { Box, Categories, Product, ProductsBox } from '@/types/Entities';
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
    GetBoxes: builder.query<Box[], void>({
      query: () => ({
        url: '/box',
        method: 'GET',
      }),
      extraOptions: { maxRetries: 0 },
    }),
    GetProductsBoxes: builder.query<ProductsBox[], void>({
      query: () => ({
        url: '/boxProducts',
        method: 'GET',
      }),
      extraOptions: { maxRetries: 0 },
    }),
    GetCategories: builder.query<Categories[], void>({
      query: () => ({
        url: '/categories',
        method: 'GET',
      }),
      extraOptions: { maxRetries: 0 },
    }),

  }),
  overrideExisting: false,
});

export const {
  useGetProductsQuery,
  useGetBoxesQuery,
  useGetProductsBoxesQuery,
  useGetCategoriesQuery
} = productApi;
