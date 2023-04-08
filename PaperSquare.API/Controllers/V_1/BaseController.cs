using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using PaperSquare.Infrastructure.Shared;
using System.Net.Mime;

namespace PaperSquare.API.Controllers.V_1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController<TModel, TType, TSearch> : ControllerBase where TModel : class where TSearch : class
    {
        protected readonly IQueryService<TModel, TSearch, TType> _queryService;

        public BaseController(IQueryService<TModel, TSearch, TType> queryService)
        {
            _queryService = queryService;
        }

        #region GET

        [HttpGet("get-all")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [AllowAnonymous]
        public virtual async Task<IActionResult> GetAll([FromQuery] TSearch searchDto)
        {
            var result = await _queryService.GetAll(searchDto);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
        
        [HttpGet("get-by-id")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [AllowAnonymous]
        public virtual async Task<IActionResult> GetById([FromQuery] TType id)
        {
            var result = await _queryService.GetById(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        #endregion GET
    }
}
