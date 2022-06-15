import React, { useState, useEffect } from 'react';
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios';


const HomeRow = ({ Person }) => {
    const { id, firstName, lastName, age, address, email } = Person;
    return (
        <tr>
            <td>{id}</td>
            <td>{firstName}</td>
            <td>{lastName}</td>
            <td>{age}</td>
            <td>{address}</td>
            <td>{email}</td>
        </tr>
    );

}
export default HomeRow;