using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Infrastructure.Shared;
using PaperSquare.Infrastructure.Shared.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Controllers.V_1
{
    public class CRUDController<TModel, TType, TSearch, TInsert, TUpdate> : BaseController<TModel, TType, TSearch> where TModel : class where TSearch : SearchDto where TInsert : class where TUpdate : class
    {
        public CRUDController(ICommandService<TModel, TSearch, TType, TInsert, TUpdate> service): base(service) {}

        #region POST

        [HttpPost("insert")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public virtual async Task<IActionResult> Insert([FromBody] TInsert insert)
        {
            var result = await ((ICommandService<TModel, TSearch, TType, TInsert, TUpdate>)_queryService).Insert(insert);

            if (!result.IsSuccess)
            {
                return BadRequest(result.Errors);
            }

            return CreatedAtAction(nameof(Insert), result.Value);
        }
        
        [HttpPost("update")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> Update(TType id, [FromBody] TUpdate update)
        {
            var result = await ((ICommandService<TModel, TSearch, TType, TInsert, TUpdate>)_queryService).Update(id, update);

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        #endregion POST

        #region DELETE

        [HttpDelete("delete")]
        [MapToApiVersion(ApiVersions.V_1)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public virtual async Task<IActionResult> Delete(TType id)
        {
            var result = await ((ICommandService<TModel, TSearch, TType, TInsert, TUpdate>)_queryService).Delete(id);

            if (!result.IsSuccess)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }

        #endregion DELETE
    }
}
