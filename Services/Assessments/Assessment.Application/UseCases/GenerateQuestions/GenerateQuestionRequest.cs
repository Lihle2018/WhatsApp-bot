namespace Assessments.Application.UseCases.GenerateQuestions
{
    public class GenerateQuestionRequest
    {
        public string Trait { get; set; }
        public string Topic { get; set; }
        public int Difficulty { get; set; }
        public int Count { get; set; }
    }
}
