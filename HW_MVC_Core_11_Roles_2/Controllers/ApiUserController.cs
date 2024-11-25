using HW_MVC_Core_11_Roles_2.Models;
using HW_MVC_Core_11_Roles_2.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HW_MVC_Core_11_Roles_2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiUserController : Controller
    {
        private IServiceUser _serviceUser;
        public ApiUserController(IServiceUser serviceUser)
        {
            _serviceUser = serviceUser;
        }
        [HttpPost("CreateUser")]public async Task<JsonResult> CreateUser(UserModel model)
        {
            return Json(await _serviceUser.CreateUserAsync(model));
        }
        [HttpPost("CreateRole")]
        public async Task<JsonResult> CreateRole(RoleModel roleName)
        {
            return Json(await _serviceUser.CreateRoleAsync(roleName));
        }
        [HttpPost("AssignRole")]
        public async Task<JsonResult> AsignRole(AssignRoleUserModel model)
        {
            return Json(await _serviceUser.AsignRoleAsync(model));
        }
        [HttpPut("UpdateUser")]
        public async Task<JsonResult> UpdateUser(UserModel model)
        {
            return Json(await _serviceUser.UpdateUserAsync(model));
        }
        [HttpDelete("DeleteUser")]
        public async Task<JsonResult> DeleteUser(UserModel model)
        {
            return Json(await _serviceUser.DeleteUserAsync(model));
        }
    }
}
