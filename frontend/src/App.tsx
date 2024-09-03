import { ChangeEvent, useState, MouseEvent } from 'react';
import './App.css';
import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';
import { FilmSearch } from './film';
import { searchFilms } from './api';

function App() {
  const [search, setSearch] = useState<string>("");
  const [searchResult, setSearchResult] = useState<FilmSearch[]>([]);
  const [serverError, setServerError] = useState<string>("");

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
      setSearch(e.target.value);
      console.log(e);
  };

  const handleClick = async (e: MouseEvent<HTMLButtonElement, globalThis.MouseEvent>) => {
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
      <Search handleClick={handleClick} search={search} handleChange={handleChange}/>
      {serverError && <h1>{serverError}</h1>}
      <CardList />
    </div>
  );
}

export default App;
