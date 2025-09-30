import type { CreateProductRequest, Product, UpdateProductRequest } from '../../types/product';
import { baseApi } from './baseApi';

export const productApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    // Obtener todos los productos
    getProducts: builder.query<Product[], void>({
      query: () => '/products',
      providesTags: ['Product'],
    }),

    // Obtener producto por ID
    getProductById: builder.query<Product, number>({
      query: (id) => `/products/${id}`,
      providesTags: (_result, _error, id) => [{ type: 'Product', id }],
    }),

    // Crear producto
    createProduct: builder.mutation<Product, CreateProductRequest>({
      query: (productData) => ({
        url: '/products',
        method: 'POST',
        body: productData,
      }),
      invalidatesTags: ['Product'],
    }),

    // Actualizar producto
    updateProduct: builder.mutation<Product, { id: number; data: UpdateProductRequest; }>({
      query: ({ id, data }) => ({
        url: `/products/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_result, _error, { id }) => [
        { type: 'Product', id },
        'Product',
      ],
    }),

    // Eliminar producto
    deleteProduct: builder.mutation<void, number>({
      query: (id) => ({
        url: `/products/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Product'],
    }),

    // Activar/desactivar producto
    toggleProductStatus: builder.mutation<void, number>({
      query: (id) => ({
        url: `/products/${id}/toggle-status`,
        method: 'PATCH',
      }),
      invalidatesTags: (_result, _error, id) => [
        { type: 'Product', id },
        'Product',
      ],
    }),

    // Obtener productos por categoría
    getProductsByCategory: builder.query<Product[], number>({
      query: (categoryId) => `/products/category/${categoryId}`,
      providesTags: ['Product'],
    }),
  }),
});

export const {
  useGetProductsQuery,
  useGetProductByIdQuery,
  useCreateProductMutation,
  useUpdateProductMutation,
  useDeleteProductMutation,
  useToggleProductStatusMutation,
  useGetProductsByCategoryQuery,
} = productApi;
