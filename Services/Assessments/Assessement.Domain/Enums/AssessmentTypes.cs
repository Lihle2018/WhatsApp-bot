
using System.Runtime.Serialization;

namespace Assessments.Domain.Enums
{
    public enum AssessmentTypes
    {
        [EnumMember(Value = "MultipleChoice")]
        MultipleChoice,
        [EnumMember(Value = "TrueFalse")]
        TrueFalse,
        [EnumMember(Value = "RatingScale")]
        RatingScale,
    }
}
