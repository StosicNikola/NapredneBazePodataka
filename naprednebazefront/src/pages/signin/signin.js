import { useState } from "react";
import Header from "../../components/header/header";
import './signin.css'
function SignIn()
{

    const[email, setEmail] = useState("");
    const[password, setPassword] = useState("");

    const signin = () =>
    {
        console.log(email)
        console.log(password)
    }

    return(<>
        <div className="signin">
            <div className="signin-part">
                <p>Email:</p>
                <input onChange={(e)=>{setEmail(e.target.value)}}/>
            </div>
            <div className="signin-part">
                <p>Password:</p>
                <input type="password" onChange={(e)=>{setPassword(e.target.value)}}/>
            </div>
            <button className="signin-button" onClick={signin} >Sign in</button>

        </div>
    </>)
}

export default SignIn;