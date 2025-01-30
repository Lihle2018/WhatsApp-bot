using Assessments.Application.DTOs;

namespace Assessments.Application.Services
{
    public interface IAssessmentResultGenerator
    {
        Task<string> GenerateAssessmentResult(AssessmentResultDto assessment);
    }
}
