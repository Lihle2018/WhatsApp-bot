
using MediatR;

namespace Assessments.Application.UseCases.TakeWhatsAppAssessment
{
    public record EndWhatsAppAssessmentCommand(string phoneNumber):IRequest
    {
    }
}
