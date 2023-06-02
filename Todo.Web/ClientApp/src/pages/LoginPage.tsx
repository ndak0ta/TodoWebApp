import React, { Dispatch, SetStateAction, useEffect, useState } from 'react';
import Alert from '@mui/material/Alert';
import LoginFrom from '../components/LoginForm';


interface LoginPageProps {
    setToken: Dispatch<SetStateAction<string | null>>;
}

export default function LoginPage({ setToken }: LoginPageProps) {
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        document.title = "Giriş yap";
    }, []);

    return (
        <div>
            {error && (<Alert severity="error">{error}</Alert>)}
            <LoginFrom setToken={setToken} setError={setError} />
        </div>
    );
}