import axios from "axios";
import { useEffect, useState } from "react";
import Header from "../../components/header/header";
import { useNavigate } from "react-router-dom";
import "./Account.css"

function Account() {
    const [person, setPerson] = useState(undefined);
    const [edit, setEdit] = useState(false);
    const [name, setName] = useState(null);
    const [surname, setSurname] = useState("");
    const [age, setAge] = useState("");
    const navigate = useNavigate();


    const deleteAcc = () => {
        axios.delete(`https://localhost:5001/Person/${person.Id}/${person.accountId}`)
            .then(res => res)
            .catch(e => alert(e))
        localStorage.clear()
        navigate("/")

    }
    const editAcc = () => {

        setEdit(!edit)
        console.log(edit)
    }
    const submitEdit = () => {
        if (localStorage.getItem("role") === "Mountaineer") {
            axios.put(`https://localhost:5001/Person/mountaineer`, { name: name, surname: surname, age: age })
                .then((res => {
                    alert("Updatovan profil")
                }))
                .catch(e => {
                    alert(e)
                })

        }
        if (localStorage.getItem("role") === "Referee") {
            axios.put(`https://localhost:5001/Person/referee`, { name: name, surname: surname, age: age })
                .then((res => {
                    alert("Updatovan profil")
                }))
                .catch(e => {
                    alert(e)
                })

        }
        if (localStorage.getItem("role") === "HikingGuide") {
            axios.put(`https://localhost:5001/Person/hikingguide`, { name: name, surname: surname, age: age })
                .then((res => {
                    alert("Updatovan profil")
                }))
                .catch(e => {
                    alert(e)
                })
        }

        setEdit(!edit)
    }

    useEffect(() => {
        if (name === null) {
            var id = localStorage.getItem("id")
            console.log(id)
            axios.get(`https://localhost:5001/Person/${id}`)
                .then(res => {
                    setName(res.data[0][0].person.name)
                    setSurname(res.data[0][0].person.surname)
                    setAge(res.data[0][0].person.age)
                    console.log(res.data[0][0].person)
                })
                .catch(e => alert(e))
        }
    }, name)


    return (<>
        <Header />
        <div>
            <div className="account-info">
                <h1>Information</h1>
                {!edit && <div>
                    <p>Name: {name}</p>
                    <p>Surname: {surname}</p>
                    <p>Age: {age}</p>
                    <button className="sign-button" onClick={editAcc}>Edit</button>
                </div>}
                {edit && <div>
                    <div>
                        <p>Name: </p><input value={name} onChange={(e) => setName(e.target.value)} />
                    </div>
                    <div>
                        <p>Surname: </p><input value={surname} onChange={(e) => setSurname(e.target.value)} />
                    </div>
                    <div>
                        <p>Age: </p><input value={age} onChange={(e) => setAge(e.target.value)} />
                    </div>
                    <button className="sign-button" onClick={submitEdit} >Submit</button>
                    <button className="sign-button" onClick={editAcc}>Edit</button>
                </div>

                }


            </div>
        </div>

    </>)
}
export default Account;