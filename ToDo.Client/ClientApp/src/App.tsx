import * as React from 'react';
import './App.css';
import NavBar from "./components/NavBar";
import ToDoCardLayout from "./components/ToDoCardLayout";

export default function App() {
    return(
        <div>
            <NavBar/>
            <ToDoCardLayout/>
        </div>
    );
}
