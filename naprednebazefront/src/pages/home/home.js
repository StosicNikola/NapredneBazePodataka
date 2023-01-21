import './home.css'
import { Link } from 'react-router-dom';
import { useEffect, useState } from "react";
import axios from 'axios';
import RegionCard from '../../components/region_card/regioncard';
import Header from '../../components/header/header';



function Home(){

    const[regions,setRegions]= useState([]);
    const[hikes,setHikes]= useState([]);

    useEffect(()=>{
        if(regions.length===0)
        axios.get(`https://localhost:5001/Region`)
        .then(res=>setRegions(res.data[0]))
        .catch(e=>console.log(e))

        if(hikes.length === 0)
        {
            axios.get(`https://localhost:5001/hike`)
            .then(res=>
                {
                    setHikes(res.data[0])
                })
                .catch(e=>console.log(e))
        }

    },regions);


    return(<>
    <Header />
    <div className='home-usponi'>
    <h1 className='home-uspon'>Hikes</h1>
    
        { 
            hikes.map(element=>{
            return <Link to={`uspon/${element.name}`} >
            <div className='home-card'>
                <h3>{element.name}</h3>&emsp;<h3>Date: {element.date}</h3>
            </div>
            </Link>
        })}
        {/* <Link to='uspon/Rtanj'>
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
    </Link>*/}
    </div> 

    
    <div className='regions'>
        <h1>Regions</h1>
        { regions.map(element => {
            return <RegionCard  regionName={element.name} regionId={element.id}/>  
        })}  
    </div>
    </>)
}

export default Home;