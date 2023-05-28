import React, { useEffect } from 'react';
import { Navigate } from 'react-router-dom';
import TodoCards from '../components/TodoCards';

interface TodoPageProps {
    token: string | null;
}

export default function TodoPage({ token }: TodoPageProps) {
    useEffect(() => {
        document.title = "Todo";
    }, []);

    if (!token) {
        return <Navigate to="/login" />;
    }

    return (
        <TodoCards token={token} />
    );
}