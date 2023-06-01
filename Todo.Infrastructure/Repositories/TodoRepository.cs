﻿using Microsoft.EntityFrameworkCore;
using Todo.Data.Contexts;
using Todo.Data.Models;

namespace Todo.Infrastructure.Repositories;

public interface ITodoRepository
{
    public IEnumerable<TodoItem> GetAll(int userId);
    public TodoItem? GetById(int id);
    public bool Add(TodoItem todoItem);
    public bool Update(TodoItem todoItem);
    public bool Delete(int id);
    public bool DeleteAllByUserId(int userId);
}

public class TodoRepository : ITodoRepository
{
    private readonly TodoDbContext _todoDbContext;

    public TodoRepository(TodoDbContext todoDbContext)
    {
        _todoDbContext = todoDbContext;
    }

    public IEnumerable<TodoItem> GetAll(int userId)
    {
        var result = _todoDbContext.Set<TodoItem>().Where(todo => todo.userId == userId).ToList();

        return result;
    }

    public TodoItem? GetById(int id)
    {
        var result = _todoDbContext.Set<TodoItem>().FirstOrDefault(todo => todo.Id == id);


        return result;
    }

    public bool Add(TodoItem todoItem)
    { 
        try
        {
            _todoDbContext.Set<TodoItem>().Add(todoItem);
            _todoDbContext.SaveChanges();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public bool Update(TodoItem todoItem)
    {
        var existingTodoItem = _todoDbContext.TodoItem?.FirstOrDefault(t => t.Id == todoItem.Id);

        if (existingTodoItem == null)
        {
            return false;
        }

        existingTodoItem.Header = todoItem.Header ?? existingTodoItem.Header;
        existingTodoItem.Body = todoItem.Body ?? existingTodoItem.Body;

        _todoDbContext.SaveChanges();

        return true;
    }

    public bool Delete(int id)
    {
        var todo = _todoDbContext.Set<TodoItem>().Find(id);

        if (todo == null)
            return false;

        _todoDbContext.Set<TodoItem>().Remove(todo);
        _todoDbContext.SaveChanges();

        return true;
    }

    public bool DeleteAllByUserId(int userId)
    {
        var recordsToDelete = _todoDbContext.Set<TodoItem>().Where(t => t.userId == userId);

        if (recordsToDelete == null)
            return false;

        _todoDbContext.TodoItem?.RemoveRange(recordsToDelete);
        _todoDbContext.SaveChanges();

        return true;
    }
}
