
using BuildingBlocks.BaseEntities;

namespace Assessments.Domain.Entities
{
    /// <summary>
    /// Represents an instance of an assessment taken by a user.
    /// </summary>
    public class Assessment : BaseEntity
    {

        /// <summary>
        /// Gets the ID of the user who is taking the assessment.
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// Gets the Phone number of the user who is taking the assessment.
        /// </summary>
        public string PhoneNumber { get; private set; }

        /// <summary>
        /// Gets the list of user responses for the assessment.
        /// </summary>
        /// 
        public List<UserResponse> Responses { get; private set; } = new();
        /// <summary>
        /// Gets current question for the assessment.
        /// </summary>
        public Question CurrentQuestion { get; private set; }

        /// <summary>
        /// Gets the date and time when the assessment was started.
        /// </summary>
        public DateTime StartedAt { get; private set; }

        /// <summary>
        /// Gets the date and time when the assessment was completed.
        /// </summary>
        public DateTime? CompletedAt { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Assessment"/> class.
        /// </summary>
        /// <param name="templateId">The ID of the assessment template.</param>
        /// <param name="userId">The ID of the user taking the assessment.</param>
        public Assessment(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
            StartedAt = DateTime.UtcNow;
        }

        /// <summary>
        /// Marks the assessment as completed.
        /// </summary>
        public void MarkAsComplete()
        {
            CompletedAt = DateTime.UtcNow;
            CurrentQuestion =null;
        }

        /// <summary>
        /// Adds a response to the assessment.
        /// </summary>
        /// <param name="response">The user response to add.</param>
        public void AddResponse(UserResponse response)
        {
            Responses.Add(response);
        }


        /// <summary>
        /// Adds a response to the assessment.
        /// </summary>
        /// <param name="question">The user response to add.</param>
        public void SetCurrentQuestion(Question question)
        {
            CurrentQuestion = question;
        }
    }
}
