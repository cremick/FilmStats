import React from "react";
import { Outlet } from "react-router-dom";

type Props = {
  title: string;
  children: React.ReactNode;
};

const FilmDashboard = ({ title, children }: Props) => {
  return (
    <div className="relative md:ml-64 bg-blueGray-100 w-full">
      <div className="relative pt-5 pb-32 bg-lightBlue-500">
        <div className="px-4 md:px-6 mx-auto w-full">
          <div>
            <div className="text-3xl text-white font-semibold py-4">
              {title}
            </div>
            <div className="flex flex-wrap">{children}</div>
            <div className="flex flex-wrap">{<Outlet />}</div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default FilmDashboard;
