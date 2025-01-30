using System.Runtime.Serialization;
namespace Assessments.Domain.Enums
{
    
    public enum AssessmentStatus
    {
        [EnumMember(Value = "Pending")]
        Pending,         

        [EnumMember(Value = "Completed")]
        Completed,      

        [EnumMember(Value = "Analyzed")]
        Analyzed        
    }
}
