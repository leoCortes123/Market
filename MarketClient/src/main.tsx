import { ChakraProvider } from '@chakra-ui/react';
import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router';
import system from './chakra/theme.ts';
import router from './routes/routes';
import { store } from './store/store.ts';


ReactDOM.createRoot(document.getElementById('root')!).render(
  <Provider store={store}>
    <ChakraProvider value={system}>
      <RouterProvider router={router} />
    </ChakraProvider>
  </Provider>
);
