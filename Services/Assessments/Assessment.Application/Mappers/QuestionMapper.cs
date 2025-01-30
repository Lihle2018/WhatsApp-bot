using Assessments.Application.DTOs;
using Assessments.Domain.Entities;

namespace Assessments.Application.Mappers
{
    public static class QuestionMapper
    {
        public static QuestionDto ToDto(this Question question)
        {
            if (question == null)
                return null;

            return new QuestionDto
            {
                Id = question.Id,
                Text = question.Text,
                Trait = question.Trait,
                Difficulty = question.Difficulty,
                Topic = question.Topic,
                Options = question.Options?.Select(o => o.ToDto()).ToList()
            };
        }

        public static List<QuestionDto> ToDto(this IEnumerable<Question> questions)
        {
            return questions?.Select(q => q.ToDto()).ToList();
        }
    }
}
