
import { Link } from 'react-router-dom';
import { useParams } from "react-router-dom";
import Header from '../../components/header/header';
import './uspon.css'
import Lobby from '../../components/Lobby';
import Chat from '../../components/Chat';
import { HubConnectionBuilder, LogLevel, HttpTransportType } from '@microsoft/signalr';
import { useEffect, useState } from 'react';
import axios from 'axios';

function Uspon() {
    const { handle } = useParams();
    const [connection, setConnection] = useState();
    const [messages, setMessages] = useState([]);
    const [users, setUsers] = useState([]);
    const [loggedIn, setLoggedIn] = useState(false)
    const [info, setInfo] = useState(null)
    const [date, setDate] = useState();
    const [about, setAbout] = useState();
    const [nameOfUser, setNameOfUser] = useState("")
    const [difficulty, setDifficulty] = useState();
    const [type, setType] = useState();
    const [id, setId] = useState();
    const [score ,setScore] = useState(); 
    const [registered,setRegistered] = useState(false);
    const [moutnaintopId,setmountainTopId] =useState();
    const joinEvent = () => {
        const personId = localStorage.getItem("id");
        if (type === 'hike')
            axios.post(`https://localhost:5001/Person/mountaineer/${personId}/hike/${id}`)
                .then(res => {
                    alert("Prijavili ste se za planinarenje")
                })
                .catch(e => {
                    alert("Error" + e)
                })
        else
            axios.post(`https://localhost:5001/Person/mountaineer/${personId}/race/${id}`)
                .then(res => {
                    alert("Prijavili ste se za trku")
                })
                .catch(e => {
                    alert("Error" + e)
                })

    }
 
    const addScore = () => {
        const id = localStorage.getItem("id");
        var name = localStorage.getItem("name");
        axios.post(`https://localhost:5001/RedisLeaderboardControllers/CreateLeaderBoardPerson`, {
            personId: id,
            name: name,
            score: score,
            mountainTop:{
                id: moutnaintopId,
                name: handle,
                height: 3
            },
            timeStampRunner: new Date(),
        })
    }



    const joinRoom = async (user, room) => {
        try {
            const connection = new HubConnectionBuilder()
                .withUrl("https://localhost:5001/chat", {
                    skipNegotiation: true,
                    transport: HttpTransportType.WebSockets
                })
                .configureLogging(LogLevel.Information)
                .build();

            connection.on("UsersInRoom", (users) => {
                setUsers(users);
            });

            //mora da bude ime kao ovamo jer je to messageSubject
            connection.on("ReceiveMessage", (user, message) => {
                setMessages(messages => [...messages, { user, message }]);
            });

            connection.onclose(e => {
                setConnection();
                setMessages([]);
                setUsers([]);
            });

            await connection.start();
            await connection.invoke("JoinRoom", { user, room });
            setConnection(connection);
        }
        catch (e) {
            console.log(e);
        }
    }

    const closeConnection = async () => {
        try {
            await connection.stop();
        } catch (e) {
            console.log(e);
        }
    }

    const sendMessage = async (message) => {
        try {
            await connection.invoke("SendMessage", message);
        } catch (e) {
            console.log(e);
        }
    }
    useEffect(() => {


        if (localStorage.getItem("id") !== null)
            setLoggedIn(true)
        else
            setLoggedIn(false)

            
        if (info === null) {
            axios.get(`https://localhost:5001/event/${handle}`)
                .then(res => {
                    setInfo(res.data[0][0])
                    setDate(res.data[0][0].date)
                    setAbout(res.data[0][0].about)
                    setDifficulty(res.data[0][0].difficulty)
                    setType(res.data[0][0].type)

                    setId(res.data[0][0].id)
                    setmountainTopId(res.data[0][0].mountainTopId)
                    console.log(res.data[0][0])
                })
        }
        if(type ==="hike")
        {
            axios.get(`https://localhost:5001/Person/GetRegisteredPersonHike/${localStorage.getItem("id")}/${id}`)
            .then(res=>setRegistered(res.data))
        }
        if(type ==="race")
        {
            axios.get(`https://localhost:5001/Person/GetRegisteredPersonRace/${localStorage.getItem("id")}/${id}`)
            .then(res=>setRegistered(res.data))
        }
        if (localStorage.getItem("name") !== null) {
            setNameOfUser(localStorage.getItem("name"))
        }
        else
            setNameOfUser("")
    }, [info, loggedIn])

    return (<>
        <Header />
        <div className='uspon-main-div'>
            <h1>{handle}</h1>
            <div className='uspon'>
                <div className='uspon-info'>
                    <h2>Date:   {date}</h2>
                </div>
                <div className='uspon-about'>
                    <h3 className='abouth3'>About!</h3>
                    {about}
                    <h3 className='abouth3' >Difficulty: {difficulty}</h3>
                    
                </div>

            </div>
            {loggedIn ?
               !registered?
                <button className='sign-button' onClick={joinEvent}> Join Event</button>:
                <div>
                <input onChange={e=>setScore(e.target.value)}/>
                <button onClick={addScore}>Add score</button>
                </div>:<></>
                }
            <div>
                <h2>Chat for running</h2>
                <hr className='line'></hr>
                {loggedIn ?
                    !connection
                        ? <Lobby joinRoom={joinRoom} username={nameOfUser} roomname={handle} />
                        : <Chat messages={messages} sendMessage={sendMessage}
                            closeConnection={closeConnection} users={users} />

                    : <Link to="/signin"><button className='sign-button'>Sign in for joining chat</button></Link>
                }
            </div>
        </div>
    </>)
}

export default Uspon;