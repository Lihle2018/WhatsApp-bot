namespace Assessments.Domain.Entities
{
    /// <summary>
    /// Provides detailed analysis for a specific question based on user responses.
    /// </summary>
    public class QuestionAnalysis
    {
        /// <summary>
        /// Gets the ID of the analyzed question.
        /// </summary>
        public string QuestionId { get; private set; }

        /// <summary>
        /// Gets the feedback generated from the analysis.
        /// </summary>
        public string Feedback { get; private set; }

        /// <summary>
        /// Gets the score assigned based on the response analysis.
        /// </summary>
        public double Score { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionAnalysis"/> class.
        /// </summary>
        /// <param name="questionId">The ID of the question.</param>
        /// <param name="feedback">The feedback from analysis.</param>
        /// <param name="score">The score for the response.</param>
        public QuestionAnalysis(string questionId, string feedback, double score)
        {
            QuestionId = questionId;
            Feedback = feedback;
            Score = score;
        }
    }
}
