using Assessments.Domain.Entities;

namespace Assessments.Application.Services
{
    /// <summary>
    /// Defines a contract for services that generate assessment questions using AI.
    /// </summary>
    public interface IQuestionGenerator
    {
        /// <summary>
        /// Generates a list of questions based on the provided parameters.
        /// </summary>
        /// <param name="trait">The trait being assessed (e.g., "Openness", "Conscientiousness").</param>
        /// <param name="topic">The topic or category of the questions (e.g., "Personality", "Behavior").</param>
        /// <param name="difficulty">The difficulty level for the questions.</param>
        /// <param name="count">The number of questions to generate.</param>
        /// <returns>A list of generated questions.</returns>
        Task<IEnumerable<Question>> GenerateQuestionsAsync(string trait, string topic, int difficulty, int count);

    }
}
