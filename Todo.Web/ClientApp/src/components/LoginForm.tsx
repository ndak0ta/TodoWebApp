import React, { Dispatch, FormEvent, SetStateAction, useState } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Card from '@mui/material/Card';
import Box from '@mui/material/Box';
import axios from 'axios';


interface LoginFormProps {
    setToken: Dispatch<SetStateAction<string | null>>;
    setError: Dispatch<SetStateAction<string | null>>;
}

export default function LoginFrom({ setToken, setError }: LoginFormProps) {
    const [username, setUsername] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    const handleLogin = async (e: FormEvent) => {
        e.preventDefault();

        try {
            const response = await axios.post('/api/auth/login', { username, password }, {
                validateStatus: (status) => {
                    if (status === 404) {
                        setError('Kullanıcı bulunamadı');
                    }
                    return (status >= 200 && status < 300);
            }});
            setToken(response.data.token);
            localStorage.setItem('token', response.data.token);
            navigate('/');
        } catch (error: any) {
            console.error('Giriş yapılamadı:', error);
        }
    };

    return (
        <Box sx={{ width: '100%' }}>
            <Card sx={{ width: 300, margin: 'auto' }}>
                <form onSubmit={handleLogin}>
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
                        <Button type="submit" variant="contained">Giriş</Button>
                    </Box>
                </form>
            </Card>
        </Box>
    );
}