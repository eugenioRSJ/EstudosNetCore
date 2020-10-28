using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Entities;
using Todo.Domain.Queries;

namespace Todo.Domain.Tests.QueryTests
{
    [TestClass]
    public class TodoQueriesTests
    {
        private List<TodoItem>  _items;

        public TodoQueriesTests()
        {
            _items = new List<TodoItem>();
            _items.Add(new TodoItem("Tarefa 1", "UsuárioA", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 2", "eugeniorocha", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 3", "UsuárioB", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 4", "eugeniorocha", DateTime.Now));
            _items.Add(new TodoItem("Tarefa 5", "UsuárioA", DateTime.Now));

        }

        [TestMethod]
        public void Dada_a_consulta_deve_retornar_tarefas_apenas_do_usuario_eugeniojunior()
        {
            var result = _items.AsQueryable().Where(TodoQueries.GetAll("eugeniorocha"));
            Assert.AreEqual(2, result.Count());
        }
    }
}