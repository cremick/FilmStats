import React from 'react'
import Card from '../Card/Card';
import { FilmSearch } from '../../film';
import { v4 as uuidv4 } from "uuid";

interface Props {
  searchResults: FilmSearch[];
}

const CardList : React.FC<Props> = ({ searchResults }: Props): JSX.Element => {
  return <>
    {searchResults.length > 0 ? (
      searchResults.map((result) => {
        return (
          <Card id={result.id} key={uuidv4()} searchResult={result} />
        );
      })
    ) : (
      <h1>No results</h1>
    )}
  </>;
}

export default CardList