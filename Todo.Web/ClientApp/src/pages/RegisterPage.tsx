import React, { Dispatch, SetStateAction, useEffect, useState } from 'react';
import Alert from '@mui/material/Alert';
import RegisterForm from '../components/RegisterForm';

interface RegisterPageProps {
    setToken: Dispatch<SetStateAction<string | null>>;
}

export default function RegisterPage({ setToken }: RegisterPageProps) {
    const [error, setError] = useState<string | null>(null);

    useEffect(() => {
        document.title = "Üye ol";
    }, []);

    return (
        <div>
            {error && (<Alert severity="error">{error}</Alert>)}
            <RegisterForm setToken={setToken} setError={setError} />
        </div>
    );
}