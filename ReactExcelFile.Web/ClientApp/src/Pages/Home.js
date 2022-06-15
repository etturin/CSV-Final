import React, { useEffect, useState } from 'react';
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios';
import HomeRow from '../Components/HomeRow';

const Home = () => {
    const [people, setPeople] = useState([]);
    useEffect(() => {
        const getPeople = async () => {
            const { data } = await axios.get('/api/people/getpeople');
            setPeople(data);
        }

        getPeople();

    }, []);

    const deleteAll = async () => {
        console.log('went in');
        await axios.post('/api/people/delete');
        const { data } = await axios.get('/api/people/getpeople');
        setPeople(data);
    }

    return <
        div id="root" >
        <div>
            <div className="container">
                <div>
                    <button className="btn btn-danger btn-lg btn-block" onClick={deleteAll} >Delete All</button>
                    <table className="table table-hover table-striped table-bordered">
                        <thead>
                            <tr>
                                <th>Id</th>
                                <th>First Name</th>
                                <th>Last Name</th>
                                <th>Age</th>
                                <th>Address</th>
                                <th>Email</th>
                            </tr>
                        </thead>
                        <tbody>
                            {people && people.map((m, i) =>
                                <HomeRow
                                    key={i}
                                    Person={m}
                                />)}
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div >
}
export default Home;