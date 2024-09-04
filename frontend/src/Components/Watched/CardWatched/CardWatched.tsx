import React, { SyntheticEvent } from 'react'
import DeleteWatched from '../DeleteWatched/DeleteWatched';

interface Props {
    watchedFilm: string;
    onWatchedDelete: (e: SyntheticEvent) => void;
}

const CardWatched = ({ watchedFilm, onWatchedDelete }: Props) => {
  return (
    <>
        <h4>{watchedFilm}</h4>
        <DeleteWatched onWatchedDelete={onWatchedDelete} watchedFilm={watchedFilm} />
    </>
  )
}

export default CardWatched