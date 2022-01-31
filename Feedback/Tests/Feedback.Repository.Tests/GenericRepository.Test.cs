using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading.Tasks;

namespace Feedback.Repository.Tests
{
    [TestClass]
    public class GenericRepository
    {
        private GenericRepository<Models.UserFeedback> _feedbackRepository;
        static decimal feedbackId = 11;
        static Guid userId = Guid.NewGuid();
        static Models.UserFeedback userFeedback = new Models.UserFeedback
        {
            Comments = "another test comment",
            CreatedDate = DateTime.UtcNow,
            Rate = 95,
            SessionId = Guid.NewGuid(),
            UserId = userId,
            Id = 12
        };

        [TestInitialize]
        public async Task Initialize()
        {
            var options = new DbContextOptionsBuilder<Infrastructure.Context.FeedbackContext>()
            .UseInMemoryDatabase(databaseName: "Feedback")
            .Options;
            new Infrastructure.Context.FeedbackContext(options);
            var context = new Infrastructure.Context.FeedbackContext(options);
            _feedbackRepository = new GenericRepository<Models.UserFeedback>(context);
           
            if (await context.UserFeedbacks.AnyAsync()) return;

            context.UserFeedbacks.Add(new Models.UserFeedback
            {
                Comments = "test comment",
                CreatedDate = DateTime.UtcNow,
                Rate = 5,
                SessionId = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Id = feedbackId
            });
            context.UserFeedbacks.Add(userFeedback);
            context.SaveChanges();
        }


        [TestMethod]
        public async Task TestFindAll()
        {
            var feedbacks = await _feedbackRepository.FindAll().ToListAsync();

            Assert.IsTrue(feedbacks.Count == 2, "should return all");
        }

        [TestMethod]
        public async Task TestFind()
        {
            var feedbacks = await _feedbackRepository.Find(f => f.Rate == 5, new string[0]).ToListAsync();

            Assert.IsTrue(feedbacks.Count == 1, "should return 1 feedback");
        }

        [TestMethod]
        public async Task TestFindById()
        {
            var feedback = await _feedbackRepository.FindByIdAsync(feedbackId);

            Assert.IsNotNull(feedback, "should find the feedback");
        }

        [TestMethod]
        public async Task TestFindByIdError()
        {
            var retrieveNull = await _feedbackRepository.FindByIdAsync((decimal)0);
            Assert.IsNull(retrieveNull);
        }

        [TestMethod]
        public async Task TestFindFirst()
        {
            var feedback = await _feedbackRepository.FindFirstOrDefaultAsync(f => f.UserId == userId);

            Assert.IsNotNull(feedback, "should return the feedback");
            Assert.IsTrue(feedback.Id == userFeedback.Id);
            Assert.IsTrue(feedback.Rate == userFeedback.Rate);
            Assert.IsTrue(feedback.Comments == userFeedback.Comments);
            Assert.IsTrue(feedback.CreatedDate == userFeedback.CreatedDate);
            Assert.IsTrue(feedback.UserId == userFeedback.UserId);
            Assert.IsTrue(feedback.SessionId == userFeedback.SessionId);
        }
    }
}
