using Assessments.Application.DTOs;
using Assessments.Application.Mappers;
using Assessments.Application.UseCases.TakeAssessment;
using Assessments.Domain.Entities;
using Assessments.Domain.Repositories;
using MediatR;

namespace Assessments.Application.UseCases.TakeWhatsAppAssessment
{
    public class TakeWhatsAppAssessmentCommandHandler(IQuestionRepository questionRepository,IAssessmentRepository assessmentRepository):IRequestHandler<TakeWhatsAppAssessmentCommand,QuestionDto>
    {
        public async Task<QuestionDto> Handle(TakeWhatsAppAssessmentCommand request, CancellationToken cancellationToken)
        {
            if (request.message.ToLower().Contains("start"))
            {
                var firstQuestion = await questionRepository.GetFirstQuestion();
                var assessment = new Assessment(request.sender);
                assessment.SetCurrentQuestion(firstQuestion);
                _ = await assessmentRepository.CreateAssessmentAsync(assessment);
                return firstQuestion.ToDto();
            }
            else
            {
                var assessment = await assessmentRepository.GetAssessment(request.sender);
                if(assessment==null||assessment.CurrentQuestion == null)
                {
                    return null;
                }
                var nextQuestion = await GetNextQuestionAsync(assessment, int.Parse(request.message));
                return nextQuestion;
            }
        }
        private async Task<QuestionDto> GetNextQuestionAsync(Assessment assessment, int order)
        {
            var currentQuestion = assessment.CurrentQuestion;
            var answer = currentQuestion.Options.FirstOrDefault(x => x.Order == order).Text;
            var userResponse = new UserResponse(currentQuestion.Id, answer);
            var isCorrect = currentQuestion.EvaluateAnswer(userResponse);
            assessment.AddResponse(userResponse);
            var nextQuestion = await GetAdaptiveNextQuestionAsync(currentQuestion, isCorrect,assessment.Responses);
            assessment.SetCurrentQuestion(nextQuestion);
            await assessmentRepository.UpdateAssessmentAsync(assessment);
            return nextQuestion.ToDto();
        }

        private async Task<Question> GetAdaptiveNextQuestionAsync(Question currentQuestion, bool isCorrect, IEnumerable<UserResponse> userResponses)
        {
            var targetDifficulty = isCorrect
                ? currentQuestion.Difficulty + 1
                : currentQuestion.Difficulty - 1;

            targetDifficulty = Math.Max(1, targetDifficulty);

            var potentialQuestions = await questionRepository.GetQuestionsByDifficulty(targetDifficulty);

            var answeredQuestionIds = userResponses.Select(response => response.QuestionId).ToHashSet();

            var filteredQuestions = potentialQuestions
                .Where(q => q.Trait == currentQuestion.Trait
                            && q.Topic == currentQuestion.Topic
                            && !answeredQuestionIds.Contains(q.Id))
                .ToList();

            return filteredQuestions.Any()
                ? filteredQuestions[new Random().Next(filteredQuestions.Count)]
                : throw new InvalidOperationException("No suitable next question found.");
        }

    }
}
