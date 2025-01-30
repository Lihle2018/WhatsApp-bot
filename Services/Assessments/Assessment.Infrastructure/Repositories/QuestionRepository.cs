using Assessments.Domain.Entities;
using Assessments.Domain.Repositories;
using Assessments.Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Assessments.Infrastructure.Repositories
{
    public class QuestionRepository(IAssessmentsContext context) : IQuestionRepository
    {
        public async Task<IEnumerable<Question>> CreateQuestions(IEnumerable<Question> questions)
        {
            await context.Questions.InsertManyAsync(questions);
            return questions;
        }

        public async Task<Question> GetById(string id)
        {
            return await context.Questions.Find(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Question> GetFirstQuestion()
        {
            return await context.Questions.Find(x => true).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsByDifficulty(int targetDifficulty)
        {
            return await context.Questions.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestionsAsync()
        {
            return await context.Questions.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetQuestionsAsync(IEnumerable<string> questionIds)
        {
            var questions = await context.Questions.Find(x => questionIds.Contains(x.Id)).ToListAsync();
            return questions;
        }
    }
}
