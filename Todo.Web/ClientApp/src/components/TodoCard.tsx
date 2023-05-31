import React, { MouseEvent } from "react";
import {useEffect, useState} from "react";
import {
    Button,
    Card,
    CardActionArea,
    CardContent,
    CardHeader,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    IconButton,
    TextField,
    Typography
} from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';
import styled from "styled-components";


type TodoItem = {
    id: number,
    header: string,
    body: string,
    date: Date
}

interface ITodoCardProps {
    todoItem: TodoItem;
    onUpdateTodo: (e: MouseEvent<HTMLButtonElement>, updatedTodoItem: TodoItem) => void;
    onRemoveTodo: (id: number) => void;
}

const StyledCard = styled(Card)`
  height: 300px;
  width: 300px;
  display: flex;
`;

export default function TodoCard(props: ITodoCardProps) {
    const [open, setOpen] = useState(false);
    const [header, setHeader] = useState('');
    const [body, setBody] = useState('');
    const [cardOpacity, setCardOpacity] = useState(1);

    const handleClickOpen = () => {
        setOpen(true);
        setHeader(props.todoItem.header);
        setBody(props.todoItem.body);
    };

    const handleClose = () => {
        setOpen(false);
    };
    
    const handleUpdateTodo = (e: MouseEvent<HTMLButtonElement>) => {
        const updatedTodo = {
            id: props.todoItem.id,
            header: header,
            body: body,
            date: props.todoItem.date
        }
        
        props.onUpdateTodo(e, updatedTodo);
        
        handleClose()
    };
    
    const handleRemoveClick = () => {
        setCardOpacity(0.25);
        props.onRemoveTodo(props.todoItem.id);
        setCardOpacity(1);
    };

    function convertDate(input: Date) {
        if (input === undefined)
            return "Tarih bilgisi alınamadı"
        
        const date = new Date(input);
        const day = date.getDate() < 10 ? `0${date.getDate()}` : date.getDate();
        const month = (date.getMonth() + 1) < 10 ? `0${date.getMonth() + 1}` : date.getMonth() + 1;
        const year = date.getFullYear();
        return `${day}/${month}/${year}`;
    }

    return (
        <div>
            <StyledCard sx={{opacity: cardOpacity, position: 'relative'}}>
                <IconButton aria-label="close" onClick={handleRemoveClick}
                            sx={{position: 'absolute', top: 20, right: 15, zIndex: 999}}>
                    <CloseIcon></CloseIcon>
                </IconButton>
                <CardActionArea onClick={handleClickOpen}>
                    <CardHeader title={props.todoItem.header} subheader={convertDate(props.todoItem.date)}
                                sx={{position: 'absolute', top: 5, left: 5}}/>
                    <CardContent>
                        <Typography variant="body1">{props.todoItem.body}</Typography>
                    </CardContent>
                </CardActionArea>
            </StyledCard>

            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>Todo Güncelle</DialogTitle>
                <DialogContent>
                    <TextField
                        autoFocus
                        margin="dense"
                        id="header"
                        label="Başlık"
                        type="text"
                        fullWidth
                        variant="standard"
                        value={header}
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
                        value={body}
                        onChange={(e) => setBody(e.target.value)}
                    />
                </DialogContent>
                <DialogActions>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button onClick={handleUpdateTodo}>Update</Button>
                </DialogActions>
            </Dialog>
        </div>
        
    );
}
