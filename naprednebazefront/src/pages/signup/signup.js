import { useState } from "react";
import axios from 'axios';
import "./signup.css"
import Header from '../../components/header/header';
import { useNavigate } from "react-router-dom";

function SignUp() {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [account, setAccount] = useState(false);
    const [accountId, setAccountId] = useState(false);
    const [name, setName] = useState("")
    const [surname, setSurname] = useState("")
    const [age, setAge] = useState(0);
    const [add, setAdd] = useState(0);
    const [mounteneer, setMounteneer] = useState(false);
    const [referee, setReferee] = useState(false);
    const [hikingGuide, sethikingGuide] = useState(false);
    const navigate = useNavigate();
    const signup = () => {
        axios.post(`https://localhost:5001/SignUp/Mountaineer/${email}/${btoa(password)}`)
            .then(res => {
                setAccountId(res.data[0].id)
                setAccount(true)
            })
            .catch(e => {
                alert(e)
            })

    }

    const mounteneerChoose = () => {
        setMounteneer(true)
        setReferee(false)
        sethikingGuide(false)
    }
    const refereeChoose = () => {
        setMounteneer(false)
        setReferee(true)
        sethikingGuide(false)
    }
    const hikingGuideChoose = () => {
        setMounteneer(false)
        setReferee(false)
        sethikingGuide(true)
    }



    const mountaineerSubmit = () => {
        axios.post(`https://localhost:5001/Person/mountaineer/${name}/${surname}/${age}/${add}/${accountId}`)
            .then(res => {
                alert("Nalog kreiran")
                navigate("/")
            })
            .catch(e => alert(e))
    }
    const refereeSubmit = () => {
        axios.post(`https://localhost:5001/Person/referee/${name}/${surname}/${age}/${accountId}`)
            .then(res => {
                alert("Nalog kreiran")
                navigate("/")
            })
            .catch(e => alert(e))
    }
    const hikingGuideSubmit = () => {
        axios.post(`https://localhost:5001/Person/hikingguide/${name}/${surname}/${age}/${add}/${accountId}`)
            .then(res => {
                alert("Nalog kreiran")
                navigate("/")
            })
            .catch(e => alert(e))
    }

    return (<><Header />
        {!account &&
            <div className="signin">
                <div className="signin-part">
                    <p>Email:</p>
                    <input onChange={(e) => { setEmail(e.target.value) }} />
                </div>
                <div className="signin-part">
                    <p>Password:</p>
                    <input type="password" onChange={(e) => { setPassword(e.target.value) }} />
                </div>
                <button className="signin-button" onClick={signup}>Sign up</button>

            </div>
        }
        {account &&
            <div className="acc-option">
                <h3>Choose account type:</h3>
                <button className="acc-button" onClick={mounteneerChoose}>Mounteneer</button>
                <button className="acc-button" onClick={refereeChoose}>Referee</button>
                <button className="acc-button" onClick={hikingGuideChoose}>HikingGuide</button>

            </div>


        }
        {
            mounteneer &&

            <div className="form">
                <h3>Mountaineer</h3>
                <div className="signin-part">
                    <p>Name:</p>
                    <input onChange={(e) => { setName(e.target.value) }} />
                </div>
                <div className="signin-part">
                    <p>Surname:</p>
                    <input onChange={(e) => { setSurname(e.target.value) }} />
                </div>
                <div className="signin-part">
                    <p>Age:</p>
                    <input onChange={(e) => { setAge(e.target.value) }} />
                </div>
                <div className="signin-part">
                    <p>Member Card:</p>
                    <input onChange={(e) => { setAdd(e.target.value) }} />
                </div>
                <button className="acc-button" onClick={mountaineerSubmit}>Submin</button>
            </div>
        }
        {
            referee &&
            <div className="form">
                <h3>Referee</h3>
                <div className="signin-part" >
                    <p>Name:</p>
                    <input onChange={(e) => { setName(e.target.value) }} />
                </div>
                <div className="signin-part">
                    <p>Surname:</p>
                    <input onChange={(e) => { setSurname(e.target.value) }} />
                </div>
                <div className="signin-part">
                    <p>Age:</p>
                    <input onChange={(e) => { setAge(e.target.value) }} />
                </div>

                <button className="acc-button" onClick={refereeSubmit}>Submin</button>
            </div>
        }
        {
            hikingGuide &&
            <div className="form">
                <h3>Hiking guide</h3>
                <div className="signin-part">
                    <p>Name:</p>
                    <input onChange={(e) => { setName(e.target.value) }} />
                </div>
                <div className="signin-part">
                    <p>Surname:</p>
                    <input onChange={(e) => { setSurname(e.target.value) }} />
                </div>
                <div className="signin-part">
                    <p>Age:</p>
                    <input onChange={(e) => { setAge(e.target.value) }} />
                    <div className="signin-part">
                        <p>licence number:</p>
                        <input onChange={(e) => { setAdd(e.target.value) }} />
                    </div>
                </div>
                <button className="acc-button" onClick={hikingGuideSubmit}>Submin</button>
            </div>
        }
    </>)
}

export default SignUp;