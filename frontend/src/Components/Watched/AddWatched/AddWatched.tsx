import React, { SyntheticEvent } from 'react'

interface Props {
    onWatchedCreate: (e: SyntheticEvent) => void;
    title: string
}

const AddWatched = ({onWatchedCreate, title}: Props) => {
  return (
    <form onSubmit={onWatchedCreate}>
        <input readOnly={true} hidden={true} value={title} />
        <button type="submit">Add</button>
    </form>
  );
}

export default AddWatched