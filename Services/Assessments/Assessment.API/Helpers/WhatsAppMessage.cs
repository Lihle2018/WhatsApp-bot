using Assessments.Application.DTOs;
using System.Text;

namespace Assessments.API.Helpers
{
    public class WhatsAppMessage
    {
        public static string BuildQuestionMessage(QuestionDto questionDto)
        {
            var messageBuilder = new StringBuilder();

            messageBuilder.AppendLine(questionDto.Text);
            messageBuilder.AppendLine();

            for (int i = 0; i < questionDto.Options.Count; i++)
            {
                var option = questionDto.Options[i];
                messageBuilder.AppendLine($"{i + 1}. {option.Text}");
            }

            return messageBuilder.ToString();
        }
    }
}
