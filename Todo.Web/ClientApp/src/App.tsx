import * as React from 'react';
import './App.css';
import NavBar from "./components/NavBar";
import TodoCards from "./components/TodoCards";

export default function App() {
    return(
        <div>
            <NavBar/>
            <TodoCards/>
        </div>
    );
}
