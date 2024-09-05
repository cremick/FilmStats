import React, { SyntheticEvent } from "react";
import CardWatched from "../CardWatched/CardWatched";

interface Props {
  watchedFilms: string[];
  onWatchedDelete: (e: SyntheticEvent) => void;
}

const WatchedList = ({ watchedFilms, onWatchedDelete }: Props) => {
  return (
    <section id="watched">
      <h2 className="mb-3 mt-3 text-3xl font-semibold text-center md:text-4xl">
        My Films
      </h2>
      <div className="relative flex flex-col items-center max-w-5xl mx-auto space-y-10 px-10 mb-5 md:px-6 md:space-y-0 md:space-x-7 md:flex-row">
        <>
          {watchedFilms.length > 0 ? (
            watchedFilms.map((watchedFilm) => {
              return (
                <CardWatched
                  watchedFilm={watchedFilm}
                  onWatchedDelete={onWatchedDelete}
                />
              );
            })
          ) : (
            <h3 className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
              Your watched list is empty.
            </h3>
          )}
        </>
      </div>
    </section>
  );
};

export default WatchedList;
