import React, { useEffect, useState } from 'react'
import { useOutletContext } from 'react-router-dom';
import { FilmCredits } from '../../film';
import { getCredits } from '../../api';
import CreditsList from '../CreditsList/CreditsList';
import Spinner from "../Spinner/Spinner";

type Props = {}

const Crew = (props: Props) => {
  const ticker = useOutletContext<number>();
  const [crew, setCrew] = useState<FilmCredits>();

  useEffect(() => {
    const getCrewInit = async () => {
      const result = await getCredits(ticker!);
      setCrew(result?.data);
    };
    getCrewInit();

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  const tableConfig = crew?.crew?.map((member) => ({
    label: member.name,
    render: member.job,
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
  )
}

export default Crew