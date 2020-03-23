using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oyang.Identity.Application.User;
using Oyang.Identity.Domain.User;

namespace Oyang.Identity.Web.ApiControllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserAppService _userAppService;
        public UserController(IUserAppService userAppService)
        {
            _userAppService = userAppService; 
        }

        public IActionResult GetList(GetListInputDto input)
        {
            return Ok(_userAppService.GetList(input));
        }

        [HttpPost]
        public void Add(AddInputDto input)
        {
            _userAppService.Add(input);
        }

        [HttpPost]
        public void Update(UpdateInputDto input)
        {
            _userAppService.Update(input);
        }

        [HttpPost]
        public void Remove(Guid id)
        {
            _userAppService.Remove(id);
        }

        [HttpPost]
        public void SetRole(SetRoleInputDto input)
        {
            _userAppService.SetRole(input);
        }

        [HttpPost]
        public void ResetPassword(ResetPasswordInputDto input)
        {
            _userAppService.ResetPassword(input);
        }

    }
}
