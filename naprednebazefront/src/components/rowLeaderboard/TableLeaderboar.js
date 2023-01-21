import axios from "axios";
import { useState } from "react";



function TableLeaderboar() {
    const [mainLeaderboard, setMainLeaderboard] = useState([])
    

    const returnMainleaderboardFrom0toNitems = () => {
        axios.get(`https://localhost:5001//RedisLeaderboardControllers/GetMainleaderboardFrom0toNitems${"100"}`)
            .then(res => {
                console.log(res);
                /*setMainLeaderboard(res.data[0])
                setMainLeaderboard(true)*/
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
            {mainLeaderboard.map(row => 
                <Row 
                score = {row.Score}
                mountain = {row.Mountain.name}
                name ={row.Name}
                time = {row.TimeStampRunner}
                />
            )}
        </tbody>
    </table>)
}

export default TableLeaderboar;