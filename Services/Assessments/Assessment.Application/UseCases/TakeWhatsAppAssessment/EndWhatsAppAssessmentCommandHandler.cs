using Assessments.Domain.Repositories;
using MediatR;

namespace Assessments.Application.UseCases.TakeWhatsAppAssessment
{
    public class EndWhatsAppAssessmentCommandHandler(IAssessmentRepository assessmentRepository) : IRequestHandler<EndWhatsAppAssessmentCommand>
    {
        public async Task Handle(EndWhatsAppAssessmentCommand request, CancellationToken cancellationToken)
        {
            var assessment = await assessmentRepository.GetAssessment(request.phoneNumber);
            assessment.MarkAsComplete();
            await assessmentRepository.UpdateAssessmentAsync(assessment);
        }
    }
}
