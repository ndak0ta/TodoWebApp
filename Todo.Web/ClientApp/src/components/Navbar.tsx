import * as React from "react";
import {AppBar, Container, Toolbar, Typography} from "@mui/material";

export default function Navbar() {
    return (
        <AppBar position="static" sx={{marginBottom: 10}}>
            <Container>
                <Toolbar disableGutters>
                    <Typography variant="h6" component="a" href="/"
                                sx={{color: 'white', textDecoration: 'none', fontWeight: 700}}>
                        To-Do
                    </Typography>
                </Toolbar>
            </Container>
        </AppBar>
    );
}
