import * as React from "react";
import {useEffect, useState} from "react";
import {Container, Grid} from "@mui/material";
import ToDoCard from "./ToDoCard";
import axios from "axios"

interface IToDoProps {
    id: Number,
    header: string,
    body: string,
    date: Date
}

export default function ToDoCardLayout() {
    const [toDos, setData] = useState(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);


    useEffect(() => {
        const getData = async () => {
            try {
                const response = await axios.get('todo');
                
                setData(response.data);
                setError(null);
            } catch (err: any) {
                setError(err.message);
                setData(null);
            } finally {
                setLoading(false);
            }
        }

        getData();
    }, []);

    return (
        <Container>
            {loading && <div>Yükleniyor...</div>}
            {error && (<div>{`Hata - ${error}`}</div>)}

            <Grid container spacing={15}>
                {toDos && (toDos as IToDoProps[]).map((toDo: IToDoProps) => (
                    <Grid item xl={3}>
                        <ToDoCard header={toDo.header} body={toDo.body} date={toDo.date}/>
                    </Grid>
                ))}
            </Grid>
        </Container>
    );
}
