import * as React from "react";
import {useEffect, useState} from "react";
import {Container, Grid} from "@mui/material";
import TodoCard from "./TodoCard";
import axios from "axios"

type TodoItem = {
    id: number,
    header: string,
    body: string,
    date: Date
}

export default function TodoCards() {
    const [todoItems, setTodoItems] = useState<Array<TodoItem> | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const getData = async () => {
            try {
                const response = await axios.get('todo');

                setTodoItems(response.data);
                setError(null);
            } catch (err: any) {
                setError(err.message);
                setTodoItems(null);
            } finally {
                setLoading(false);
            }
        }

        getData();
    }, []);

    const handleRemoveTodo = async (id: number) => {
        await axios.delete("todo/" + id)
            .then(() => {
                if (todoItems === null)
                    return
                const filteredTodoItems = todoItems.filter((todo: TodoItem) => id !== todo.id);
                setTodoItems(filteredTodoItems);
            })
    }

    return (
        <Container>
            {loading && <div>Yükleniyor...</div>}
            {error && (<div>{`Hata - ${error}`}</div>)}

            <Grid container spacing={15}>
                {todoItems && (todoItems as TodoItem[]).map((todoItem: TodoItem) => (
                    <Grid item xl={3}>
                        <TodoCard todoItem={todoItem} onRemoveTodo={handleRemoveTodo}/>
                    </Grid>
                ))}
            </Grid>
        </Container>
    );
}
