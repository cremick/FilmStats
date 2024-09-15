import React, { useEffect, useState } from "react";
import { useOutletContext } from "react-router";
import { FilmCredits } from "../../film";
import { getCast } from "../../api";

type Props = {};

const Cast = (props: Props) => {
  const ticker = useOutletContext<number>();
  const [cast, setCast] = useState<FilmCredits>();

  useEffect(() => {
    const getCastInit = async () => {
      const result = await getCast(ticker!);
      setCast(result?.data);
    };
    getCastInit();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const tableConfig = cast?.cast?.map((member) => ({
    label: member.name,
    render: member.character,
  }));

  return (
    <>
      {tableConfig && tableConfig.length > 0 ? (
        <div className="cast-table-container">
          <table>
            <thead>
              <tr>
                <th>Cast Member</th>
                <th>Role</th>
              </tr>
            </thead>
            <tbody>
              {tableConfig.map((row, index) => (
                <tr key={index}>
                  <th>{row.label}</th> {/* Render cast member's name */}
                  <td>{row.render}</td> {/* Render character name */}
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      ) : (
        <div className="bg-default">Cast not found!</div>
      )}
    </>
  );
};

export default Cast;
