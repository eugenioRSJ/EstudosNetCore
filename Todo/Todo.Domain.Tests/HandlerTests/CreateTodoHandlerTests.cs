using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Todo.Domain.Commands;
using Todo.Domain.Handlers;
using Todo.Domain.Tests.Repositories;

namespace Todo.Domain.Tests.HandlerTests
{
    [TestClass]
    public class CreateTodoHandlerTests
    {
        private readonly CreateTodoCommand _invalidCommand = new CreateTodoCommand("", "", DateTime.Now);
        private readonly CreateTodoCommand _validCommand = new CreateTodoCommand("Titulo da tarefa", "Eugenio Júnior", DateTime.Now);
        private readonly TodoHandler _handler = new TodoHandler(new FakeTodoRepository());

        [TestMethod]
        public void Dado_um_comando_invalido_deve_interrompe()
        {
            var result = (GenericCommandResult)_handler.Handle(_invalidCommand);
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void Dado_um_comando_valido_deve_retornar_a_tarefa()
        {
            var result = (GenericCommandResult)_handler.Handle(_validCommand);
            Assert.AreEqual(result.Success, true);

        }
    }
}