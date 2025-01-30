

using BuildingBlocks.BaseEntities;

namespace Assessments.Domain.Entities
{
    /// <summary>
    /// Represents the result of an assessment, including analysis and scoring.
    /// </summary>
    public class AssessmentResult : BaseEntity
    {
        /// <summary>
        /// Gets the ID of the completed assessment.
        /// </summary>
        public Guid AssessmentId { get; private set; }

        /// <summary>
        /// Gets the list of scores for each assessed trait.
        /// </summary>
        public List<AssessmentTraitScore> TraitScores { get; private set; } = new();

        /// <summary>
        /// Gets the overall analysis summary.
        /// </summary>
        public string Summary { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentResult"/> class.
        /// </summary>
        /// <param name="assessmentId">The ID of the completed assessment.</param>
        /// <param name="summary">The summary of the analysis.</param>
        public AssessmentResult(Guid assessmentId, string summary)
        {
            AssessmentId = assessmentId;
            Summary = summary;
        }

        /// <summary>
        /// Adds a trait score to the result.
        /// </summary>
        /// <param name="score">The trait score to add.</param>
        public void AddTraitScore(AssessmentTraitScore score)
        {
            TraitScores.Add(score);
        }
    }
}
