namespace Assessments.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for Question.
    /// </summary>
    public class QuestionDto
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Trait { get; set; }
        public int Difficulty { get; set; }
        public string Topic { get; set; }
        public List<OptionDto> Options { get; set; }
    }

    /// <summary>
    /// Data Transfer Object for Option.
    /// </summary>
    public class OptionDto
    {
        public string Text { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }

        public OptionDto(string text,int order, bool isCorrect)
        {
            Text = text;
            IsCorrect = isCorrect;
            Order = order;
        }
    }
}
