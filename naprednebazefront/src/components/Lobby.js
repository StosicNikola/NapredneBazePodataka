
import { useState } from "react";
import {Button, Form} from "react-bootstrap";

const Lobby = ({joinRoom, username, roomname}) => {

    const [user, setUser] = useState();
    const [room, setRoom] = useState();

    return <Form className ="lobby"
        onSubmit={e => {
            e.preventDefault();
            joinRoom(username,roomname);
        }}>
        <Button variant='success' type='submit' disabled ={!username || !roomname}>Join</Button>
    </Form>
}

export default Lobby;