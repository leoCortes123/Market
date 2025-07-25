import App from "@/App";
import Home from "@/pages/Home";
import LoginPage from "@/pages/LoginPage";
import Products from "@/pages/Products";
import Register from "@/pages/Register";
import { createBrowserRouter } from "react-router-dom";

const router = createBrowserRouter([
  {
    path: '/',
    Component: App,
    children: [
      { index: true, Component: Home },
      { path: 'home', Component: Home },
      { path: 'login', Component: LoginPage },
      { path: 'register', Component: Register },
      { path: 'products', Component: Products }
    ],
  },
]);

export default router;


