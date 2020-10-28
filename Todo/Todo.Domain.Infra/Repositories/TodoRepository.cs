using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Queries;
using Todo.Domain.Repositories.Contracts;

namespace Todo.Domain.Infra.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly TodoContext _context;
        
        public TodoRepository(TodoContext context)
        {
            _context = context;
        }
        public void Create(TodoItem todo)
        {
            _context.Todo.Add(todo);
            _context.SaveChanges();
        }

        public IEnumerable<TodoItem> GetAll(string user)
        {
            return _context
                .Todo.AsNoTracking().Where(TodoQueries.GetAll(user)).OrderBy(x=> x.Date);   
        }

        public IEnumerable<TodoItem> GetAllDone(string user)
        {
             return _context
                .Todo.AsNoTracking().Where(TodoQueries.GetAllDone(user)).OrderBy(x=> x.Date);   

        }

        public IEnumerable<TodoItem> GetAllUndone(string user)
        {
            return _context
                .Todo.AsNoTracking().Where(TodoQueries.GetAllUndone(user)).OrderBy(x=> x.Date);   

        }

        public TodoItem GetById(Guid id, string user)
        {
            return _context
                .Todo.AsNoTracking().FirstOrDefault(x => x.Id == id && x.User == user);
        }

        public IEnumerable<TodoItem> GetByPeriod(string user, DateTime date, bool done)
        {
            return _context
                .Todo.AsNoTracking().Where(TodoQueries.GetByPeriod(user, date, done)).OrderBy(x=> x.Date);   

        }

        public void Update(TodoItem todo)
        {
            _context.Entry(todo).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}