import React, { ReactNode } from 'react';
import Navbar from './Navbar';
import Container from '@mui/material/Container';


interface LayoutProps {
    children: ReactNode;
}

export default function Layout({ children }: LayoutProps) {
    return (
        <div>
            <Navbar />
            <Container>
                {children}
            </Container>
        </div>
    );
}