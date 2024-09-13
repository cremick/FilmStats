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
  }, []);

  return (
    <>
      {film ? (
        <div className="w-full relative flex ct-docs-disable-sidebar-content overflow-x-hidden">
          <Sidebar />
          <FilmDashboard>
            <Tile title="Film Name" subTitle={film.title}></Tile>
          </FilmDashboard>
        </div>
      ) : (
        <div>Film not found!</div>
      )}
    </>
  );
};

export default FilmPage;
