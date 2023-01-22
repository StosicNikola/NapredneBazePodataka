import './home.css'
import { Link } from 'react-router-dom';
import { useEffect, useState } from "react";
import axios from 'axios';
import RegionCard from '../../components/region_card/regioncard';
import Header from '../../components/header/header';
import Leaderboard from '../../components/LeaderBoardCard/LeaderBoardCard';



function Home() {

    const [regions, setRegions] = useState([]);

    const [hikes, setHikes] = useState([]);
    const [races, setRaces] = useState([]);
    const [role, setRole] = useState("");
    const [name, setName] = useState("");
    const [about, setAbout] = useState();
    const [date, setDate] = useState();
    const [difficulty, setDifficility] = useState();
    const [personelId, setPersonelId] = useState();
    const [addHike, setAddHike] = useState(false);
    const [addRace, setAddRace] = useState(false);
    const [mountainTopId, setMountainTopId] = useState()
    const [mainLeaderboard, setMainLeaderboard] = useState([])
    const tmp = 10;

    const addHikeEvent = () => {
        setAddHike(!addHike)
        setAddRace(false)
    }
    const submitHike = () => {
        axios.post(`https://localhost:5001/hike/${name}/${date}/${difficulty}/${about}/${mountainTopId}/${personelId}`)
            .then(res => alert("Kreirano"))
            .catch(e => alert(e))

        addHikeEvent()
    }
    const addRaceEvent = () => {
        setAddRace(!addRace)
        setAddHike(false)
    }
    const submitRace = () => {
        axios.post(`https://localhost:5001/race/${name}/${date}/${difficulty}/${about}/${mountainTopId}/${personelId}`)
            .then(res => alert("Kreirano"))
            .catch(e => alert(e))
        addRaceEvent()
    }

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
        if (mainLeaderboard.length === 0) {
            axios.get(`https://localhost:5001/RedisLeaderboardControllers/GetMainleaderboardFrom0toNitems/${tmp}`)
                .then(res => {
                    console.log(res.data);
                    setMainLeaderboard(res.data)

                })
                .catch(e => alert(e))
        }
        setRole(localStorage.getItem("role"))

    }, regions);


    return (<>
        <Header />
        <div className='home-usponi'>
            <div className='home-part'>
                <h1 className='home-uspon'>Hikes</h1>
                {role === "Admin" ?
                    addHike ? <div>
                        <div>
                            <p>Name:</p>
                            <input onChange={e => setName(e.target.value)} />
                        </div>
                        <div>
                            <p>Date:</p>
                            <input type="date" onChange={e => setDate(e.target.value)} />
                        </div>
                        <div>
                            <p>Dificility:</p>
                            <input type="number" onChange={e => setDifficility(e.target.value)} />
                        </div>
                        <div>
                            <p>Id hiking guide:</p>
                            <input type="number" onChange={e => setPersonelId(e.target.value)} />
                        </div>
                        <div>
                            <p>Mountain top id:</p>
                            <input type="number" onChange={e => setMountainTopId(e.target.value)} />
                        </div>
                        <button className="signin-button" onClick={submitHike}>Submit</button>
                    </div> : <div><button className="signin-button" onClick={addHikeEvent}> Add Hike</button></div>
                    : <div></div>
                }

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
                {role === "Admin" ?
                    addRace ? <div>
                        <div>
                            <p>Name:</p>
                            <input onChange={e => setName(e.target.value)} />
                        </div>
                        <div>
                            <p>Date:</p>
                            <input type="date" onChange={e => setDate(e.target.value)} />
                        </div>
                        <div>
                            <p>Dificility:</p>
                            <input type="number" onChange={e => setDifficility(e.target.value)} />
                        </div>
                        <div>
                            <p>Id referee:</p>
                            <input type="number" onChange={e => setPersonelId(e.target.value)} />
                        </div>
                        <div>
                            <p>Mountain top id:</p>
                            <input type="number" onChange={e => setMountainTopId(e.target.value)} />
                        </div>
                        <button className="signin-button" onClick={submitRace}>Submit</button>
                    </div> : <div><button className="signin-button" onClick={addRaceEvent}> Add race</button></div>
                    : <div></div>
                }
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
                <h1>Leaderboard</h1>
                <Leaderboard data={mainLeaderboard} /> 
                
            </div>
            <div className='home-part'>
                <h1>Regions</h1>
                {regions.map(element => {
                    return <RegionCard regionName={element.name} regionId={element.id} />
                })}

            </div>

        </div>


    </>)
}

export default Home;