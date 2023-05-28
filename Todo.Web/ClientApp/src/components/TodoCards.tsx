import React, { MouseEvent } from "react";
import {useEffect, useState} from "react";
import {Container, Grid} from "@mui/material";
import TodoCard from "./TodoCard";
import axios from "axios"
import TodoAddCard from "./TodoAddCard";

type TodoItem = {
    id: number,
    header: string,
    body: string,
    date: Date
}

interface TodoCardsProps {
    token: string | null;
}

export default function TodoCards({ token }: TodoCardsProps) {
    const [todoItems, setTodoItems] = useState<Array<TodoItem> | null>(null);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    const handleGetData = async () => {
        try {
            const response = await axios.get('api/todo', {
                headers: {
                    Authorization: `Bearer ${token}`
                }
            });
            setTodoItems(response.data);
            setError(null);
        } catch (err: any) {
            setError(err.message);
            setTodoItems(null);
        } finally {
            setLoading(false);
        }
    }

    const handleAddTodo = async (e: Event, todoItem: { Header: string, Body: string, Date: Date }) => {
        e.preventDefault();

        await axios.post('/api/todo', todoItem, {
            headers: {
                Authorization: `Bearer ${token}`
            }
        })
            .then(() => handleGetData())
            .catch((err) => console.error(err));
    }

    const handleUpdateTodo = async (e: MouseEvent<HTMLButtonElement>, updatedTodoItem: TodoItem) => {
        e.preventDefault();

        await axios.put('api/todo/' + updatedTodoItem.id, updatedTodoItem)
            .then(() => handleGetData())
            .catch((err) => console.error(err));
    }

    const handleRemoveTodo = async (id: number) => {
        await axios.delete("api/todo/" + id)
            .then(() => {
                if (todoItems === null)
                    return
                const filteredTodoItems = todoItems.filter((todo: TodoItem) => id !== todo.id);
                setTodoItems(filteredTodoItems);
            })
            .catch((err) => {
                console.error(err)
            });
    }

    useEffect(() => {
        handleGetData();
    }, []);

    return (
        <Container>
            {loading && <div>Yükleniyor...</div>}
            {error && (<div>{`Hata - ${error}`}</div>)}

            <Grid container rowSpacing={5} columnSpacing={15}>
                {todoItems && (todoItems as TodoItem[]).map((todoItem: TodoItem, index) => (
                    <Grid item xl={3}>
                        <TodoCard key={index} todoItem={todoItem} onUpdateTodo={handleUpdateTodo} onRemoveTodo={handleRemoveTodo}/>
                    </Grid>
                ))}
                {!loading &&
                    <Grid item xl={3}>
                        <TodoAddCard onAddTodo={handleAddTodo} />
                    </Grid>
                }
            </Grid>
        </Container>
    );
}
