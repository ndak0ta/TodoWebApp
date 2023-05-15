import * as React from "react";
import {Card, CardContent, CardHeader, CardActionArea, Typography, IconButton} from "@mui/material";
import CloseIcon from '@mui/icons-material/Close';

interface IToDoCardProps {
    header: string;
    body: string;
    date: Date;
}

export default function ToDoCard(props: IToDoCardProps) {
    function convertDate(inputFormat: Date) {
        function pad(s: number) { return (s < 10) ? '0' + s : s; }
        let d = new Date(inputFormat)
        return [pad(d.getDate()), pad(d.getMonth()+1), d.getFullYear()].join('/')
    }
    
    return (
        <Card sx={{width: '300px', maxHeight: '300px'}}>
            <CardActionArea>
                <CardHeader title={props.header} subheader={convertDate(props.date)} action={
                    <IconButton aria-label="close">
                        <CloseIcon></CloseIcon>
                    </IconButton>
                }/>
                <CardContent>
                    <Typography variant="body1">{props.body}</Typography>
                </CardContent>
            </CardActionArea>
        </Card>
    );
}
