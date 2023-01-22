import './home.css'
import { Link } from 'react-router-dom';
import { useEffect, useState } from "react";
import axios from 'axios';
import RegionCard from '../../components/region_card/regioncard';
import Header from '../../components/header/header';
import TableLeaderboar from '../../components/rowLeaderboard/TableLeaderboar';



function Home() {

    const [regions, setRegions] = useState([]);
    const [rows, setRows] = useState(cities);
    const [hikes, setHikes] = useState([]);
    const [races, setRaces] = useState([]);

    useEffect(() => {
        if (regions.length === 0)
            axios.get(`https://localhost:5001/Region`)
                .then(res => setRegions(res.data[0]))
                .catch(e => console.log(e))

        if (hikes.length === 0) {
            axios.get(`https://localhost:5001/hike`)
                .then(res => {
                    setHikes(res.data[0])
                })
                .catch(e => console.log(e))
        }
        if (races.length === 0) {
            axios.get(`https://localhost:5001/race`)
                .then(res => {
                    setRaces(res.data[0])
                })
                .catch(e => console.log(e))
        }

    }, regions);


    return (<>
        <Header />
        <div className='home-usponi'>
            <div className='home-part'>
                <h1 className='home-uspon'>Hikes</h1>

                {
                    hikes.map(element => {
                        return <Link to={`hike/${element.name}`} >
                            <div className='home-card'>
                                <h3>{element.name}</h3>&emsp;<h3>Date: {element.date}</h3>
                            </div>
                        </Link>
                    })}
            </div>
            <div className='home-part'>
                <h1>Races</h1>
                {
                    races.map(element => {
                        return <Link to={`race/${element.name}`} >
                            <div className='home-card'>
                                <h3>{element.name}</h3>&emsp;<h3>Date: {element.date}</h3>
                            </div>
                        </Link>
                    })
                }
            </div>
            <div className='home-part'>
                <h1>Regions</h1>
                {regions.map(element => {
                    return <RegionCard regionName={element.name} regionId={element.id} />
                })}
               
            </div>
            <div className='home-part'>
                <h1>Leaderboard</h1>
                <TableLeaderboar />
                </div>
        </div>


    </>)
}

const cities = [
    { score: "10", mountain: "Trem", name: "Tamara", time: "10.1.2022" },
    { score: "20", mountain: "Rtanj", name: "Nikola", time: "10.1.2021" },
    { score: "30", mountain: "Sokolov kamen", name: "Tamara", time: "10.1.2020" },
    { score: "40", mountain: "Mosor", name: "Nikola", time: "10.1.2022" }

]
export const Row = (props) => {
    const { score, mountain, name, time } = props;
    return (<tr>
        <td>{score}</td>
        <td>{mountain}</td>
        <td>{name}</td>
        <td>{time}</td>
    </tr>)
}

const Table = (props) => {
    const { data } = props;
    return (<table>
        <tr>
            <th>Score</th>
            <th>Mountain</th>
            <th>Name of runner</th>
            <th>Time</th>
        </tr>
        <tbody>
            {data.map(row =>
                <Row
                    score={row.score}
                    mountain={row.mountain}
                    name={row.name}
                    time={row.time}
                />
            )}
        </tbody>
    </table>)
}

export default Home;