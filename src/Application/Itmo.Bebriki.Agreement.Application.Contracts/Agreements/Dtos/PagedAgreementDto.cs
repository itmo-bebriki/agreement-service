namespace Itmo.Bebriki.Agreement.Application.Contracts.Agreements.Dtos;

public sealed record PagedAgreementDto(long? Cursor, IReadOnlyCollection<AgreementDto> AgreementDtos);