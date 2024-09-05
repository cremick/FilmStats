import { ChangeEvent, useState, FormEvent } from "react";
import "./App.css";
import CardList from "./Components/CardList/CardList";
import Search from "./Components/Search/Search";
import { FilmSearch } from "./film";
import { searchFilms } from "./api";
import WatchedList from "./Components/Watched/WatchedList/WatchedList";
import Navbar from "./Components/Navbar/Navbar";
import Hero from "./Components/Hero/Hero";

function App() {
  const [search, setSearch] = useState<string>("");
  const [watchedFilms, setWatchedFilms] = useState<string[]>([]);
  const [searchResult, setSearchResult] = useState<FilmSearch[]>([]);
  const [serverError, setServerError] = useState<string>("");

  const handleSearchChange = (e: ChangeEvent<HTMLInputElement>) => {
    setSearch(e.target.value);
    console.log(e);
  };

  const onWatchedCreate = (e: any) => {
    e.preventDefault();
    const exists = watchedFilms.find((value) => value === e.target[0].value);
    if (exists) return;
    const updatedWatched = [...watchedFilms, e.target[0].value];
    setWatchedFilms(updatedWatched);
  };

  const onWatchedDelete = (e: any) => {
    e.preventDefault();
    const removed = watchedFilms.filter((value) => {
      return value !== e.target[0].value;
    });
    setWatchedFilms(removed);
  };

  const handleSearchSubmit = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    const result = await searchFilms(search);

    if (typeof result === "string") {
      setServerError(result);
    } else if (Array.isArray(result.data.results)) {
      setSearchResult(result.data.results);
    }

    console.log(searchResult);
  };

  return (
    <div className="App">
      <Navbar />
      <Search
        handleSearchSubmit={handleSearchSubmit}
        search={search}
        handleSearchChange={handleSearchChange}
      />
      <WatchedList
        watchedFilms={watchedFilms}
        onWatchedDelete={onWatchedDelete}
      />
      <CardList
        searchResults={searchResult}
        onWatchedCreate={onWatchedCreate}
      />
      {serverError && <h1>{serverError}</h1>}
    </div>
  );
}

export default App;
