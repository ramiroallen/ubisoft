using Feedback.DomainServices.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.DomainServices.Infrastructure.Tests
{
    [TestClass]
    public class Dispatcher
    {
        Infrastructure.Dispatcher dispatcher;
        Mock<IServiceProvider> providerMock;
        Queries.GetLatestFeedbackQuery query;

        [TestInitialize]
        public void Initialize()
        {
            providerMock = new Mock<IServiceProvider>();
            dispatcher = new Infrastructure.Dispatcher(providerMock.Object);
            query = new Queries.GetLatestFeedbackQuery();
        }

        [TestMethod]
        public async Task TestSendAsync()
        {
            var commandQueryHandlerMock = new Mock<ICommandQueryHandler<Models.UserFeedback,Queries.GetLatestFeedbackQuery>>();
            providerMock.Setup(p => p.GetService(It.IsAny<Type>())).Returns(commandQueryHandlerMock.Object);
            await dispatcher.DispatchQuery<Models.UserFeedback, Queries.GetLatestFeedbackQuery>(query);

            providerMock.Verify(m => m.GetService(It.IsAny<Type>()), Times.Once());
            commandQueryHandlerMock.Verify(m => m.HandleAsync(query), Times.Once);
        }

        [TestMethod]
        public async Task ShouldThrowAnException()
        {
            providerMock.Setup(p => p.GetService(It.IsAny<Type>())).Throws(new Exception());
            await Assert.ThrowsExceptionAsync<Exception>(async () => await dispatcher.DispatchQuery<Models.UserFeedback, Queries.GetLatestFeedbackQuery>(query));
        }
    }
}
