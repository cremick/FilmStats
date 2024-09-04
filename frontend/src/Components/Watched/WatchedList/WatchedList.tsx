import React from 'react'
import CardWatched from '../CardWatched/CardWatched';

interface Props {
    watchedFilms: string[];
}

const WatchedList = ({watchedFilms}: Props) => {
  return (
    <>
        <h3>My Films</h3>
        <ul>
            {watchedFilms && 
                watchedFilms.map((watchedFilm) => {
                    return <CardWatched watchedFilm={watchedFilm} />;
                })
            }
        </ul>
    </>
  )
}

export default WatchedList