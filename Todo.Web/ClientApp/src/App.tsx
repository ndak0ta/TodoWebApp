import React, { useState } from 'react';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './components/Layout';
import LoginPage from "./pages/LoginPage";
import TodoPage from './pages/TodoPage';
import './App.css';


export default function App() {
    const [token, setToken] = useState<string | null>(null);

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
