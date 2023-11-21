namespace PaperSquare.Core.Application.Features.UserManagement.Dto;

public record UserDto
{
    public string Id { get; set; }
    public string UserName { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Birthdate { get; set; }
}
