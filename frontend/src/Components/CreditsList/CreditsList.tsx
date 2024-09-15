import React from "react";

type Props = {
  config: any;
};

const CreditsList = ({ config }: Props) => {
  const renderedCells = config.map((row: any) => {
    return (
      <li className="py-4 sm:py-4">
        <div className="flex items-center space-x-4">
          <div className="flex-1 min-w-0">
            <p className="text-sm font-medium text-white truncate">
              {row.label}
            </p>
            <p className="text-sm text-gray-500 italic truncate">
              {row.subTitle && "popularity score: " + row.subTitle}
            </p>
          </div>
          <div className="inline-flex items-center text-base font-semibold text-white">
            {row.render}
          </div>
        </div>
      </li>
    );
  });
  return (
    <div className="bg-lightBlack shadow rounded-lg ml-4 mr-4 mt-4 mb-4 p-4 sm:p-6 w-full">
      <ul className="divide-y divide-gray-200">{renderedCells}</ul>
    </div>
  );
};

export default CreditsList;
