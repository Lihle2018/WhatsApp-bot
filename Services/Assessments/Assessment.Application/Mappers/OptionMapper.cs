using Assessments.Application.DTOs;
using Assessments.Domain.ValueObjects;

namespace Assessments.Application.Mappers
{
    public static class OptionMapper
    {
        public static OptionDto ToDto(this Option option)
        {
            if (option == null)
                return null;

            return new OptionDto(option.Text,option.Order, option.IsCorrect);
        }

        public static IEnumerable<OptionDto> ToDto(this IEnumerable<Option> questions)
        {
            return questions?.Select(q => q.ToDto()).ToList();
        }

    }
}
