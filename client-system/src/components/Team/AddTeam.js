import React, { useState } from 'react';

const AddTeam = () => {
    const [teamName, setTeamName] = useState('')
    const [image, setImage] = useState(null)
    const [imgUrl, setImgUrl] = useState('')

    const handlerImputChange = (e) => {
        const {name, value} = e.target;
        setTeamName(before => ({
            ...before,
            [name] : value
        }))
    }
    return (
        <div className='row mt-5'>
            <div className='col'>
                <form onSubmit={}></form>
            </div>
        </div>
    );
};

export default AddTeam;