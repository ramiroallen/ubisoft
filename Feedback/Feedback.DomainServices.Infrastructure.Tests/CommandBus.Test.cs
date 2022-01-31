using Feedback.DomainServices.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Infrastructure.Tests
{
    [TestClass]
    public class CommandBus
    {
        Infrastructure.CommandBus commandBus;
        Mock<IServiceProvider> providerMock;

        [TestInitialize]
        public void Initialize()
        {
            providerMock = new Mock<IServiceProvider>();
            commandBus = new Infrastructure.CommandBus(providerMock.Object);
        }

        [TestMethod]
        public async Task TestSendAsync()
        {
            var command = new Commands.CreateUpdateFeedbackCommand();
            var commandHandlerMock = new Mock<ICommandHandler<Commands.CreateUpdateFeedbackCommand>> ();
            providerMock.Setup(p => p.GetService(It.IsAny<Type>())).Returns(commandHandlerMock.Object);
            await commandBus.SendAsync(command);

            providerMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Once());
            commandHandlerMock.Verify(m => m.HandleAsync(command), Times.Once);
        }

        [TestMethod]
        public async Task ShouldThrowAnException()
        {
            providerMock.Setup(p => p.GetService(It.IsAny<Type>())).Throws(new Exception());
            await Assert.ThrowsExceptionAsync<Exception>(async () => await commandBus.SendAsync(new Commands.CreateUpdateFeedbackCommand()));
        }
    }
}
