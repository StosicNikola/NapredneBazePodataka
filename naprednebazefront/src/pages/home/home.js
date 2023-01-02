
import Header from '../../components/header/header';
import './home.css'
import { Link } from 'react-router-dom';



function Home()
{

    return(<>
    <div className='home-usponi'>
    <h1 className='home-uspon'>Usponi</h1>
        <Link to='uspon/Rtanj'>
            <div className='home-card'>
                <h3>RTANJ</h3>&emsp;<h3>Datum:13.12.2022.</h3>
            </div>
        </Link>
        <Link to='uspon/Trem'>
            <div className='home-card'>
                <h3>TREM</h3>&emsp;<h3>Datum:13.12.2022.</h3>
            </div>
        </Link>
        <Link to='uspon/Midzor'>
            <div className='home-card'>
                <h3>MIDZOR</h3>&emsp;<h3>Datum:13.12.2022.</h3>
            </div>
        </Link>
    </div>
    </>)
}

export default Home;