using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Feedback.Repository.Tests
{
    [TestClass]
    public class UnitOfWork
    {
        static Repository.UnitOfWork unitOfWork;
        static Infrastructure.Context.FeedbackContext context;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<Infrastructure.Context.FeedbackContext>()
            .UseInMemoryDatabase(databaseName: "Feedback")
            .Options;
            new Infrastructure.Context.FeedbackContext(options);
            context = new Infrastructure.Context.FeedbackContext(options);
            unitOfWork = new Repository.UnitOfWork(context);
        }

        [TestMethod]
        public async Task TestSaveChanges()
        {
            var userFeedback = new Models.UserFeedback
            {
                Comments = "test comment",
                CreatedDate = DateTime.UtcNow,
                Rate = 5,
                SessionId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Id = 1
            };
            var previousCount = await context.UserFeedbacks.CountAsync();
            context.UserFeedbacks.Add(userFeedback);
            unitOfWork.SaveChanges();
            var afterCount = await context.UserFeedbacks.CountAsync();
            Assert.IsTrue(previousCount + 1 == afterCount);
        }
    }
}
