import type { Combo } from '../../types/combo';
import type { Product } from '../../types/product';
import { baseApi } from './baseApi';

export const catalogApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    // Obtener productos activos
    getActiveProducts: builder.query<Product[], void>({
      query: () => '/catalog/active-products',
      providesTags: ['Product'],
    }),

    // Obtener combos activos
    getActiveCombos: builder.query<Combo[], void>({
      query: () => '/catalog/active-combos',
      providesTags: ['Combo'],
    }),

    // Obtener productos por categoría (desde catálogo)
    getCatalogProductsByCategory: builder.query<Product[], number>({
      query: (categoryId) => `/catalog/category/${categoryId}/products`,
      providesTags: ['Product'],
    }),
  }),
});

export const {
  useGetActiveProductsQuery,
  useGetActiveCombosQuery,
  useGetCatalogProductsByCategoryQuery,
} = catalogApi;
