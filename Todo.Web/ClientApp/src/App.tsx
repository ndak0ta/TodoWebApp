import React, { useEffect, useState } from 'react';
import {BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import LoginPage from "./pages/LoginPage";
import TodoPage from './pages/TodoPage';
import axios from 'axios';
import './App.css';


export default function App() {
    const [token, setToken] = useState<string | null>(localStorage.getItem('token') || null);

    const AppRoutes = [
        {
            index: true,
            element: <TodoPage token={token} />
        },
        {
            path: '/login',
            element: <LoginPage setToken={setToken} />
        }
    ];

    axios.interceptors.response.use(
        response => response,
        error => {
            if (error.response && error.response.status === 401) {
                localStorage.removeItem('token');
                setToken(null);
            }
        }
    );

    useEffect(() => {
        if (token) {
            localStorage.setItem('token', token);
        } else {
            localStorage.removeItem('token');
        }
    }, [token]);

    return (
        <BrowserRouter>
            <Layout>
                <Routes>
                    {AppRoutes.map((route, index) => {
                        const { element, ...rest } = route;
                        return <Route key={index} {...rest} element={element} />;
                    })}

                    
                </Routes>
            </Layout>
        </BrowserRouter>
    );
}
