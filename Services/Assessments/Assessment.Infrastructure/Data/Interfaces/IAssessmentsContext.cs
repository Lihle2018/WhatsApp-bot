using Assessments.Domain.Entities;
using MongoDB.Driver;

namespace Assessments.Infrastructure.Data.Interfaces
{
    public interface IAssessmentsContext
    {
        IMongoCollection<Assessment> Assessments { get; }
        IMongoCollection<Question> Questions { get; }
        IMongoCollection<AssessmentResult> AssessmentResults { get; }
    }
}
