import {
  Box,
  Flex,
  GridItem,
  Heading,
  SimpleGrid,
  Text
} from '@chakra-ui/react';
import { skipToken } from '@reduxjs/toolkit/query';
import { useMemo, useState } from 'react';
import Calendar from 'react-calendar';
import 'react-calendar/dist/Calendar.css';
import { FaCarrot } from 'react-icons/fa';
import {
  useGetCalendarByDateQuery,
  useGetCalendarsQuery,
} from '../../store/api/calendarApi';
import {
  useGetEventsByDateQuery,
  useGetEventsQuery
} from '../../store/api/eventApi';


export function CalendarPage() {
  const [selectedDate, setSelectedDate] = useState<Date | null>(null);

  // Obtener todos los calendarios para mostrar los marcadores
  const { data: calendars = [] } = useGetCalendarsQuery();

  // Obtener todos los eventos (para cuando no hay fecha seleccionada)
  const { data: allEvents = [] } = useGetEventsQuery();

  // Fecha seleccionada en formato YYYY-MM-DD
  const formattedDate = selectedDate?.toISOString().split('T')[0];

  // Hook para obtener el calendario específico por fecha
  const { data: calendarByDate } = useGetCalendarByDateQuery(
    formattedDate || skipToken
  );

  // Hook para obtener eventos por fecha específica
  const { data: eventsByDate = [] } = useGetEventsByDateQuery(
    formattedDate || skipToken
  );

  // Eventos a mostrar: filtrados por fecha si hay fecha seleccionada, si no, todos
  const filteredEvents = useMemo(() => {
    if (formattedDate && calendarByDate) {
      return eventsByDate;
    }
    return allEvents;
  }, [formattedDate, calendarByDate, eventsByDate, allEvents]);

  // Función para formatear la fecha en español
  const formatDate = (dateString: string) => {
    const date = new Date(dateString);
    return date.toLocaleDateString('es-ES', {
      weekday: 'long',
      year: 'numeric',
      month: 'long',
      day: 'numeric'
    });
  };

  // Verificar si una fecha tiene eventos
  const hasEvents = (date: Date) => {
    const dateStr = date.toISOString().split('T')[0];
    return calendars.some(calendar => calendar.date === dateStr && calendar.events.length > 0);
  };

  return (
    <Flex
      gap={8}
      direction={['column', 'row']}
      align="start"
      justify="start"
      p={4}
      w="full"
      h="100%"
    >
      {/* Calendario */}
      <Box
        flex="1"
        maxW="300px"
        border="1px solid"
        borderColor="gray.200"
        borderRadius="md"
        p={4}
        bg="white"
        boxShadow="md"
      >
        <Calendar
          onChange={(value) => {
            if (!value || Array.isArray(value)) return;
            setSelectedDate(value);
          }}
          value={selectedDate || new Date()}
          locale="es-ES" // Para que esté en español
          tileContent={({ date, view }) => {
            // Mostrar ícono solo en vista mensual y si la fecha tiene eventos
            return view === 'month' && hasEvents(date) ? (
              <Box mt={1} textAlign="center">
                <FaCarrot color='green' size={15} />
              </Box>
            ) : null;
          }}
          // Estilos personalizados para días con eventos
          tileClassName={({ date, view }) => {
            if (view === 'month' && hasEvents(date)) {
              return 'has-events';
            }
            return null;
          }}
        />

        {/* Leyenda del calendario */}
        <Box mt={4} p={3} bg="gray.50" borderRadius="md">
          <Text fontSize="sm" fontWeight="bold" mb={2}>
            Leyenda:
          </Text>
          <Flex align="center" gap={2} mb={1}>
            <FaCarrot color='green' size={12} />
            <Text fontSize="xs">Días con eventos disponibles</Text>
          </Flex>
        </Box>
      </Box>

      {/* Lista de eventos */}
      <Box flex="3">
        <SimpleGrid columns={{ base: 1, md: 2 }} gap={4}>
          <GridItem colSpan={{ base: 1, md: 2 }}>
            <Heading size="lg" mb={4} color="green.700">
              {formattedDate ? (
                calendarByDate ? (
                  `Eventos para el ${formatDate(formattedDate)}`
                ) : (
                  `No hay eventos programados para el ${formatDate(formattedDate)}`
                )
              ) : (
                "Todos los eventos disponibles"
              )}
            </Heading>

            {!formattedDate && (
              <Text color="gray.600" mb={4}>
                Selecciona una fecha en el calendario para ver los eventos específicos de ese día.
              </Text>
            )}
          </GridItem>

          {filteredEvents.length === 0 ? (
            <GridItem colSpan={{ base: 1, md: 2 }}>
              <Box
                p={6}
                textAlign="center"
                border="2px dashed"
                borderColor="gray.300"
                borderRadius="md"
                bg="gray.50"
              >
                <Text color="gray.500">
                  {formattedDate
                    ? "No hay eventos programados para esta fecha."
                    : "No hay eventos disponibles en este momento."
                  }
                </Text>
              </Box>
            </GridItem>
          ) : (
            filteredEvents.map((event) => (
              <GridItem key={event.id}>
                <Box
                  p={4}
                  border="1px solid"
                  borderColor="green.200"
                  borderRadius="md"
                  boxShadow="sm"
                  bg="white"
                  _hover={{
                    boxShadow: "md",
                    borderColor: "green.300",
                    transform: "translateY(-2px)",
                    transition: "all 0.2s"
                  }}
                >
                  <Text
                    fontWeight="bold"
                    fontSize="lg"
                    color="green.700"
                    mb={2}
                  >
                    {event.place}
                  </Text>

                  <Flex align="center" mb={2}>
                    <Text fontSize="sm" color="gray.600" fontWeight="medium">
                      📍 {event.address}
                    </Text>
                  </Flex>

                  {event.description && (
                    <Text
                      mt={2}
                      mb={3}
                      fontSize="sm"
                      color="gray.700"
                      fontStyle="italic"
                    >
                      {event.description}
                    </Text>
                  )}

                  <Flex justify="space-between" align="center" mt={3}>
                    <Text fontSize="sm" color="green.600" fontWeight="medium">
                      🕒 {event.timeStart} - {event.timeEnd}
                    </Text>
                    {!formattedDate && (
                      <Text fontSize="xs" color="gray.500">
                        {formatDate(event.calendarDate)}
                      </Text>
                    )}
                  </Flex>
                </Box>
              </GridItem>
            ))
          )}
        </SimpleGrid>
      </Box>
    </Flex>
  );
}
