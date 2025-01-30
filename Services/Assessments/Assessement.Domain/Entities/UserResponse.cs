using BuildingBlocks.BaseEntities;

namespace Assessments.Domain.Entities
{
    /// <summary>
    /// Represents a user's response to a specific question in the assessment.
    /// </summary>
    public class UserResponse : BaseEntity
    {
        /// <summary>
        /// Gets the ID of the related question.
        /// </summary>
        public string QuestionId { get; private set; }

        /// <summary>
        /// Gets the user's answer to the question.
        /// </summary>
        public string Answer { get; private set; }

        /// <summary>
        /// Gets the confidence score for the response (useful for AI-derived answers).
        /// </summary>
        public double ConfidenceScore { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserResponse"/> class.
        /// </summary>
        /// <param name="questionId">The ID of the question.</param>
        /// <param name="answer">The user's answer.</param>
        /// <param name="confidenceScore">The confidence score of the response.</param>
        public UserResponse(string questionId, string answer, double confidenceScore = 1.0)
        {
            QuestionId = questionId;
            Answer = answer;
            ConfidenceScore = confidenceScore;
        }
    }
}
