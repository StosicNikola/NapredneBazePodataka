import axios from "axios";
import { useEffect, useState } from "react";
import Header from "../../components/header/header";

function Account() {
    const [person, setPerson] = useState(undefined);
    const [edit, setEdit] = useState(false);
    const [name, setName] = useState("");
    const [surname, setSurname] = useState("");
    const [age, setAge] = useState("");


    const deleteAcc = () =>
    {
        axios.delete(``)
    }



    useEffect(() => {
        if (person === undefined) {
            var id = localStorage.getItem("id")
            if (localStorage.getItem("role") === "Mountaineer") {
                axios.get(`https://localhost:5001/Person/mountaineer/id/${id}`)
                    .then(res => {
                        setPerson(res.data[0][0].person)

                    })
                    .catch(e => alert(e))
            }

            if (localStorage.getItem("role") === "HikingGuide") {
                axios.get(`https://localhost:5001/Person/hikingguide/id/${id}`)
                    .then(res => {
                        setPerson(res.data[0][0].person)


                    })
                    .catch(e => alert(e))
            }

            if (localStorage.getItem("role") === "Referee") {
                axios.get(`https://localhost:5001/Person/referee/id/${id}`)
                    .then(res => {
                        setPerson(res.data[0][0].person)


                    })
                    .catch(e => alert(e))
            }
        }
    })
    return (<>
        <Header />
        <div>
            <div className="account-info">
                <h1>Information</h1>
                {!edit && <div>
                    <p>Name: {person.name}</p>
                     <p>Surname: {person.surname}</p>
                    <p>Age: {person.age}</p>
                    <button onClick={setEdit(!edit)}></button> 
                    <button onClick={deleteAcc}>Delete account</button>
                    </div>}
                    {edit && <div>
                        <div>
                        <p>Name: </p><input value={person.name}  onChange={(e)=>setName(e.target.value)}/>
                        </div>
                        <div>
                        <p>Surname: </p><input value={person.surname}  onChange={(e)=>setSurname(e.target.value)} />
                        </div>
                        <div>
                        <p>Age: </p><input value={person.age}  onChange={(e)=>setAge(e.target.value)}/>
                        </div>
                        <button onClick={setEdit(!edit)}></button> 
                    </div>
                    
                    }

            </div>
        </div>

    </>)
}
export default Account;