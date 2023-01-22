
import { useEffect, useState } from 'react';
import { Link, Navigate } from 'react-router-dom';
import './header.css'
import { useNavigate } from "react-router-dom";

function Header() {
    const [loggedIn, setLoggedIn] = useState(false)
    const navigate = useNavigate()

    const loggeOut =( )=>
    {
        setLoggedIn(false)
        localStorage.clear()
        navigate("/")
        
    }
    useEffect(() => {
        if (localStorage.getItem("id") !== undefined)
            setLoggedIn(true)
        else
            setLoggedIn(false)
    },loggedIn)
    return (<header className='header'>
        <Link to='/'>
            <h1 className='header-logo'>Mountain running</h1>
        </Link>
        {!loggedIn && <div className='header-div-buttons'>
            <Link to="/signin">
                <button className='sign-button'>Sing in</button>
            </Link>
            <Link to="/signup">
                <button className='sign-button'>Sing up</button>
            </Link>
        </div>}
        {
            loggedIn&&<div>
                <Link to="/account">
                <button className='sign-button'>Account</button>
                </Link>
                <button className='sign-button' onClick={loggeOut}>Sign Out</button>
            </div>
        }
    </header>)
}

export default Header;