import axios from "axios";
import { useState } from "react";



function TableLeaderboar() {
    const [mainLeaderboard, setMainLeaderboard] = useState([])
    
    const tmp = 10;
    const returnMainleaderboardFrom0toNitems = () => {
        axios.get(`https://localhost:5001/RedisLeaderboardControllers/GetMainleaderboardFrom0toNitems/${tmp}`)
            .then(res => {      
                console.log(res.data[0]);
                
                setMainLeaderboard(res.data[0])
                
            })
            .catch(e => alert(e))
    }
    const Row = (props) => {
        const{score,mountain,name,time} = props;
        return (<tr>
            <td>{score}</td>
            <td>{mountain}</td>
            <td>{name}</td>
            <td>{time}</td>
        </tr>)
    }
    return( <table>
         <button onClick= {returnMainleaderboardFrom0toNitems}>Main leaderboard</button>
         <tr>
            <th>Score</th>
            <th>Mountain</th>
            <th>Name of runner</th>
            <th>Time</th>
        </tr>
        <tbody>
            {
            mainLeaderboard.map(row =>{
                return <Row 
                score = {row.Score}
                mountain = {row.Mountain.name}
                name ={row.Name}
                time = {row.TimeStampRunner}
                />
})}
        </tbody>
    </table>)
}

export default TableLeaderboar;