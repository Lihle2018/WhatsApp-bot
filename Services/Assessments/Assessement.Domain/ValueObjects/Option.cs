namespace Assessments.Domain.ValueObjects
{
    /// <summary>
    /// Represents an option for a multiple-choice question.
    /// </summary>
    public class Option
    {
        /// <summary>
        /// Gets the text of the option.
        /// </summary>
        public string Text { get; private set; }


        /// <summary>
        /// Gets the order of the option.
        /// </summary>
        public int Order { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the option is correct.
        /// </summary>
        public bool IsCorrect { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Option"/> class.
        /// </summary>
        /// <param name="text">The text of the option.</param>
        /// <param name="order">The order of the option.</param>
        /// <param name="isCorrect">Indicates whether the option is correct.</param>
        public Option(string text,int order, bool isCorrect = false)
        {
            Text = text;
            Order = order;
            IsCorrect = isCorrect;
        }
    }
}
