import React, { useEffect, useState } from "react";
import { useOutletContext } from "react-router-dom";
import { FilmContextType, FilmCredits } from "../../film";
import { getCredits } from "../../api";
import CreditsList from "../CreditsList/CreditsList";
import Spinner from "../Spinner/Spinner";

type Props = {};

const Cast = (props: Props) => {
  const { ticker }= useOutletContext<FilmContextType>();
  const [cast, setCast] = useState<FilmCredits>();

  useEffect(() => {
    const getCastInit = async () => {
      const result = await getCredits(ticker!);
      setCast(result?.data);
    };
    getCastInit();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const tableConfig = cast?.cast?.map((member) => ({
    id: member.credit_id,
    label: member.name,
    render: member.character,
    subTitle: member.popularity
  }));

  return (
    <>
      {tableConfig ? (
        <>
          <CreditsList config={tableConfig} />
        </>
      ) : (
        <Spinner />
      )}
    </>
  );
};

export default Cast;
