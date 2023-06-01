import React, { Dispatch, FormEvent, SetStateAction, useState } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Card from '@mui/material/Card';
import Box from '@mui/material/Box';
import axios from 'axios';


interface RegisterFormProps {
    setToken: Dispatch<SetStateAction<string | null>>;
}

export default function RegisterForm({ setToken }: RegisterFormProps) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    const handleLogin = async (e: FormEvent) => {
        e.preventDefault();

        try {
            const response = await axios.post('/api/auth/login', { username, password });
            setToken(response.data.token);
            localStorage.setItem('token', response.data.token)
            navigate('/');
        } catch (error) {
            console.error('Giriş yapılamadı:', error);
        }
    };

    const handleRegister = async (e: FormEvent) => {
        e.preventDefault();

        const response = await axios.post('api/user', { username, password })
            .then(() => handleLogin(e))
            .catch((err) => console.error(err));
    }

    return (
        <Box sx={{ width: '100%' }}>
            <Card sx={{ width: 300, margin: 'auto' }}>
                <form onSubmit={handleRegister}>
                    <Box sx={{ width: '150', margin: 'auto', paddingTop: 2, paddingBottom: 2 }}>
                        <TextField
                            id="outlined-basic"
                            label="Kullanıcı Adı"
                            variant="outlined"
                            sx={{ display: 'block' }}
                            value={username}
                            onChange={(e) => setUsername(e.target.value)}
                        />
                        <TextField
                            id="outlined-basic"
                            label="Şifre"
                            variant="outlined"
                            type='password'
                            sx={{ display: 'block' }}
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        <Button type="submit" variant="contained">Üye ol</Button>
                    </Box>
                </form>
            </Card>
        </Box>
    );
}