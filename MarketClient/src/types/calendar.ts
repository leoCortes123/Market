export interface Calendar {
  id: number;
  date: string; // Formato: "2025-08-08"
  events: Event[];
}

export interface CreateCalendarRequest {
  date: string;
}

export interface UpdateCalendarRequest {
  date: string;
}

export interface Event {
  id: number;
  place: string;
  address: string;
  description?: string;
  timeStart: string; // Formato: "07:00"
  timeEnd: string;   // Formato: "16:00"
  calendarId: number;
  calendarDate: string;
}

export interface CreateEventRequest {
  place: string;
  address: string;
  description?: string;
  timeStart: string;
  timeEnd: string;
  calendarId: number;
}

export interface UpdateEventRequest {
  place: string;
  address: string;
  description?: string;
  timeStart: string;
  timeEnd: string;
  calendarId: number;
}

export interface DateRangeParams {
  startDate: string;
  endDate: string;
}
