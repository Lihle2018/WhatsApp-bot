using Assessments.Application.DTOs;
using Assessments.Application.Mappers;
using Assessments.Application.Services;
using Assessments.Domain.Repositories;
using MediatR;
using Serilog;

namespace Assessments.Application.UseCases.GenerateQuestions
{
    public class GenerateQuestionsCommandHandler(IQuestionGenerator questionGenerator, IQuestionRepository questionRepository) : IRequestHandler<GenerateQuestionsCommand, IEnumerable<QuestionDto>> 
    { 
        public async Task<IEnumerable<QuestionDto>> Handle(GenerateQuestionsCommand request, CancellationToken cancellationToken)
        {
            Log.Information("Generating {Count} questions for topic '{Topic}' and trait '{Trait}' with difficulty {Difficulty}.", request.Count, request.Topic, request.Trait, request.Difficulty);
            var generatedQuestions =await questionGenerator.GenerateQuestionsAsync(request.Trait,request.Topic,request.Difficulty,request.Count);
            _=await questionRepository.CreateQuestions(generatedQuestions);
            return generatedQuestions.ToDto();
        }
    }

}
