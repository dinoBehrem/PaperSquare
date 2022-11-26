using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Controllers.V_1;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Core.Infrastructure.CurrentUserAccessor;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Core.Permissions;
using PaperSquare.Infrastructure.Features.UserManagement;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Features.Users.V_1
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : CRUDController<UserDto, string, UserSearchDto, UserInsertDto, UserUpdateDto>
    {
        public UsersController(IUserService userService) : base(userService){}

        #region POST

        [AllowAnonymous]
        public override async Task<IActionResult> Insert([FromBody] UserInsertDto insert)
        {
            return await base.Insert(insert);
        }
        
        [Authorize(Policy = Permission.RegisteredUser)]
        public override async Task<IActionResult> Update(string id, [FromBody] UserUpdateDto update)
        {
            return await base.Update(id, update);
        }

        #endregion POST

        #region DELETE

        [Authorize(Policy = Permission.RegisteredUser)]
        public override async Task<IActionResult> Delete(string id)
        {
            return await base.Delete(id);
        }

        #endregion DELETE
    }
}
