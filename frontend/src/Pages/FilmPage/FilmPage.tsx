import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { FilmProfile } from "../../film";
import { getFilmProfile } from "../../api";

interface Props {}

const FilmPage = (props: Props) => {
  let { ticker } = useParams();
  const [film, setFilm] = useState<FilmProfile>();

  useEffect(() => {
    const getProfileInit = async () => {
      const result = await getFilmProfile(ticker!);
      setFilm(result?.data);
    };
    getProfileInit();
  }, [ticker]);

  return <>{film ? <div>{film.title}</div> : <div>Film not found!</div>}</>;
};

export default FilmPage;
