import React, { SyntheticEvent } from 'react';
import "./Card.css";
import { FilmSearch } from '../../film';
import AddWatched from '../Watched/AddWatched/AddWatched';

interface Props {
  id: number;
  searchResult: FilmSearch;
  onWatchedCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({ id, searchResult, onWatchedCreate }: Props) : JSX.Element => {
  return (
    <div className='card'>
        <img 
            src='https://picsum.photos/300/300'
            alt='film'
        />
        <div className='details'>
            <h2>{searchResult.title}</h2>
            <p>{searchResult.release_date}</p>
        </div>
        <p className='info'>
            {searchResult.overview}
        </p>
        <AddWatched onWatchedCreate={onWatchedCreate} title={searchResult.title}/>
    </div>
  );
};

export default Card