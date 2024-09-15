import { createBrowserRouter } from "react-router-dom";
import App from "../App";
import HomePage from "../Pages/HomePage/HomePage";
import SearchPage from "../Pages/SearchPage/SearchPage";
import FilmPage from "../Pages/FilmPage/FilmPage";
import FilmProfile from "../Components/FilmProfile/FilmProfile";
import Cast from "../Components/Cast/Cast";
import Crew from "../Components/Crew/Crew";
import Genres from "../Components/Genres/Genres";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <App />,
    children: [
      { path: "", element: <HomePage /> },
      { path: "search", element: <SearchPage /> },
      {
        path: "film/:ticker",
        element: <FilmPage />,
        children: [
          { path: "film-profile", element: <FilmProfile /> },
          { path: "cast", element: <Cast /> },
          { path: "crew", element: <Crew /> },
          { path: "genres", element: <Genres /> },
        ],
      },
    ],
  },
]);
