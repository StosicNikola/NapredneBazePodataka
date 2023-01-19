import { useState } from "react";
import Header from "../../components/header/header";

function SignUp()
{
    const[email, setEmail] = useState("");
    const[password, setPassword] = useState("");
    const[firstName, setFirstName] = useState("");
    const[lastName, setLastName] = useState("");

    const signup = () => 
    {
        console.log(firstName)
        console.log(lastName)
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
            <button className="signin-button" onClick={signup}>Sign up</button>

        </div>
        </>)
}

export default SignUp;