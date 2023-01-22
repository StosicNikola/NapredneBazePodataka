import logo from './logo.svg';
import './App.css';
import ReactDOM from "react-dom/client";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import Home from './pages/home/home';
import SignIn from './pages/signin/signin';
import SignUp from './pages/signup/signup';
import Uspon from './pages/uspon/uspon';
import Header from './components/header/header';
import Lobby from './components/Lobby';
import 'bootstrap/dist/css/bootstrap.min.css';
import Account from './pages/Account/Account';



function App() {

  return (
    <BrowserRouter>
      <Routes>  
        <Route path='/' element={<Home/>}/>
        <Route path='/Lobby' element={<Lobby/>}/>
        <Route path='/signin' element={<SignIn/>}/>
        <Route path='/signup' element={<SignUp/>}/>
        <Route path='/hike/:handle' element={<Uspon/>}/>
        <Route path='/race/:handle' element={<Uspon/>}/>
        <Route path='/account' element={<Account />}/>
      </Routes>
    </BrowserRouter>
  );
}

export default App;
