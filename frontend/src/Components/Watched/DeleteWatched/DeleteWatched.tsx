import React, { SyntheticEvent } from "react";

interface Props {
  onWatchedDelete: (e: SyntheticEvent) => void;
  watchedFilm: string;
}

const DeleteWatched = ({ onWatchedDelete, watchedFilm }: Props) => {
  return (
    <div>
      <form onSubmit={onWatchedDelete}>
        <input hidden={true} value={watchedFilm}></input>
        <button>X</button>
      </form>
    </div>
  );
};

export default DeleteWatched;
