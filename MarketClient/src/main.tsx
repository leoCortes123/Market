import { ChakraProvider } from '@chakra-ui/react';
import '@fontsource/outfit/200.css';
import '@fontsource/outfit/800.css';
import ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import { RouterProvider } from 'react-router';
import router from './routes/routes';
import { store } from './store/store.ts';
import { system } from './themes.ts';

ReactDOM.createRoot(document.getElementById('root')!).render(
  <Provider store={store}>
    <ChakraProvider value={system}>
      <RouterProvider router={router} />
    </ChakraProvider>
  </Provider>
);

