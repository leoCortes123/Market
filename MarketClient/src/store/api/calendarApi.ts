// api/calendarApi.ts
import type {
  Calendar,
  CreateCalendarRequest,
  DateRangeParams,
  UpdateCalendarRequest
} from '../../types/calendar';
import { baseApi } from './baseApi';

export const calendarApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    // Obtener todos los calendarios
    getCalendars: builder.query<Calendar[], void>({
      query: () => '/calendars',
      providesTags: ['Calendar'],
    }),

    // Obtener calendario por ID
    getCalendarById: builder.query<Calendar, number>({
      query: (id) => `/calendars/${id}`,
      providesTags: (_result, _error, id) => [{ type: 'Calendar', id }],
    }),

    // Obtener calendario por fecha
    getCalendarByDate: builder.query<Calendar, string>({
      query: (date) => `/calendars/date/${date}`,
      providesTags: (_result, _error, date) => [{ type: 'Calendar', id: date }],
    }),

    // Obtener calendarios por rango de fechas
    getCalendarsByDateRange: builder.query<Calendar[], DateRangeParams>({
      query: ({ startDate, endDate }) =>
        `/calendars/range?startDate=${startDate}&endDate=${endDate}`,
      providesTags: ['Calendar'],
    }),

    // Crear calendario
    createCalendar: builder.mutation<Calendar, CreateCalendarRequest>({
      query: (calendarData) => ({
        url: '/calendars',
        method: 'POST',
        body: calendarData,
      }),
      invalidatesTags: ['Calendar'],
    }),

    // Actualizar calendario
    updateCalendar: builder.mutation<Calendar, { id: number; data: UpdateCalendarRequest; }>({
      query: ({ id, data }) => ({
        url: `/calendars/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_result, _error, { id }) => [
        { type: 'Calendar', id },
        'Calendar',
      ],
    }),

    // Eliminar calendario
    deleteCalendar: builder.mutation<void, number>({
      query: (id) => ({
        url: `/calendars/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Calendar'],
    }),
  }),
});

export const {
  useGetCalendarsQuery,
  useGetCalendarByIdQuery,
  useGetCalendarByDateQuery,
  useGetCalendarsByDateRangeQuery,
  useCreateCalendarMutation,
  useUpdateCalendarMutation,
  useDeleteCalendarMutation,
} = calendarApi;
