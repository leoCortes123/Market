import { useMemo } from 'react';
import {
  useCreateCalendarMutation,
  useDeleteCalendarMutation,
  useGetCalendarByDateQuery,
  useGetCalendarsQuery,
  useUpdateCalendarMutation
} from '../api/calendarApi';
import {
  useCreateEventMutation,
  useDeleteEventMutation,
  useGetEventsByDateQuery,
  useGetUpcomingEventsQuery,
  useUpdateEventMutation,
} from '../api/eventApi';

export const useCalendar = () => {
  // Calendarios
  const { data: calendars = [], isLoading: calendarsLoading } = useGetCalendarsQuery();
  const [createCalendar, { isLoading: creatingCalendar }] = useCreateCalendarMutation();
  const [updateCalendar, { isLoading: updatingCalendar }] = useUpdateCalendarMutation();
  const [deleteCalendar, { isLoading: deletingCalendar }] = useDeleteCalendarMutation();

  // Eventos
  const [createEvent, { isLoading: creatingEvent }] = useCreateEventMutation();
  const [updateEvent, { isLoading: updatingEvent }] = useUpdateEventMutation();
  const [deleteEvent, { isLoading: deletingEvent }] = useDeleteEventMutation();

  const isLoading = useMemo(() =>
    calendarsLoading || creatingCalendar || updatingCalendar || deletingCalendar ||
    creatingEvent || updatingEvent || deletingEvent,
    [
      calendarsLoading, creatingCalendar, updatingCalendar, deletingCalendar,
      creatingEvent, updatingEvent, deletingEvent
    ]
  );

  return {
    // Calendarios
    calendars,
    calendarsLoading,
    createCalendar,
    updateCalendar,
    deleteCalendar,
    creatingCalendar,
    updatingCalendar,
    deletingCalendar,

    // Eventos
    createEvent,
    updateEvent,
    deleteEvent,
    creatingEvent,
    updatingEvent,
    deletingEvent,

    // Estado general
    isLoading,
  };
};

export const useCalendarByDate = (date: string) => {
  const { data: calendar, isLoading: calendarLoading, error: calendarError } =
    useGetCalendarByDateQuery(date, { skip: !date });

  const { data: events = [], isLoading: eventsLoading, error: eventsError } =
    useGetEventsByDateQuery(date, { skip: !date });

  const isLoading = calendarLoading || eventsLoading;
  const error = calendarError || eventsError;

  return {
    calendar,
    events,
    isLoading,
    error,
  };
};

export const useUpcomingEvents = (days: number = 30) => {
  const { data: events = [], isLoading, error } = useGetUpcomingEventsQuery(days);

  return {
    events,
    isLoading,
    error,
  };
};
