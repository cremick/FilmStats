import React from 'react';
import "./Card.css";

type Props = {}

const Card = (props: Props) => {
  return (
    <div className='card'>
        <img 
            src='https://picsum.photos/300/300'
            alt='film'
        />
        <div className='details'>
            <h2>Hairspray</h2>
            <p>2007</p>
        </div>
        <p className='info'>
            Lorem ipsum dolor sit amet consectetur adipisicing elit. Similique, officia.
        </p>
    </div>
  );
};

export default Card