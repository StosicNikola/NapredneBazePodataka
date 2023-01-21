
import './home.css'
import { Link } from 'react-router-dom';
import { useEffect, useState } from "react";
import axios from 'axios';
import RegionCard from '../../components/region_card/regioncard';
import Header from '../../components/header/header';
import TableLeaderboar from '../../components/rowLeaderboard/TableLeaderboar';



function Home(){

    const[regions,setRegions]= useState([]);
    const[rows,setRows] = useState(cities);

    useEffect(()=>{
        if(regions.length===0)
        axios.get(`https://localhost:5001/Region`)
        .then(res=>setRegions(res.data[0]))
        .catch(e=>alert(e))
        console.log(regions)

    },regions);


    return(<>
    <Header />
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
    <div className='regions'>
        <h1>Regions</h1>
        { regions.map(element => {
            return <RegionCard  regionName={element.name} regionId={element.id}/>  
        })}  
    </div>
    <div>
        <TableLeaderboar  />
    </div>
    

    </>)
}

const cities = [
    {score:"10",mountain:"Trem",name:"Tamara", time: "10.1.2022"},
    {score:"20",mountain:"Rtanj",name:"Nikola", time: "10.1.2021"},
    {score:"30",mountain:"Sokolov kamen",name:"Tamara", time: "10.1.2020"},
    {score:"40",mountain:"Mosor",name:"Nikola", time: "10.1.2022"}
    
]
const Row = (props) => {
    const{score,mountain,name,time} = props;
    return (<tr>
        <td>{score}</td>
        <td>{mountain}</td>
        <td>{name}</td>
        <td>{time}</td>
    </tr>)
}
const Table = (props) => {
    const {data} = props;
    return( <table>
         <tr>
            <th>Score</th>
            <th>Mountain</th>
            <th>Name of runner</th>
            <th>Time</th>
        </tr>
        <tbody>
            {data.map(row => 
                <Row 
                score = {row.score}
                mountain = {row.mountain}
                name ={row.name}
                time = {row.time}
                />
            )}
        </tbody>
    </table>)
}

export default Home;