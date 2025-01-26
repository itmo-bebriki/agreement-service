using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Agreement.Contracts;

public sealed partial class QueryAgreementRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (AgreementIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "AgreementIds contains invalid values. Agreement ID is required and must be greater than zero.",
                [nameof(AgreementIds)]);
        }

        if (JobTaskIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "JobTaskIds contains invalid values. Job task ID is required and must be greater than zero.",
                [nameof(JobTaskIds)]);
        }

        if (States.Any(state => !Enum.IsDefined(typeof(JobTaskState), state)))
        {
            yield return new ValidationResult(
                "States contains invalid JobTaskState values.",
                [nameof(States)]);
        }

        if (AssigneeIds.Any(id => id <= 0))
        {
            yield return new ValidationResult(
                "AssigneeIds contains invalid values. Assignee ID is required and must be greater than zero.",
                [nameof(AssigneeIds)]);
        }

        if (Cursor < 0)
        {
            yield return new ValidationResult(
                "Cursor must be greater than or equal to 0.",
                [nameof(Cursor)]);
        }

        if (PageSize < 0)
        {
            yield return new ValidationResult(
                "PageSize must be greater than or equal to 0.",
                [nameof(PageSize)]);
        }
    }
}