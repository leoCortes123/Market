import type { ComboProduct, ComboProductItem } from '../../types/comboProduct';
import { baseApi } from './baseApi';

export const comboProductApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    // Obtener todas las relaciones combo-producto
    getAllComboProducts: builder.query<ComboProduct[], void>({
      query: () => '/comboProducts',
      providesTags: ['ComboProduct'],
    }),

    // Obtener relación específica combo-producto
    getComboProduct: builder.query<
      ComboProduct,
      { comboId: number; productId: number; unitId: number; }
    >({
      query: ({ comboId, productId, unitId }) =>
        `/comboProducts/combo/${comboId}/product/${productId}/unit/${unitId}`,
      providesTags: (_result, _error, { comboId, productId, unitId }) => [
        { type: 'ComboProduct', id: `${comboId}-${productId}-${unitId}` },
      ],
    }),

    // Agregar producto a combo
    addProductToCombo: builder.mutation<
      ComboProduct,
      { comboId: number; data: ComboProductItem; }
    >({
      query: ({ comboId, data }) => ({
        url: `/comboProducts/combo/${comboId}`,
        method: 'POST',
        body: data,
      }),
      invalidatesTags: (_result, _error, { comboId }) => [
        { type: 'Combo', id: comboId },
        'ComboProduct',
      ],
    }),

    // Actualizar producto en combo
    updateProductInCombo: builder.mutation<
      void,
      {
        comboId: number;
        productId: number;
        unitId: number;
        data: ComboProductItem;
      }
    >({
      query: ({ comboId, productId, unitId, data }) => ({
        url: `/comboProducts/combo/${comboId}/product/${productId}/unit/${unitId}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_result, _error, { comboId, productId, unitId }) => [
        { type: 'Combo', id: comboId },
        { type: 'ComboProduct', id: `${comboId}-${productId}-${unitId}` },
        'ComboProduct',
      ],
    }),

    // Remover producto de combo
    removeProductFromCombo: builder.mutation<
      void,
      { comboId: number; productId: number; unitId: number; }
    >({
      query: ({ comboId, productId, unitId }) => ({
        url: `/comboProducts/combo/${comboId}/product/${productId}/unit/${unitId}`,
        method: 'DELETE',
      }),
      invalidatesTags: (_result, _error, { comboId }) => [
        { type: 'Combo', id: comboId },
        'ComboProduct',
      ],
    }),
  }),
});

export const {
  useGetAllComboProductsQuery,
  useGetComboProductQuery,
  useAddProductToComboMutation,
  useUpdateProductInComboMutation,
  useRemoveProductFromComboMutation,
} = comboProductApi;
