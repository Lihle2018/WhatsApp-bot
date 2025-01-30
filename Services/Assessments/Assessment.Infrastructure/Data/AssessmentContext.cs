
using Assessments.Domain.Entities;
using Assessments.Infrastructure.Data.Interfaces;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Assessments.Infrastructure.Data
{
    public class AssessmentContext : IAssessmentsContext
    {
        public AssessmentContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetSection("DatabaseSettings:ConnectionString").Value);
            var database = client.GetDatabase(configuration.GetSection("DatabaseSettings:DatabaseName").Value);
            Assessments = database.GetCollection<Assessment>(configuration.GetSection("DatabaseSettings:AssessmentCollectionName").Value);
            AssessmentResults = database.GetCollection<AssessmentResult>(configuration.GetSection("DatabaseSettings:AssessmentResultCollectionName").Value);
            Questions = database.GetCollection<Question>(configuration.GetSection("DatabaseSettings:QuestionCollectionName").Value);
        }
        public IMongoCollection<Assessment> Assessments { get; }
        public IMongoCollection<AssessmentResult> AssessmentResults { get; }
        public IMongoCollection<Question> Questions { get; }
    }
}
