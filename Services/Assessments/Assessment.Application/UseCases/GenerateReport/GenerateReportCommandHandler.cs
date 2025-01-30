using Assessments.Application.DTOs;
using Assessments.Application.Services;
using Assessments.Domain.Repositories;
using MediatR;

namespace Assessments.Application.UseCases.GenerateReport
{
    public class GenerateReportCommandHandler(IAssessmentRepository assessmentRepository,IQuestionRepository questionRepository, IAssessmentResultGenerator resultGenerator) : IRequestHandler<GenerateReportCommand, string>
    {
        public async Task<string> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
        {
            // Fetch the assessment and its responses
            var assessment = await assessmentRepository.GetAssessment(request.phoneNumber);
            var questionIds = assessment.Responses.Select(x => x.QuestionId).ToList();

            // Fetch the questions related to the responses
            var questions = await questionRepository.GetQuestionsAsync(questionIds);

            // Create the payload
            var payload = new AssessmentResultDto
            {
                // All questions belong to the same topic and trait
                Topic = questions.FirstOrDefault()?.Topic ?? string.Empty,
                Trait = questions.FirstOrDefault()?.Trait ?? string.Empty,
                Answers = assessment.Responses.Select(response =>
                {
                    var question = questions.FirstOrDefault(q => q.Id == response.QuestionId);
                    if (question == null) return null;

                    return new Answer
                    {
                        Question = question.Text,
                        selectedAnswer = response.Answer,
                        Options = string.Join(", ", question.Options.Select(option => option.Text))
                    };
                }).Where(answer => answer != null).ToList()
            };
            var result = await resultGenerator.GenerateAssessmentResult(payload);
            return result;
        }

    }
}
