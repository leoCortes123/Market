// api/comboApi.ts
// api/comboApi.ts
import type { Combo, CreateComboRequest, UpdateComboRequest } from '../../types/combo';
import type { ComboProduct } from '../../types/comboProduct';
import { baseApi } from './baseApi';

export const comboApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    // Obtener todos los combos
    getCombos: builder.query<Combo[], void>({
      query: () => '/combos',
      providesTags: ['Combo'],
    }),

    // Obtener combo por ID
    getComboById: builder.query<Combo, number>({
      query: (id) => `/combos/${id}`,
      providesTags: (_result, _error, id) => [{ type: 'Combo', id }],
    }),

    // Crear combo
    createCombo: builder.mutation<Combo, CreateComboRequest>({
      query: (comboData) => ({
        url: '/combos',
        method: 'POST',
        body: comboData,
      }),
      invalidatesTags: ['Combo'],
    }),

    // Actualizar combo
    updateCombo: builder.mutation<Combo, { id: number; data: UpdateComboRequest; }>({
      query: ({ id, data }) => ({
        url: `/combos/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_result, _error, { id }) => [
        { type: 'Combo', id },
        'Combo',
      ],
    }),

    // Eliminar combo
    deleteCombo: builder.mutation<void, number>({
      query: (id) => ({
        url: `/combos/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Combo'],
    }),

    // Activar/desactivar combo
    toggleComboStatus: builder.mutation<void, number>({
      query: (id) => ({
        url: `/combos/${id}/toggle-status`,
        method: 'PATCH',
      }),
      invalidatesTags: (_result, _error, id) => [
        { type: 'Combo', id },
        'Combo',
      ],
    }),

    // Obtener productos de un combo
    getComboProducts: builder.query<ComboProduct[], number>({
      query: (comboId) => `/combos/${comboId}/products`,
      providesTags: (_result, _error, comboId) => [{ type: 'Combo', id: comboId }],
    }),
  }),
});

export const {
  useGetCombosQuery,
  useGetComboByIdQuery,
  useCreateComboMutation,
  useUpdateComboMutation,
  useDeleteComboMutation,
  useToggleComboStatusMutation,
  useGetComboProductsQuery,
} = comboApi;
