import React, { Dispatch, SetStateAction, useEffect } from 'react';
import LoginFrom from '../components/LoginForm';


interface LoginPageProps {
    setToken: Dispatch<SetStateAction<string | null>>;
}

export default function LoginPage({ setToken }: LoginPageProps) {
    useEffect(() => {
        document.title = "Giriş yap";
    }, []);

    return (
        <LoginFrom setToken={setToken} />
    );
}