using PaperSquare.Core.Application.Shared.Dto;

namespace PaperSquare.Core.Application.Features.UserManagement.Dto;

public record UserSearchDto : SearchRequest
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
}
