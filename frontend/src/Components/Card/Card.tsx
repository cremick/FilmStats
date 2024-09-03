import React from 'react';
import "./Card.css";
import { FilmSearch } from '../../film';

interface Props {
  id: number;
  searchResult: FilmSearch;

}

const Card: React.FC<Props> = ({ id, searchResult }: Props) : JSX.Element => {
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
    </div>
  );
};

export default Card