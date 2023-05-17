import * as React from "react";
import {
    Button,
    Card, CardActionArea,
    CardContent,
    Dialog, DialogActions,
    DialogContent,
    DialogTitle,
    TextField
} from "@mui/material";
import AddIcon from '@mui/icons-material/Add';
import styled from 'styled-components';
import axios from "axios";
import {useState} from "react";

const StyledCard = styled(Card)`
  height: 300px;
  width: 300px;
  display: flex;
  justify-content: center;
  align-items: center;
`;

interface ITodoAddCardProps {
    onAddTodo: (e: Event, todo: { Header: string, Body: string, Date: Date}) => void;
}

export default function TodoAddCard(props: ITodoAddCardProps) {
    const [open, setOpen] = React.useState(false);
    const [header, setHeader] = useState('');
    const [body, setBody] = useState('');

    const handleClickOpen = () => {
        setOpen(true);
    };

    const handleClose = () => {
        setOpen(false);
    };

    const handleAddTodo = (e: any) => {
        e.preventDefault();

        const newTodoItem = {
            Header: header,
            Body: body,
            Date: new Date()
        };

        props.onAddTodo(e, newTodoItem);

        setHeader('');
        setBody('');
        handleClose()
    }

    return (
        <div>
            <StyledCard>
                <CardActionArea onClick={handleClickOpen} sx={{display: 'flex', height: '100%', width: '100%'}}>
                    <CardContent>
                        <AddIcon/>
                    </CardContent>
                </CardActionArea>
            </StyledCard>


            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Todo Ekle</DialogTitle>
                <DialogContent>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="header"
                        label="Başlık"
                        type="text"
                        fullWidth
                        variant="standard"
                        onChange={(e) => setHeader(e.target.value)}
                    />
                    <TextField
                        autoFocus
                        margin="dense"
                        id="body"
                        label="İçerik"
                        type="text"
                        fullWidth
                        variant="standard"
                        onChange={(e) => setBody(e.target.value)}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button onClick={handleAddTodo}>Add</Button>
                </DialogActions>
            </Dialog>
        </div>
    );
}