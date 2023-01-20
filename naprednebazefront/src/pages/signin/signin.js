import { useState } from "react";
import axios from 'axios';
import './signin.css'
import { useNavigate } from "react-router-dom";
import Header from '../../components/header/header';
function SignIn() {

    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const navigate = useNavigate();

    const signin = () => {
        if (email !== "" && password !== "") {
            axios.get(`https://localhost:5001/SignIn/${email}/${btoa(password)}`)
                .then(res => {
                    localStorage.setItem("role", res.data[0].person.role)
                    localStorage.setItem("id", res.data[0].person.id)
                    localStorage.setItem("name",res.data[0].person.name)
                    localStorage.setItem("surname",res.data[0].person.surname)
                    console.log(res.data[0])
                    navigate("/")
                })
                .catch(err => (alert(err)))
        }
        else
            alert("Password and email can't be empty!")
    }

    return (<>
    <Header />
        <div className="signin">
            <div className="signin-part">
                <p>Email:</p>
                <input onChange={(e) => { setEmail(e.target.value) }} />
            </div>
            <div className="signin-part">
                <p>Password:</p>
                <input type="password" onChange={(e) => { setPassword(e.target.value) }} />
            </div>
            <button className="signin-button" onClick={signin}>Sign in</button>

        </div>
    </>)
}

export default SignIn;