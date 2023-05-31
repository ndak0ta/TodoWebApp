import React from "react";
import {useLocation, useNavigate } from 'react-router-dom';
import {AppBar, Button, Container, Toolbar, Typography} from "@mui/material";

export default function Navbar() {
    const location = useLocation();
    const navigate = useNavigate();

    const logout = () => {
        localStorage.removeItem('token');
        navigate('/login');
    }

    return (
        <AppBar position="static" sx={{marginBottom: 10}}>
            <Container>
                <Toolbar disableGutters>
                    <Typography variant="h6" component="a" href="/" sx={{flexGrow: 1, color: 'white', textDecoration: 'none', fontWeight: 700}}>
                        To-Do
                    </Typography>
                    {location.pathname === '/' && (<Button variant="text" sx={{ color: 'white' }} onClick={logout}>Çıkış Yap</Button>)}
                </Toolbar>
            </Container>
        </AppBar>
    );
}
