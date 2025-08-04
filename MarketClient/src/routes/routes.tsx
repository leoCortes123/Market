import App from "@/App";
import LoginPage from "@/features/auth/LoginPage";
import { CalendarPage } from "@/features/calendar/CalendarPage";
import HomePage from "@/features/home/HomePage";
import ProductPage from "@/features/product/ProductsPage";
import { createBrowserRouter } from "react-router-dom";

const router = createBrowserRouter([
  {
    path: '/',
    Component: App,
    children: [
      { index: true, Component: HomePage },
      { path: 'home', Component: HomePage },
      { path: 'login', Component: LoginPage },
      { path: 'products', Component: ProductPage },
      { path: 'calendar', Component: CalendarPage }

    ],
  },
]);

export default router;


