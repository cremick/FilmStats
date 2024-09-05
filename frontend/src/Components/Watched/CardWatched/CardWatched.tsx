import React, { SyntheticEvent } from "react";
import DeleteWatched from "../DeleteWatched/DeleteWatched";

interface Props {
  watchedFilm: string;
  onWatchedDelete: (e: SyntheticEvent) => void;
}

const CardWatched = ({ watchedFilm, onWatchedDelete }: Props) => {
  return (
    <div className="flex flex-col w-full p-8 space-y-4 text-center rounded-lg shadow-lg md:w-1/3">
      <p className="pt-6 text-xl font-bold">{watchedFilm}</p>
      <DeleteWatched
        watchedFilm={watchedFilm}
        onWatchedDelete={onWatchedDelete}
      />
    </div>
  );
};

export default CardWatched;
