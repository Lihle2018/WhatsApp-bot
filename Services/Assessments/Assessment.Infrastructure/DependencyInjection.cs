using Assessments.Application.Services;
using Assessments.Domain.Repositories;
using Assessments.Infrastructure.Data;
using Assessments.Infrastructure.Data.Interfaces;
using Assessments.Infrastructure.Repositories;
using Assessments.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Assessments.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAssessmentsContext, AssessmentContext>();
            services.AddScoped<IAssessmentRepository, AssessmentRepository>();
            services.AddScoped<IQuestionRepository, QuestionRepository>();

            services.AddHttpClient<IQuestionGenerator, OpenAIQuestionGenerator>((httpClient) =>
            {
                var apiKey = configuration["OpenAISettings:ApiKey"];
                var baseUrl = configuration["OpenAISettings:BaseUrl"];

                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    throw new InvalidOperationException("OpenAI API key is not configured.");
                }

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    throw new InvalidOperationException("OpenAI base URL is not configured.");
                }

                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                return new OpenAIQuestionGenerator(httpClient, apiKey, baseUrl);
            });

            services.AddHttpClient<IAssessmentResultGenerator, AssessmentResultGenerator>((httpClient) =>
            {
                var apiKey = configuration["OpenAISettings:ApiKey"];
                var baseUrl = configuration["OpenAISettings:BaseUrl"];

                if (string.IsNullOrWhiteSpace(apiKey))
                {
                    throw new InvalidOperationException("OpenAI API key is not configured.");
                }

                if (string.IsNullOrWhiteSpace(baseUrl))
                {
                    throw new InvalidOperationException("OpenAI base URL is not configured.");
                }

                httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

                return new AssessmentResultGenerator(httpClient, apiKey, baseUrl);
            });

            return services;
        }
    }
}
