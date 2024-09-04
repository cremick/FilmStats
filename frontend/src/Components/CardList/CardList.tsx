import React, { SyntheticEvent } from 'react'
import Card from '../Card/Card';
import { FilmSearch } from '../../film';
import { v4 as uuidv4 } from "uuid";

interface Props {
  searchResults: FilmSearch[];
  onWatchedCreate: (e: SyntheticEvent) => void;
}

const CardList : React.FC<Props> = ({ searchResults, onWatchedCreate }: Props): JSX.Element => {
  return (
    <>
      {searchResults.length > 0 ? (
        searchResults.map((result) => {
          return (
            <Card 
              id={result.id}
              key={uuidv4()}
              searchResult={result}
              onWatchedCreate={onWatchedCreate}
            />
          );
        })
      ) : (
        <h1>No results</h1>
      )}
    </>
  );
}

export default CardList