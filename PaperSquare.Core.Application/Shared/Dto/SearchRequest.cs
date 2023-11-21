namespace PaperSquare.Core.Application.Shared.Dto
{
    public abstract record SearchRequest(int? page = 1, int? pageSize = 10);
}
