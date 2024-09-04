import React from 'react'

interface Props {
    watchedFilm: string;
}

const CardWatched = ({ watchedFilm }: Props) => {
  return (
    <>
        <h4>{watchedFilm}</h4>
        <button>X</button>
    </>
  )
}

export default CardWatched