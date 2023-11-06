namespace PaperSquare.Core.Application.Features.JWT.Dto;

public sealed class TokenResource
{
    public TokenResource(string token, DateTime expiriation)
    {
        Token = token;
        Expiriation = expiriation;
    }

    public string Token { get; init; }
    public DateTime Expiriation { get; init; }
}
