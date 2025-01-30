
using Assessments.Application.DTOs;
using Assessments.Application.Mappers;
using Assessments.Domain.Repositories;
using MediatR;

namespace Assessments.Application.UseCases.GetQuestions
{
    public class GetQuestionsHandler(IQuestionRepository questionRepository) : IRequestHandler<GetQuestionsQuery, IEnumerable<QuestionDto>>
    {
        public async Task<IEnumerable<QuestionDto>> Handle(GetQuestionsQuery request, CancellationToken cancellationToken)
        {
            var questions =await questionRepository.GetAllQuestionsAsync();
            return questions.ToDto();
        }
    }
}
