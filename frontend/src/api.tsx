import axios from "axios";
import { FilmCredits, FilmProfile, FilmSearch } from "./film";

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
      console.log("error message from API: ", error.message);
    } else {
      console.log("unexpected error: ", error);
    }
    return "An unexpected error has occured.";
  }
};

export const getFilmProfile = async (query: string) => {
  const id = Number(query);
  try {
    const data = await axios.get<FilmProfile>(
      `https://api.themoviedb.org/3/movie/${id}?api_key=${process.env.REACT_APP_API_KEY}`
    )
    return data;
  } catch (error: any) {
    console.log("error message from API: ", error.message);
  }
}

export const getCredits = async (query: number) => {
  try {
    const data = await axios.get<FilmCredits>(
      `https://api.themoviedb.org/3/movie/${query}/credits?api_key=${process.env.REACT_APP_API_KEY}`
    )
    return data;
  } catch (error: any) {
    console.log("error message from API: ", error.message);
  }
}