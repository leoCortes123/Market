import { Box, Flex, Heading, List, Text } from '@chakra-ui/react';
import { useState } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';

export function CalendarPage() {
  const [selectedDate, setSelectedDate] = useState<Date>(new Date());

  // Simulación de datos
  const calendarData = [
    { id: 1, date: '2025-08-01' },
    { id: 2, date: '2025-08-02' },
    { id: 3, date: '2025-08-03' },
  ];

  const eventData = [
    {
      id: 1,
      place: 'UNIDAD NACIONAL DE PROTECCIÓN',
      address: 'Carrera 44 # 20 - 21',
      description: 'PLaza de mercado con productos frescos y orgánicos.',
      timeStart: '07:00',
      timeEnd: '16:00',
      calendarId: 1,
    },
    {
      id: 2,
      place: 'Plazoleta Capital Towers',
      address: 'Carrera 52 # 24-30',
      description: 'Parqueadero disponible en la zona Bulevar de la 26.',
      timeStart: '07:00',
      timeEnd: '16:00',
      calendarId: 1,
    },
    {
      id: 3,
      place: 'Parque de Alcalá',
      address: 'Calle 136 con Cra 19',
      description: '',
      timeStart: '07:00',
      timeEnd: '16:00',
      calendarId: 2,
    },
    // ...otros eventos
  ];

  const formattedDate = selectedDate.toISOString().split('T')[0]; // YYYY-MM-DD

  const calendarEntry = calendarData.find((entry) => entry.date === formattedDate);
  const filteredEvents = calendarEntry
    ? eventData.filter((event) => event.calendarId === calendarEntry.id)
    : [];

  return (
    <Flex gap={8} direction={['column', 'row']} p={4}>
      <Box
        flex="1"
        maxW="300px"
        border="1px solid"
        borderColor="gray.200"
        borderRadius="md"
        p={4}
      >
        <Calendar
          onChange={(value) => {
            if (!value || Array.isArray(value)) return;
            setSelectedDate(value);
          }}
          value={selectedDate}
          tileContent={({ date, view }) => {
            const dateStr = date.toISOString().split('T')[0];
            const isEventDate = calendarData.some((entry) => entry.date === dateStr);
            return view === 'month' && isEventDate ? (
              <div style={{ textAlign: 'center', marginTop: 2 }}>
                <span style={{ color: '#ff0000ff', fontSize: '1.5em' }}>X</span>
              </div>
            ) : null;
          }}
        />
      </Box>

      <Box flex="1">
        <Heading size="md" mb={4}>
          Eventos para {formattedDate}
        </Heading>

        {filteredEvents.length > 0 ? (
          <List.Root gap={4}>
            {filteredEvents.map((event) => (
              <Box
                key={event.id}
                p={4}
                border="1px solid"
                borderColor="gray.200"
                borderRadius="md"
              >
                <Text fontWeight="bold">{event.place}</Text>
                <Text fontSize="sm" color="gray.600">
                  {event.address}
                </Text>
                {event.description && (
                  <Text mt={2}>{event.description}</Text>
                )}
                <Text mt={2} fontSize="sm">
                  Horario: {event.timeStart} - {event.timeEnd}
                </Text>
              </Box>
            ))}
          </List.Root>
        ) : (
          <Text color="gray.500">No hay eventos para esta fecha.</Text>
        )}
      </Box>
    </Flex>
  );
}
