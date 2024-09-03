import React, { ChangeEvent, MouseEvent } from 'react'

interface Props {
    handleClick: (e: MouseEvent<HTMLButtonElement, globalThis.MouseEvent>) => void;
    search: string | undefined;
    handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
};

const Search : React.FC<Props> = ({
    handleClick,
    search,
    handleChange,
}: Props): JSX.Element => {
    return (
        <div>
            <input value={search} onChange={(e) => handleChange(e)}></input>
            <button onClick={(e) => handleClick(e)} />
        </div>
    )
}

export default Search