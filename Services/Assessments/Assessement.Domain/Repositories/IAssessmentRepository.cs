using Assessments.Domain.Entities;

namespace Assessments.Domain.Repositories
{
    public interface IAssessmentRepository
    {
        Task<Assessment> CreateAssessmentAsync(Assessment assessment);
        Task<Assessment> GetAssessment(string phoneNumber);
        Task<Assessment> GetAssessmentById(string assessmentId);
        Task<Assessment> UpdateAssessmentAsync(Assessment assessment);
    }
}
