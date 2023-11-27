using PaperSquare.Core.Domain;

namespace PaperSquare.Core.Application.Features.UserManagement.Dto;

public sealed record UserResponse(string id, string username, PersonalInfo personalInfo, string? createdBy = "");
