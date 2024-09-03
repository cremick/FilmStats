import React from 'react';
import "./Card.css";

interface Props {
  filmTitle: string;
  releaseYear: number;
}

const Card = ({ filmTitle, releaseYear }: Props) => {
  return (
    <div className='card'>
        <img 
            src='https://picsum.photos/300/300'
            alt='film'
        />
        <div className='details'>
            <h2>{filmTitle}</h2>
            <p>{releaseYear}</p>
        </div>
        <p className='info'>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Similique, officia.
        </p>
    </div>
  );
};

export default Card