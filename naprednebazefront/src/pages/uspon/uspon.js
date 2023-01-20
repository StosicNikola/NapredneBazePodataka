
import { Link } from 'react-router-dom';
import {useParams } from "react-router-dom";
import Header from '../../components/header/header';
import './uspon.css'
import Lobby from '../../components/Lobby';
import Chat from '../../components/Chat';
import { HubConnectionBuilder, LogLevel , HttpTransportType} from '@microsoft/signalr';
import { useEffect, useState } from 'react';
import axios from 'axios';

function Uspon()
{
    const {handle}=useParams();
    const [connection,setConnection] = useState();
    const [messages,setMessages] = useState([]);
    const [users, setUsers] = useState([]);
    const [loggedIn, setLoggedIn] = useState()
    const [info,setInfo] = useState(null)

    const joinRoom = async (user,room) =>{
        try{
            const connection = new HubConnectionBuilder()
            .withUrl("https://localhost:5001/chat",{
                skipNegotiation: true,
                transport: HttpTransportType.WebSockets
            })
            .configureLogging(LogLevel.Information)
            .build();

            connection.on("UsersInRoom", (users) => {
                setUsers(users);
            });

            //mora da bude ime kao ovamo jer je to messageSubject
            connection.on("ReceiveMessage",(user,message)=>{
                setMessages(messages => [...messages, {user,message}]);
            });

            connection.onclose(e =>{
                setConnection();
                setMessages([]);
                setUsers([]);
            });

            await connection.start();
            await connection.invoke("JoinRoom", {user,room});
            setConnection(connection);
        }
        catch(e){
            console.log(e);
        }
    }

    const closeConnection = async() => {
        try{
            await connection.stop();
        }catch(e){
            console.log(e);
        }
    }

    const sendMessage = async(message) => {
        try{
            await connection.invoke("SendMessage", message);
        } catch(e){
            console.log(e);
        }
    }
    useEffect(()=>
    {
        if (localStorage.getItem("id") !== null)
            setLoggedIn(true)
        else
            setLoggedIn(false)
        if(info ===null)
        {
            axios.get()
        }
    })

    return(<div className='uspon-main-div'>
        <Header />
        <h1>{handle}</h1>
        <div className='uspon'>
            <div className='uspon-info'>
            <h2>Datum: dd.mm.yyyy.</h2>
            </div>
            <div className='uspon-about'>
                <h3>About!</h3>
                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Maecenas nunc justo, rhoncus et tortor non, consectetur ornare magna. Integer mattis dui a ante interdum, vitae tincidunt mauris suscipit. Pellentesque congue, elit et pretium volutpat, ante purus efficitur lorem, in lobortis tellus justo sed dolor. Phasellus eget dui a turpis maximus aliquam non quis dui. Donec id justo bibendum tortor molestie rutrum et a neque. Cras lobortis tincidunt lorem vel blandit. Donec ac rutrum nulla, sed bibendum enim. Maecenas sit amet enim magna. Mauris faucibus mollis turpis, sit amet congue erat molestie eu. Quisque pellentesque nunc tellus, a euismod urna tristique vitae. Aliquam nisl mi, ullamcorper non augue a, egestas ornare libero. Integer eget augue quis odio commodo dapibus. Nulla facilisi. Vivamus sit amet vestibulum quam. Integer ut lorem ultricies, egestas turpis sed, pellentesque dui. Aliquam malesuada ac nulla sit amet malesuada.</p></div>
        
                
        </div>
        <div>
            <h2>Chat for running</h2>
            <hr className='line'></hr>
            {!connection
                ?<Lobby joinRoom ={joinRoom} />
                :<Chat messages={messages} sendMessage= {sendMessage}
                 closeConnection={closeConnection} users={users}/>
            }
        </div>
       
    </div>)
}

export default Uspon;