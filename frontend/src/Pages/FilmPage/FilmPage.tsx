import React, { useEffect, useState } from "react";
import { useParams } from "react-router-dom";
import { FilmProfile } from "../../film";
import { getFilmProfile } from "../../api";
import Sidebar from "../../Components/Sidebar/Sidebar";
import FilmDashboard from "../../Components/FilmDashboard/FilmDashboard";
import Tile from "../../Components/Tile/Tile";

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
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <>
      {film ? (
        <div className="min-h-screen w-full relative flex ct-docs-disable-sidebar-content bg-default text-white overflow-x-hidden">
          <Sidebar />
          <FilmDashboard film={film} ticker={ticker!} >
            <Tile title="Release Date" subTitle={film.release_date}></Tile>
            <Tile
              title="Runtime"
              subTitle={String(film.runtime) + " minutes"}
            ></Tile>
            <Tile title="Average Rating" subTitle={String(film.vote_average) + " / 10"}></Tile>
            <Tile title="Popularity Score" subTitle={String(film.popularity)}></Tile>
          </FilmDashboard>
        </div>
      ) : (
        <div className="bg-default">Film not found!</div>
      )}
    </>
  );
};

export default FilmPage;
