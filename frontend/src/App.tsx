import { ChangeEvent, useState, MouseEvent } from 'react';
import './App.css';
import CardList from './Components/CardList/CardList';
import Search from './Components/Search/Search';

function App() {
  const [search, setSearch] = useState<string>("");

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
      setSearch(e.target.value);
      console.log(e);
  };

  const handleClick = (e: MouseEvent<HTMLButtonElement, globalThis.MouseEvent>) => {
      console.log(e);
  };
  
  return (
    <div className="App">
      <Search handleClick={handleClick} search={search} handleChange={handleChange}/>
      <CardList />
    </div>
  );
}

export default App;
