using PaperSquare.Core.Domain.Entities.UserAggregate.ValueObjects;
using PaperSquare.Core.Domain.Primitives;

namespace PaperSquare.Core.Domain.Entities.UserAggregate.Events;

public sealed record UserCreatedDomainEvent(string username, VerificationCode verificationCode, string mail): IDomainEvent {}

