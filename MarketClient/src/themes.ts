// theme.ts
import {
  createSystem,
  defaultConfig,
  defineConfig,
} from '@chakra-ui/react';


const theme = defineConfig({
  theme: {
    tokens: {
      fonts: {
        heading: { value: "outfit, sans-serif" },
        body: { value: "outfit, sans-serif" },
      },
    },
  },
  globalCss: {
    "html": {
      margin: 0,
      padding: 0,
      height: '100%',

    },
    "body": {
      position: 'relative',
      margin: 0,
      padding: 0,
      height: '100%',
      overflowX: 'hidden',
      fontFamily: 'Outfit, sans-serif',
    },
    "#root": {
      position: 'absolute',
      top: 0,
      left: 0,
      right: 0,
      bottom: 0,
      margin: 0,
      padding: 0,
      width: '100%',
      height: '100vh',
      display: 'flex',
      flexDirection: 'column',
      alignItems: 'center',
      justifyContent: 'end',

    },
  },


});

export const system = createSystem(defaultConfig, theme);
