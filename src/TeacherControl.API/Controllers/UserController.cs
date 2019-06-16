using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using TeacherControl.API.Extensors;
using TeacherControl.Common.Enums;
using TeacherControl.Common.Extensors;
using TeacherControl.Core.DTOs;
using TeacherControl.Core.Queries;
using TeacherControl.Domain.Repositories;

namespace TeacherControl.API.Controllers
{
    [Authorize]
    [Route("api/users")]
    public class UserController : Controller
    {
        protected readonly IUserRepository _UserRepo;
        protected readonly ILogger<UserController> _Logger;
        
        public UserController(IUserRepository UserRepo, ILogger<UserController> logger)
        {
            _UserRepo = UserRepo;
            _Logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllUsers([FromQuery] UserQuery query)
        {
            return this.Ok(() => new {
                    filters = query,
                    data = _UserRepo.GetAll(query)
                }, _Logger);
        }

        [HttpPost, Route("{UserId:int:min(1)}/add-userinfo")]
        public IActionResult CreateUserInfo([FromRoute] int UserId, [FromBody] UserInfoDTO dto)
        {
            return this.Created(() => 
                _UserRepo.AddUserInfo(UserId, dto).Equals(TransactionStatus.SUCCESS)
                ? dto
                : new UserInfoDTO()
                ,_Logger);
        }

        [HttpPost, Route("is-username-available")]
        //TODO: Implement regex for the username here
        public IActionResult IsUsernameAvailable([FromBody] string Username)
        {
            return this.Ok(() => _UserRepo.IsUsernameAvailable(Username).ToString(), _Logger);
        }

        [HttpPost, Route("is-email-available")]
        //TODO: Implement regex for the username here
        public IActionResult IsEmailAvailable([FromBody] string Email)
        {
            return Json(_UserRepo.IsEmailAvailable(Email));
        }

        [HttpDelete, Route("{UserId:int:min(1)}/remove-user")]
        public IActionResult RemoveUser([FromRoute] int UserId)
        {
            return this.NoContent(() => 
                _UserRepo.Remove(UserId).Equals(TransactionStatus.SUCCESS), _Logger);
        }

        [HttpPost, Route("{UserId:int:min(1)}/create-userinfo")]
        public IActionResult CreatedUserInfo([FromRoute] int UserId, [FromBody] UserInfoDTO dto)
        {
            return this.Created(() =>
            {
                return _UserRepo.AddUserInfo(UserId, dto).Equals(TransactionStatus.SUCCESS)
                    ? dto
                    : new UserInfoDTO();
            }, _Logger);
        }

        [HttpPut, Route("{UserId:int:min(1)}/update-userinfo")]
        public IActionResult UpdateUserInfo([FromRoute] int UserId, [FromBody] UserInfoDTO dto)
        {
            return this.NoContent(() =>
                _UserRepo.AddUserInfo(UserId, dto).Equals(TransactionStatus.SUCCESS), _Logger);
        }

        [HttpGet, Route("{UserId:int:min(1)}/course-credits")]
        public IActionResult GetUserCourseCredits([FromRoute] int UserId)
        {
            return this.Ok(() => _UserRepo.GetCourseCredits(UserId), _Logger);
        }

        [HttpGet, Route("{UserId:int:min(1)}/course-credits/{CourseId:int:min(1)}")]
        public IActionResult GetUserCourseCreditsByCourseId([FromRoute] int UserId, [FromRoute] int CourseId)
        {
            return this.Ok(() => 
                _UserRepo.GetCourseCreditsByCourseId(UserId, CourseId), _Logger);
        }
    }
}