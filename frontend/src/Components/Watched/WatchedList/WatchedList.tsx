import React, { SyntheticEvent } from "react";
import CardWatched from "../CardWatched/CardWatched";

interface Props {
  watchedFilms: string[];
  onWatchedDelete: (e: SyntheticEvent) => void;
}

const WatchedList = ({ watchedFilms, onWatchedDelete }: Props) => {
  return (
    <>
      <h3>My Films</h3>
      <ul>
        {watchedFilms &&
          watchedFilms.map((watchedFilm) => {
            return (
              <CardWatched
                watchedFilm={watchedFilm}
                onWatchedDelete={onWatchedDelete}
              />
            );
          })}
      </ul>
    </>
  );
};

export default WatchedList;
