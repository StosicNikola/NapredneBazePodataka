
import { Link } from 'react-router-dom';
import './header.css'

function Header() {
    return (<header className='header'>
        <Link to='/'>
            <h1 className='header-logo'>Mouten running</h1>
        </Link>
        <div className='header-div-buttons'>
        <Link to="/signin">
            <button className='sign-button'>Sing in</button>
        </Link>
        <Link to="/signup">
            <button className='sign-button'>Sing up</button>
        </Link>
        </div>
    </header>)
}

export default Header;