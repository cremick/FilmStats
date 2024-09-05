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
        <button className="block w-full py-3 text-white duration-200 border-2 rounded-lg bg-red-500 hover:text-red-500 hover:bg-white border-red-500">
          X
        </button>
      </form>
    </div>
  );
};

export default DeleteWatched;
