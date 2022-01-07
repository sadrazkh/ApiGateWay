using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Common;
using Services.WebFramework.Pagination;

namespace Services.DTOs;



public class ServiceResultDto
{
    public bool Success { get; set; }
    public string[] Message { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public bool IsSuccessFullyFinished() => Success;

    #region Implicit Operators
    public static implicit operator ServiceResultDto(bool value)
    {
        if (value)
        {
            return new ServiceResultDto() { Message = new[] { "با موفقیت انجام شد" }, StatusCode = HttpStatusCode.OK, Success = value };
        }
        else
        {
            return new ServiceResultDto() { Message = new[] { "با موفقیت انجام نشد" }, StatusCode = HttpStatusCode.BadRequest, Success = value }; 
        }

    }

    //public static implicit operator ServiceResultDto(OkObjectResult result)
    //{
    //    return new ServiceResultDto(true, ApiResultStatusCode.Success, (string[])result.Value);
    //}

    //public static implicit operator ServiceResultDto(BadRequestResult result)
    //{
    //    return new ServiceResultDto(false, ApiResultStatusCode.BadRequest);
    //}

    //public static implicit operator ServiceResultDto(BadRequestObjectResult result)
    //{
    //    var message = result.Value?.ToString();
    //    if (result.Value is SerializableError errors)
    //    {
    //        var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
    //        message = string.Join(" | ", errorMessages);
    //    }
    //    return new ServiceResultDto(false, ApiResultStatusCode.BadRequest, new string[] { message });
    //}

    //public static implicit operator ApiReServiceResultDtosult(ContentResult result)
    //{
    //    return new ServiceResultDto(true, ApiResultStatusCode.Success, new string[] { result.Content });
    //}

    //public static implicit operator ServiceResultDto(NotFoundResult result)
    //{
    //    return new ServiceResultDto(false, ApiResultStatusCode.NotFound);
    //}

    #endregion

}


public class ServiceResultDto<TData> : ServiceResultDto
{
    public TData Object { get; set; }

    #region Implicit Operators
    public static implicit operator ServiceResultDto<TData>(TData data)
    {
        if (data == null)
        {
            return new ServiceResultDto<TData>() { Message = new[] { "با موفقیت انجام نشد" }, Success = false, StatusCode = HttpStatusCode.BadRequest, Object = data };
        }

        return new ServiceResultDto<TData>() { Message = new []{"با موفقیت انجام شد"}, Success = true, StatusCode = HttpStatusCode.OK, Object = data};
    }


    public static async Task<ServiceResultDto<PagedList<TData>>> FromIQueryable(IQueryable query,
        IConfigurationProvider configurationProvider, PaginationParameters paginationParameters,
        CancellationToken cancellationToken)

    {
        return new ServiceResultDto<PagedList<TData>>()
        {
            Success = true,
            Message = new[] {"با موفقیت انجام شد"},
            StatusCode = HttpStatusCode.OK,
            Object = await PagedList<TData>.ToPagedList(query.ProjectTo<TData>(configurationProvider),
                paginationParameters.PageNumber, paginationParameters.PageSize, cancellationToken)
        };
    }


    //public static implicit operator ApiResult<TData>(OkResult result)
    //{
    //    return new ServiceResultDto<TData>(true, HttpStatusCode.OK, null);
    //}

    //public static implicit operator ServiceResultDto<TData>(OkObjectResult result)
    //{
    //    return new ServiceResultDto<TData>(true, HttpStatusCode.OK, (TData)result.Value);
    //}

    //public static implicit operator ServiceResultDto<TData>(BadRequestResult result)
    //{
    //    return new ServiceResultDto<TData>(false, HttpStatusCode.BadRequest, null);
    //}

    //public static implicit operator ApiResult<TData>(BadRequestObjectResult result)
    //{
    //    var message = result.Value?.ToString();
    //    if (result.Value is SerializableError errors)
    //    {
    //        var errorMessages = errors.SelectMany(p => (string[])p.Value).Distinct();
    //        message = string.Join(" | ", errorMessages);
    //    }
    //    return new ServiceResultDto<TData>(false, ServiceResultDto.BadRequest, null, new[] { message });
    //}

    //public static implicit operator ServiceResultDto<TData>(ContentResult result)
    //{
    //    return new ApiResult<TData>(true, ApiResultStatusCode.Success, null, new[] { result.Content });
    //}

    //public static implicit operator ServiceResultDto<TData>(NotFoundResult result)
    //{
    //    return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, null);
    //}

    //public static implicit operator ServiceResultDto<TData>(NotFoundObjectResult result)
    //{
    //    return new ApiResult<TData>(false, ApiResultStatusCode.NotFound, (TData)result.Value);
    //}

    #endregion

}
