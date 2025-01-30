using Assessments.Application.DTOs;
using Assessments.Application.Services;
using System.Text;
using Assessments.Infrastructure.Services.Models;
using System.Text.Json;

namespace Assessments.Infrastructure.Services
{
    public class AssessmentResultGenerator : IAssessmentResultGenerator
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public AssessmentResultGenerator(HttpClient httpClient, string apiKey, string baseUrl)
        {
            _httpClient = httpClient;
            _apiKey = apiKey;
            _baseUrl = baseUrl;
        }
        public async Task<string> GenerateAssessmentResult(AssessmentResultDto assessment)
        {
            var prompt = CreateOpenAIPrompt(assessment);
            var openAiResponse = await CallOpenAIAsync(prompt);
            return openAiResponse.Choices[0].Message.Content;


        }
        private string CreateOpenAIPrompt(AssessmentResultDto assessment)
        {
            var prompt = $"You are an expert in evaluating assessments. Please evaluate the following responses for the topic '{assessment.Topic}' and the trait '{assessment.Trait}'.\n\n";

            foreach (var answer in assessment.Answers)
            {
                prompt += $"Question: {answer.Question}\n";
                prompt += $"Selected Answer: {answer.selectedAnswer}\n";
                prompt += $"Options: {answer.Options}\n\n";
            }

            prompt += "Based on the overall responses, provide a concise evaluation focused on the trait and topic. Your evaluation should include:\n";
            prompt += "- Include insights into how the answers align with the given topic and trait.\n";
            prompt += "- Offer any suggestions for improvement or strengths that should be highlighted.\n";
            prompt += "Keep feedback professional, focused, and ensure your response is less than 500 characters.";



            return prompt;
        }

        private async Task<OpenAiResponse> CallOpenAIAsync(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-4-turbo",
                messages = new[]
     {
        new { role = "user", content = prompt }
    },
                temperature = 0.7,
                max_tokens = 2000
            };

            var requestContent = new StringContent(
                                                 JsonSerializer.Serialize(requestBody, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase }),
                                                 Encoding.UTF8,
                                                 "application/json"
                                             );

            using var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl)
            {
                Headers = { { "Authorization", $"Bearer {_apiKey}" } },
                Content = requestContent
            };

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<OpenAiResponse>(responseContent, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        }
    }
}

