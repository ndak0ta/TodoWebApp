import * as React from "react";
import {Card, CardContent, CardHeader, CardActionArea, Typography, IconButton} from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';
import {useState} from "react";


type TodoItem = {
    id: number,
    header: string,
    body: string,
    date: Date
}

interface ITodoCardProps {
    todoItem: TodoItem;
    onRemoveTodo: (id: number) => void;
}

export default function TodoCard(props: ITodoCardProps) {
    const [cardOpacity, setCardOpacity] = useState(1);
    
    const handleRemoveClick = () => {
        try {
            setCardOpacity(0.25);
            props.onRemoveTodo(props.todoItem.id)
        }
        catch (err) {
            setCardOpacity(1);
        }
    }

    function convertDate(inputFormat: Date) {
        function pad(s: number) {
            return (s < 10) ? '0' + s : s;
        }

        let d = new Date(inputFormat)
        return [pad(d.getDate()), pad(d.getMonth() + 1), d.getFullYear()].join('/')
    }

    return (
        <Card sx={{width: '300px', maxHeight: '300px', opacity: cardOpacity}}>
            <CardActionArea>
                <CardHeader title={props.todoItem.header} subheader={convertDate(props.todoItem.date)} action={
                    <IconButton aria-label="close" onClick={handleRemoveClick}>
                        <CloseIcon></CloseIcon>
                    </IconButton>
                }/>
                <CardContent>
                    <Typography variant="body1">{props.todoItem.body}</Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}
