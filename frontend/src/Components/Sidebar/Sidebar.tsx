import React from "react";
import { Link } from "react-router-dom";
import { FaHome, FaStar, FaTheaterMasks } from "react-icons/fa";
import { GiFilmProjector } from "react-icons/gi";
import "./Sidebar.css";

type Props = {};

const Sidebar = (props: Props) => {
  return (
    <nav className="block py-4 px-6 top-0 bottom-0 w-64 bg-default shadow-xl left-0 absolute flex-row flex-nowrap md:z-10 z-9999 transition-all duration-300 ease-in-out transform md:translate-x-0 -translate-x-full bg-default">
      <button className="md:hidden flex items-center justify-center cursor-pointer text-blueGray-700 w-6 h-10 border-l-0 border-r border-t border-b border-solid border-blueGray-100 text-xl leading-none bg-white rounded-r border border-solid border-transparent absolute top-1/2 -right-24-px focus:outline-none z-9998">
        <i className="fas fa-ellipsis-v"></i>
      </button>

      <div className="flex-col min-h-full px-0 flex flex-wrap items-center justify-between w-full mx-auto overflow-y-auto overflow-x-hidden">
        <div className="flex bg-default flex-col items-stretch opacity-100 relative mt-4 overflow-y-auto overflow-x-hidden h-auto z-40 items-center flex-1 rounded w-full">
          <div className="md:flex-col md:min-w-full flex flex-col list-none">
            <Link
              to="film-profile"
              className="flex md:min-w-full text-blueGray-500 text-medium uppercase font-bold block pt-1 pb-4 no-underline"
            >
              <FaHome className="icon-spacing" />
              <h6 className="m1-3">Film Profile</h6>
            </Link>
            <Link
              to="cast"
              className="flex md:min-w-full text-blueGray-500 text-medium uppercase font-bold block pt-1 pb-4 no-underline"
            >
              <FaStar className="icon-spacing" />
              <h6 className="m1-3">Cast</h6>
            </Link>
            <Link
              to="crew"
              className="flex md:min-w-full text-blueGray-500 text-medium uppercase font-bold block pt-1 pb-4 no-underline"
            >
              <GiFilmProjector className="icon-spacing" />
              <h6 className="m1-3">Crew</h6>
            </Link>
            <Link
              to="genres"
              className="flex md:min-w-full text-blueGray-500 text-medium uppercase font-bold block pt-1 pb-4 no-underline"
            >
              <FaTheaterMasks className="icon-spacing" />
              <h6 className="m1-3">Genres</h6>
            </Link>
          </div>
        </div>
      </div>
    </nav>
  );
};

export default Sidebar;
