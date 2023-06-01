import React, { Dispatch, SetStateAction, useEffect } from 'react';
import RegisterFrom from '../components/RegisterForm';

interface RegisterPageProps {
    setToken: Dispatch<SetStateAction<string | null>>;
}

export default function RegisterPage({ setToken }: RegisterPageProps) {
    useEffect(() => {
        document.title = "Üye ol";
    }, []);

    return (
        <RegisterFrom setToken={setToken} />
    );
}