﻿using System.Net;

namespace PaperSquare.API.Shared;

public abstract class BaseApiResponse
{
    public HttpStatusCode StatusCode { get; set; }
    public bool IsSuccess { get; set; }

    public BaseApiResponse(HttpStatusCode statusCode, bool isSuccess)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
    }
}

public sealed class ApiResponse<TEntity> : BaseApiResponse
{
    public TEntity? Data { get; set; }

    public ApiResponse(TEntity data, HttpStatusCode statusCode = HttpStatusCode.OK, bool isSuccess = true) : base(statusCode, isSuccess)
    {
        Data = data;
    }
}

public sealed class ApiErrorResponse : BaseApiResponse
{
    public List<string> Messages { get; set; } = new();
    public string? Exception { get; set; }
    public string? SupportMesage { get; set; }

    public ApiErrorResponse(HttpStatusCode statusCode = HttpStatusCode.InternalServerError, bool isSuccess = false) : base(statusCode, isSuccess) { }
}