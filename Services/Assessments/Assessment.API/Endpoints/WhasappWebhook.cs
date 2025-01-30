using Assessments.API.Helpers;
using Assessments.Application.UseCases.GenerateReport;
using Assessments.Application.UseCases.TakeAssessment;
using Assessments.Application.UseCases.TakeWhatsAppAssessment;
using Carter;
using MediatR;
using Twilio.Clients;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
namespace Assessments.API.Endpoints
{
    public class WhasappWebhook: ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/api/whatsapp-webhook", async (HttpContext context, ISender sender, IConfiguration configuration) =>
            {
                var form = await context.Request.ReadFormAsync();
                var messageBody = form["Body"].ToString().Trim();
                var senderNumber = form["From"].ToString().Trim();

                1var twilioAccountSid = configuration["Twilio:AccountSid"];
                var twilioAuthToken = configuration["Twilio:AuthToken"];
                var twilioWhatsAppNumber = configuration["Twilio:WhatsAppFromNumber"];
                var client = new TwilioRestClient(twilioAccountSid, twilioAuthToken);

                string message;

                if (messageBody.Equals("start", StringComparison.OrdinalIgnoreCase))
                {
                    // Start the assessment
                    var firstQuestion = await sender.Send(new TakeWhatsAppAssessmentCommand(senderNumber, "start"));
                    message = "Welcome to the Adaptive Assessment! 🎉\n\nHere's how it works:\n" +
                              "1️⃣ To begin the assessment, reply with the word *START*.\n" +
                              "2️⃣ You will be presented with a series of multiple-choice questions. Each question will have numbered options.\n" +
                              "   👉 Reply with the number corresponding to your chosen option (e.g., 1, 2, etc.).\n" +
                              "3️⃣ If you need to stop at any time, reply with the word *END*.\n\n" +
                              "Ready? Here's your first question:\n\n" +
                              WhatsAppMessage.BuildQuestionMessage(firstQuestion);
                }
                else if (messageBody.Equals("end", StringComparison.OrdinalIgnoreCase))
                {
                    // End the assessment
                    await sender.Send(new EndWhatsAppAssessmentCommand(senderNumber));
                    var feedback = await sender.Send(new GenerateReportCommand(senderNumber));
                    message = $"Thank you for completing your assessment! 🎓\n\nYour responses have been reviewed, and here’s your feedback:\n\n{feedback}\n\nWe appreciate your time and effort. Have a wonderful day ahead!";

                }
                else if (int.TryParse(messageBody, out int answer))
                {
                    // Validate the answer
                    if (answer < 1 || answer > 4)
                    {
                        message = "Invalid response. Please reply with a number between 1 and 4 to select an option.";
                    }
                    else
                    {
                        // Process the answer and get the next question
                        var nextQuestion = await sender.Send(new TakeWhatsAppAssessmentCommand(senderNumber, messageBody));
                        if (nextQuestion == null)
                        {
                            message = "The assessment is now complete! 🎉\n\nThank you for your time. We’ll review your responses and provide feedback soon.";
                        }
                        else
                        {
                            message = WhatsAppMessage.BuildQuestionMessage(nextQuestion);
                        }
                    }
                }
                else
                {
                    // Provide help instructions for unknown inputs
                    message = "Hi there! 👋 I’m here to guide you through an Adaptive Assessment.\n\n" +
                              "To get started, reply with *START*. If you’re unsure how it works:\n" +
                              "1️⃣ Reply with *START* to begin the assessment.\n" +
                              "2️⃣ Answer questions by replying with the number of your chosen option (e.g., 1, 2, etc.).\n" +
                              "3️⃣ Reply with *END* to stop at any time.\n\n" +
                              "I’m here when you’re ready. 😊";
                }

                // Send the response via Twilio
                var messageResponse = await MessageResource.CreateAsync(
                    from: new PhoneNumber($"{twilioWhatsAppNumber}"),
                    to: new PhoneNumber(senderNumber),
                    body: message,
                    client: client
                );

                if (messageResponse.Sid != null)
                {
                    return Results.Ok($"Message sent successfully. SID: {messageResponse.Sid}");
                }

                return Results.BadRequest();
            });
        }


    }
}
