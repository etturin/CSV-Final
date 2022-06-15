import React, { useEffect, useState, useRef } from 'react';
import { Link, useHistory } from 'react-router-dom';
import axios from 'axios';

const Generate = () => {
    const history = useHistory();
    const [count, setCount] = useState(0);
    const generate = async () => {
        /*window.location.href = await ('/api/people/generate', count);*/
        window.location.href = `/api/people/generate?count=${count}`;
    }
    return (
        <div className="container">
            <div className="d-flex vh-100">
                <div className="d-flex w-100 justify-content-center align-self-center">
                    <div className="row">
                        <input type="text" className="form-control-lg" placeholder="Amount" onChange={e => setCount(e.target.value)} />
                            </div>
                        <div className="row">
                        <div className="col-md-4">
                            <button className="btn btn-primary btn-lg" onClick={generate}>Generate</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
    )
}
export default Generate;