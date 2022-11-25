using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaperSquare.API.Controllers.V_1;
using PaperSquare.API.Infrastructure.Versioning;
using PaperSquare.Core.Models.Identity;
using PaperSquare.Infrastructure.Features.UserManagement;
using PaperSquare.Infrastructure.Features.UserManagement.Dto;
using System.Net.Mime;

namespace PaperSquare.API.Features.Users.V_1
{
    [Route("api/user")]
    [ApiController]
    public class UsersController : CRUDController<UserDto, string, UserSearchDto, UserInsertDto, UserInsertDto>
    {   
        public UsersController(IUserService userService): base(userService){}

        #region GET


        #endregion GET

        #region POST

        [AllowAnonymous]
        public override async Task<IActionResult> Insert([FromBody] UserInsertDto insert)
        {
            return await base.Insert(insert);
        }

        #endregion POST
    }
}
