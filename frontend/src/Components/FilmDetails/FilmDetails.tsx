import React from "react";
import { useOutletContext } from "react-router-dom";
import { FilmContextType } from "../../film";

type Props = {};

const FilmDetails = (props: Props) => {
  const { film } = useOutletContext<FilmContextType>();

  return <div>
    {film.title}
    {film.revenue}
  </div>;
};

export default FilmDetails
