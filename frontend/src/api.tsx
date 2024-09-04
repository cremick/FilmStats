import axios from "axios";
import { FilmSearch } from "./film";

interface SearchResponse {
  results: FilmSearch[];
}

export const searchFilms = async (query: string) => {
  try {
    const data = await axios.get<SearchResponse>(
      `https://api.themoviedb.org/3/search/movie?api_key=${process.env.REACT_APP_API_KEY}&query=${query}`
    );
    return data;
  } catch (error) {
    if (axios.isAxiosError(error)) {
      console.log("error message: ", error.message);
    } else {
      console.log("unexpected error: ", error);
    }
    return "An unexpected error has occured.";
  }
};
