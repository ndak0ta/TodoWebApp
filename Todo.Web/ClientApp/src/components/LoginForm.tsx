import React, { Dispatch, FormEvent, SetStateAction, useState } from 'react';
import { Navigate, useNavigate } from 'react-router-dom';
import Button from '@mui/material/Button';
import TextField from '@mui/material/TextField';
import Card from '@mui/material/Card';
import Box from '@mui/material/Box';
import axios from 'axios';


interface LoginFormProps {
    setToken: Dispatch<SetStateAction<string | null>>;
}

export default function LoginFrom({ setToken }: LoginFormProps) {
    const [userName, setUserName] = useState('');
    const [password, setPassword] = useState('');

    const navigate = useNavigate();

    const handleSubmit = async (e: FormEvent) => {
        e.preventDefault();

        try {
            const response = await axios.post('/api/user/login', { userName, password });
            setToken(response.data.token);
            navigate('/');
        } catch (error) {
            console.error('Giriş yapılamadı:', error);
        }
    };

    return (
        <Box sx={{ width: '100%' }}>
            <Card sx={{ width: 300, margin: 'auto' }}>
                <form onSubmit={handleSubmit}>
                    <Box sx={{ width: '150', margin: 'auto', paddingTop: 2, paddingBottom: 2 }}>
                        <TextField
                            id="outlined-basic"
                            label="Kullanıcı Adı"
                            variant="outlined"
                            sx={{ display: 'block' }}
                            value={userName}
                            onChange={(e) => setUserName(e.target.value)}
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