import React, { SyntheticEvent } from "react";
import "./Card.css";
import { FilmSearch } from "../../film";
import AddWatched from "../Watched/AddWatched/AddWatched";

interface Props {
  id: number;
  searchResult: FilmSearch;
  onWatchedCreate: (e: SyntheticEvent) => void;
}

const Card: React.FC<Props> = ({
  id,
  searchResult,
  onWatchedCreate,
}: Props): JSX.Element => {
  return (
    <div
      className="flex flex-col items-center justify-between w-full p-6 bg-slate-100 rounded-lg md:flex-row"
      key={id}
    >
      <h2 className="font-bold text-center text-black md:text-left">
        {searchResult.title} ({searchResult.release_date})
      </h2>
      <p className="text-black">{searchResult.popularity}</p>
      <p className="font-bold text-black"> </p>
      <AddWatched
        onWatchedCreate={onWatchedCreate}
        title={searchResult.title}
      />
    </div>
  );
};

export default Card;
