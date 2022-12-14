
import { Link } from 'react-router-dom';
import {useParams } from "react-router-dom";
import Header from '../../components/header/header';
import './uspon.css'
function Uspon()
{
    const {handle}=useParams();

    return(<div className='uspon-main-div'>
        <h1>{handle}</h1>
        <div className='uspon'>
            <div className='uspon-info'>
            <h2>Datum: dd.mm.yyyy.</h2>
            <img className='uspon-image' src="../../../public/assets/images.jpg" alt="slika" />
            </div>
            <div className='uspon-about'>
                <h3>About!</h3>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas nunc justo, rhoncus et tortor non, consectetur ornare magna. Integer mattis dui a ante interdum, vitae tincidunt mauris suscipit. Pellentesque congue, elit et pretium volutpat, ante purus efficitur lorem, in lobortis tellus justo sed dolor. Phasellus eget dui a turpis maximus aliquam non quis dui. Donec id justo bibendum tortor molestie rutrum et a neque. Cras lobortis tincidunt lorem vel blandit. Donec ac rutrum nulla, sed bibendum enim. Maecenas sit amet enim magna. Mauris faucibus mollis turpis, sit amet congue erat molestie eu. Quisque pellentesque nunc tellus, a euismod urna tristique vitae. Aliquam nisl mi, ullamcorper non augue a, egestas ornare libero. Integer eget augue quis odio commodo dapibus. Nulla facilisi. Vivamus sit amet vestibulum quam. Integer ut lorem ultricies, egestas turpis sed, pellentesque dui. Aliquam malesuada ac nulla sit amet malesuada.</p></div>
        </div>
       
    </div>)
}

export default Uspon;