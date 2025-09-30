// api/eventApi.ts
import type {
  CreateEventRequest,
  Event,
  UpdateEventRequest
} from '../../types/calendar';
import { baseApi } from './baseApi';

export const eventApi = baseApi.injectEndpoints({
  endpoints: (builder) => ({
    // Obtener todos los eventos
    getEvents: builder.query<Event[], void>({
      query: () => '/events',
      providesTags: ['Event'],
    }),

    // Obtener evento por ID
    getEventById: builder.query<Event, number>({
      query: (id) => `/events/${id}`,
      providesTags: (_result, _error, id) => [{ type: 'Event', id }],
    }),

    // Obtener eventos por calendario
    getEventsByCalendar: builder.query<Event[], number>({
      query: (calendarId) => `/events/calendar/${calendarId}`,
      providesTags: (_result, _error, calendarId) => [
        { type: 'Event', id: `calendar-${calendarId}` }
      ],
    }),

    // Obtener eventos por fecha
    getEventsByDate: builder.query<Event[], string>({
      query: (date) => `/events/date/${date}`,
      providesTags: (_result, _error, date) => [
        { type: 'Event', id: `date-${date}` }
      ],
    }),

    // Obtener próximos eventos
    getUpcomingEvents: builder.query<Event[], number | void>({
      query: (days = 30) => `/events/upcoming?days=${days}`,
      providesTags: ['Event'],
    }),

    // Crear evento
    createEvent: builder.mutation<Event, CreateEventRequest>({
      query: (eventData) => ({
        url: '/events',
        method: 'POST',
        body: eventData,
      }),
      invalidatesTags: ['Event'],
    }),

    // Actualizar evento
    updateEvent: builder.mutation<Event, { id: number; data: UpdateEventRequest; }>({
      query: ({ id, data }) => ({
        url: `/events/${id}`,
        method: 'PUT',
        body: data,
      }),
      invalidatesTags: (_result, _error, { id }) => [
        { type: 'Event', id },
        'Event',
      ],
    }),

    // Eliminar evento
    deleteEvent: builder.mutation<void, number>({
      query: (id) => ({
        url: `/events/${id}`,
        method: 'DELETE',
      }),
      invalidatesTags: ['Event'],
    }),
  }),
});

export const {
  useGetEventsQuery,
  useGetEventByIdQuery,
  useGetEventsByCalendarQuery,
  useGetEventsByDateQuery,
  useGetUpcomingEventsQuery,
  useCreateEventMutation,
  useUpdateEventMutation,
  useDeleteEventMutation,
} = eventApi;
