import React, { SyntheticEvent } from "react";
import Card from "../Card/Card";
import { FilmSearch } from "../../film";
import { v4 as uuidv4 } from "uuid";

interface Props {
  searchResults: FilmSearch[];
  onWatchedCreate: (e: SyntheticEvent) => void;
}

const CardList: React.FC<Props> = ({
  searchResults,
  onWatchedCreate,
}: Props): JSX.Element => {
  return (
    <div>
      {searchResults.length > 0 ? (
        searchResults.map((result) => {
          return (
            <Card
              id={result.id}
              key={uuidv4()}
              searchResult={result}
              onWatchedCreate={onWatchedCreate}
            />
          );
        })
      ) : (
        <p className="mb-3 mt-3 text-xl font-semibold text-center md:text-xl">
          No results!
        </p>
      )}
    </div>
  );
};

export default CardList;
