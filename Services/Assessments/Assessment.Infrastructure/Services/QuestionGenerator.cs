using System.Text.Json;
using System.Text;
using Assessments.Domain.Entities;
using Assessments.Domain.Enums;
using Assessments.Domain.ValueObjects;
using Assessments.Application.Services;
using Assessments.Infrastructure.Services.Models;

namespace Assessments.Infrastructure.Services;

/// <summary>
/// Implementation of the IQuestionGenerator interface using OpenAI API.
/// </summary>
public class OpenAIQuestionGenerator : IQuestionGenerator
{
    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly string _baseUrl;

    public OpenAIQuestionGenerator(HttpClient httpClient, string apiKey,string baseUrl)
    {
        _httpClient = httpClient;
        _apiKey = apiKey;
        _baseUrl = baseUrl;
    }

    public async Task<IEnumerable<Question>> GenerateQuestionsAsync(string trait, string topic, int difficulty, int count)
    {
        var prompt = BuildPrompt(trait, topic, difficulty, count);
        var openAiResponse = await CallOpenAiApiAsync(prompt);
        if (string.IsNullOrEmpty(openAiResponse.Choices.FirstOrDefault().Message.Content))
            throw new InvalidOperationException("Failed to generate a valid question from OpenAI API.");
        return ExtractQuestions(openAiResponse);
    }


    private async Task<OpenAiResponse> CallOpenAiApiAsync(string prompt)
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

    private string BuildPrompt(string trait, string topic, int difficulty, int count)
    {
        return $@"
        You are an AI specializing in psychometric assessment question generation.
        Generate {count} multiple-choice questions related to the topic '{topic}' that assess the trait '{trait}'.
        Each question should be appropriate for a difficulty level of {difficulty} on a scale from 1 (easy) to 10 (very difficult).
        For each question, provide the following details:
        - The question text.
        - The difficulty level (already provided in this request as {difficulty}).
        - The discrimination value: how well the question differentiates between different levels of the trait (a value between 0.5 and 2.5, higher indicates better differentiation).
        - The guessing parameter: the probability of guessing the correct answer for multiple-choice questions (a value between 0 and 0.25).
        - The trait being assessed (already provided in this request as '{trait}').
        - The topic or category (already provided in this request as '{topic}').
        - Whether the question is part of an adaptive assessment (set this to false for now).
        Provide four answer options for each question, clearly indicating which one is correct.
        Format the response as follows:
        ---
        Question: <The question text>
        Difficulty: <The difficulty level>
        Discrimination: <The discrimination value>
        Guessing: <The guessing parameter>
        Trait: <The trait being assessed>
        Topic: <The topic or category>
        IsAdaptive: <true/false>
        Options:
        1. <Option 1>
        2. <Option 2>
        3. <Option 3>
        4. <Option 4>
        Correct Answer: <The correct option number>
        ---
        Ensure all fields are populated for each question.";
    }


    private List<Question> ExtractQuestions(OpenAiResponse response)
    {
        var responseText = response?.Choices?.FirstOrDefault()?.Message?.Content?.Trim();
        if (string.IsNullOrEmpty(responseText))
            throw new InvalidOperationException("Failed to parse questions from OpenAI response.");

        var questionBlocks = responseText.Split("---", StringSplitOptions.RemoveEmptyEntries);
        var questions = new List<Question>();

        foreach (var block in questionBlocks)
        {
            var lines = block.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            string questionText = null;
            string trait = null;
            string topic = null;
            int difficulty = 0;
            List<Option> optionList = new();
            int correctIndex = -1;
            int order = 1;
            foreach (var line in lines)
            {
                if (line.StartsWith("Question:"))
                    questionText = line.Substring(9).Trim();

                if (line.StartsWith("Trait:"))
                    trait = line.Substring(6).Trim();

                if (line.StartsWith("Topic:"))
                    topic = line.Substring(6).Trim();

                if (line.StartsWith("Difficulty:"))
                    difficulty = int.Parse(line.Substring(11).Trim());

                if (line.StartsWith("1. ") || line.StartsWith("2. ") || line.StartsWith("3. ") || line.StartsWith("4. "))
                {
                    optionList.Add(new Option(line.Substring(3).Trim(), order));
                    order++; 
                }

                if (line.StartsWith("Correct Answer:"))
                {
                    var correctAnswerText = line.Split(':')[1].Trim();
                    if (int.TryParse(correctAnswerText, out var index))
                    {
                        correctIndex = index - 1; // Convert to zero-based index
                    }
                }
            }

            if (string.IsNullOrEmpty(questionText) || string.IsNullOrEmpty(trait) ||
                string.IsNullOrEmpty(topic) || optionList.Count != 4 || correctIndex < 0)
            {
                throw new InvalidOperationException("Invalid question block in OpenAI response.");
            }

            // Mark the correct option
            for (int i = 0; i < optionList.Count; i++)
            {
                if (i == correctIndex)
                {
                    var option = optionList[i];
                    optionList[i] = new Option(optionList[i].Text, option.Order, isCorrect: true);
                }
                   
            }

            // Create the Question object
            var question = new Question(
                type: AssessmentTypes.MultipleChoice,
                text: questionText,
                trait: trait,
                difficulty: difficulty,
                discrimination: 0.8,
                guessing: 0.2,
                topic: topic,
                isAdaptive: true
            );

            question.AddOptions(optionList);
            questions.Add(question);
        }

        return questions;
    }
}
