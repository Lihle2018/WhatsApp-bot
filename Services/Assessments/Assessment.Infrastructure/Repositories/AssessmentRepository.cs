using Assessments.Domain.Entities;
using Assessments.Domain.Repositories;
using Assessments.Infrastructure.Data.Interfaces;
using MongoDB.Driver;

namespace Assessments.Infrastructure.Repositories
{
    public class AssessmentRepository(IAssessmentsContext context) : IAssessmentRepository
    {
        public async Task<Assessment> CreateAssessmentAsync(Assessment assessment)
        {
            await context.Assessments.InsertOneAsync(assessment);
            return assessment;
        }

        public async Task<Assessment> GetAssessment(string phoneNumber)
        {
            return await context.Assessments
                                 .Find(x => x.PhoneNumber == phoneNumber)
                                 .SortByDescending(x => x.StartedAt)
                                 .FirstOrDefaultAsync();

        }

        public async Task<Assessment> GetAssessmentById(string assessmentId)
        {
            return await context.Assessments
                                 .Find(x => x.Id == assessmentId)
                                 .FirstOrDefaultAsync();
        }

        public async Task<Assessment> UpdateAssessmentAsync(Assessment assessment)
        {
            if (assessment == null)
            {
                throw new ArgumentNullException(nameof(assessment), "Assessment cannot be null.");
            }
            var filter = Builders<Assessment>.Filter.Eq(x => x.Id, assessment.Id);

            var options = new ReplaceOptions { IsUpsert = false };
            var result = await context.Assessments.ReplaceOneAsync(filter, assessment, options);
            if (result.MatchedCount == 0)
            {
                throw new InvalidOperationException($"No assessment found with ID {assessment.Id}.");
            }
            return assessment;
        }

    }
}
