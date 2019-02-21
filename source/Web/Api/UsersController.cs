using DotNetCore.AspNetCore;
using DotNetCore.Extensions;
using DotNetCore.Objects;
using DotNetCoreArchitecture.Application;
using DotNetCoreArchitecture.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DotNetCoreArchitecture.Web
{
    [ApiController]
    [RouteController]
    public class UsersController : ControllerBase
    {
        public UsersController(IUserService userService)
        {
            UserService = userService;
        }

        private IUserService UserService { get; }

        [HttpPost]
        public async Task<IActionResult> AddAsync(AddUserModel addUserModel)
        {
            var result = await UserService.AddAsync(addUserModel);

            return new ActionIResult(result);
        }

        [HttpDelete("{userId}")]
        public async Task<IResult> DeleteAsync(long userId)
        {
            return await UserService.DeleteAsync(userId);
        }

        [HttpGet]
        public async Task<IEnumerable<UserModel>> ListAsync()
        {
            return await UserService.ListAsync();
        }

        [HttpGet("{userId}")]
        public async Task<UserModel> SelectAsync(long userId)
        {
            return await UserService.SelectAsync(userId);
        }

        [AllowAnonymous]
        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(SignInModel signInModel)
        {
            var result = await UserService.SignInJwtAsync(signInModel);

            return new ActionIResult(result);
        }

        [HttpPost("SignOut")]
        public Task SignOutAsync()
        {
            var signOutModel = new SignOutModel(User.Id());

            return UserService.SignOutAsync(signOutModel);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateAsync(long userId, UpdateUserModel updateUserModel)
        {
            updateUserModel.UserId = userId;

            var result = await UserService.UpdateAsync(updateUserModel);

            return new ActionIResult(result);
        }
    }
}
