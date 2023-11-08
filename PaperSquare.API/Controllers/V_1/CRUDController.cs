using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.API.Shared;
using PaperSquare.Core.Application.Shared.Dto;
using PaperSquare.Infrastructure.Shared;
using System.Net.Mime;

namespace PaperSquare.API.Controllers.V_1
{
    public class CRUDController<TModel, TType, TSearch, TInsert, TUpdate> : BaseController<TModel, TType, TSearch> where TModel : class where TSearch : SearchRequest where TInsert : class where TUpdate : class
    {
        public CRUDController(ICommandService<TModel, TSearch, TType, TInsert, TUpdate> service): base(service) {}

        #region POST

        [HttpPost("insert")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorResponse))]
        public virtual async Task<IActionResult> Insert([FromBody] TInsert insert)
        {
            var result = await ((ICommandService<TModel, TSearch, TType, TInsert, TUpdate>)_queryService).Insert(insert);

            return CreatedAtAction(nameof(Insert), new ApiResponse<TModel>(result.Value));
        }
        
        [HttpPost("update")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorResponse))]
        public virtual async Task<IActionResult> Update(TType id, [FromBody] TUpdate update)
        {
            var result = await ((ICommandService<TModel, TSearch, TType, TInsert, TUpdate>)_queryService).Update(id, update);

            return Ok(new ApiResponse<TModel>(result.Value));
        }

        #endregion POST

        #region DELETE

        [HttpDelete("delete")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ApiErrorResponse))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiErrorResponse))]
        public virtual async Task<IActionResult> Delete(TType id)
        {
            var result = await ((ICommandService<TModel, TSearch, TType, TInsert, TUpdate>)_queryService).Delete(id);

            return Ok(new ApiResponse<TModel>(result.Value));
        }

        #endregion DELETE
    }
}
