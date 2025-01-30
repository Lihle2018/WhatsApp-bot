using Assessments.Domain.Entities;
namespace Assessments.Domain.Repositories
{
    public interface IQuestionRepository
    {
        Task<Question> GetById(string id);
        Task<Question> GetFirstQuestion();
        Task<IEnumerable<Question>> GetQuestionsByDifficulty(int targetDifficulty);
        Task<IEnumerable<Question>> CreateQuestions(IEnumerable<Question> questions);
        Task<IEnumerable<Question>> GetAllQuestionsAsync();
        Task<IEnumerable<Question>> GetQuestionsAsync(IEnumerable<string> questionIds);
    }
}
