import { createSystem, defaultConfig, defineConfig, defineTextStyles } from "@chakra-ui/react";

const textStyles = defineTextStyles({
  body: {
    description: "Body text style",
    value: {
      fontFamily: "Inter, sans-serif",
    }
  }
});

const config = defineConfig({
  theme: {
    textStyles
  }
});

const system = createSystem(defaultConfig, config);
export default system;
