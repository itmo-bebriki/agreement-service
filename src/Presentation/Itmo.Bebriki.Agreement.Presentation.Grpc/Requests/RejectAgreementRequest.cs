using System.ComponentModel.DataAnnotations;

namespace Itmo.Bebriki.Agreement.Contracts;

public sealed partial class RejectAgreementRequest : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (AgreementId <= 0)
        {
            yield return new ValidationResult(
                "Agreement ID is required and must be greater than zero.",
                [nameof(AgreementId)]);
        }
    }
}