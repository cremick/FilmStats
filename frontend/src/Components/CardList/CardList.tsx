import React from 'react'
import Card from '../Card/Card';

interface Props {}

const CardList = (props: Props) => {
  return (
    <div>
        <Card filmTitle='Hairspray' releaseYear={2007}/>
        <Card filmTitle='Challengers' releaseYear={2024}/>
        <Card filmTitle='Almost Famous' releaseYear={2000}/>
    </div>
  );
}

export default CardList