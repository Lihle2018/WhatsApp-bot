namespace Assessments.Domain.Entities
{
    /// <summary>
    /// Represents the score for a specific trait in the assessment.
    /// </summary>
    public class AssessmentTraitScore
    {
        /// <summary>
        /// Gets the name of the trait (e.g., "Openness").
        /// </summary>
        public string TraitName { get; private set; }

        /// <summary>
        /// Gets the score for the trait.
        /// </summary>
        public double Score { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AssessmentTraitScore"/> class.
        /// </summary>
        /// <param name="traitName">The name of the trait.</param>
        /// <param name="score">The score for the trait.</param>
        public AssessmentTraitScore(string traitName, double score)
        {
            TraitName = traitName;
            Score = score;
        }
    }
}
