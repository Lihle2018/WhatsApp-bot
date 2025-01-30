
namespace Assessments.Application.DTOs
{
    /// <summary>
    /// Represents the data transfer object for the result of an assessment.
    /// </summary>
    public class AssessmentResultDto
    {
        public string Topic { get; set; }
        public string Trait { get; set; }
        public List<Answer> Answers { get; set; } = new();
    }
    public class Answer
    {
        public string Question { get; set; }
        public string selectedAnswer { get; set; }
        public string Options { get; set; }
    }
}
