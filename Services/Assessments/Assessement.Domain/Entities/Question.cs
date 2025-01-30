using Assessments.Domain.Enums;
using Assessments.Domain.ValueObjects;
using BuildingBlocks.BaseEntities;

namespace Assessments.Domain.Entities;

/// <summary>
/// Represents a question in the assessment, including properties for Item Response Theory (IRT) and AI integration.
/// </summary>
public class Question : BaseEntity
{
    /// <summary>
    /// Gets the type of the question (e.g., MultipleChoice, OpenEnded).
    /// </summary>
    public AssessmentTypes Type { get; private set; }

    /// <summary>
    /// Gets the text of the question.
    /// </summary>
    public string Text { get; private set; }

    /// <summary>
    /// Gets the trait being assessed (e.g., "Openness", "Conscientiousness").
    /// </summary>
    public string Trait { get; private set; }

    /// <summary>
    /// Gets the difficulty level of the question. Higher values indicate more difficult questions.
    /// </summary>
    public int Difficulty { get; private set; }

    /// <summary>
    /// Gets the discrimination value, indicating how well the question differentiates between different levels of the trait.
    /// </summary>
    public double Discrimination { get; private set; }

    /// <summary>
    /// Gets the guessing parameter, representing the probability of guessing the correct answer (useful for multiple-choice questions).
    /// </summary>
    public double Guessing { get; private set; }

    /// <summary>
    /// Gets the topic or category of the question (e.g., "Personality", "Behavior").
    /// </summary>
    public string Topic { get; private set; }

    /// <summary>
    /// Gets a value indicating whether the question is part of an adaptive assessment.
    /// </summary>
    public bool IsAdaptive { get; private set; }

    /// <summary>
    /// Gets the list of options for multiple-choice questions.
    /// </summary>
    public List<Option> Options { get; private set; } = new();

    /// <summary>
    /// Initializes a new instance of the <see cref="Question"/> class.
    /// </summary>
    /// <param name="type">The type of the question.</param>
    /// <param name="text">The text of the question.</param>
    /// <param name="trait">The trait being assessed.</param>
    /// <param name="difficulty">The difficulty level of the question.</param>
    /// <param name="discrimination">The discrimination parameter for the question.</param>
    /// <param name="guessing">The guessing parameter for the question.</param>
    /// <param name="topic">The topic or category of the question.</param>
    /// <param name="isAdaptive">Indicates if the question is part of an adaptive assessment.</param>
    public Question(
        AssessmentTypes type,
        string text,
        string trait,
        int difficulty,
        double discrimination,
        double guessing,
        string topic,
        bool isAdaptive)
    {
        Type = type;
        Text = text;
        Trait = trait;
        Difficulty = difficulty;
        Discrimination = discrimination;
        Guessing = guessing;
        Topic = topic;
        IsAdaptive = isAdaptive;
    }

    /// <summary>
    /// Adds an option to the list of options for the question (useful for multiple-choice questions).
    /// </summary>
    /// <param name="text">The text of the option.</param>
    /// <param name="isCorrect">Indicates whether the option is correct.</param>
    public void AddOption(string text, bool isCorrect = false)
    {
        Options.Add(new Option(text, Options.Count()+1, isCorrect));
    }
    public void AddOptions(List<Option> options)
    {
        Options.AddRange(options);
    }

    public bool EvaluateAnswer(UserResponse response)
    {
        return Options.FirstOrDefault(x => x.Text == response.Answer).IsCorrect;
    }
}
