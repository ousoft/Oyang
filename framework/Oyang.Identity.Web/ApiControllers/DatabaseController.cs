﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Oyang.Identity.Application.Database;
using Oyang.Identity.Domain.User;

namespace Oyang.Identity.Web.ApiControllers
{
    [Route("api/[controller]/[action]/{id?}")]
    [ApiController]
    public class DatabaseController : ControllerBase
    {
        private readonly IDatabaseAppService _appService;
        public DatabaseController(IDatabaseAppService appService)
        {
            _appService = appService; 
        }

        public IActionResult GenerateDatabase()
        {
            _appService.GenerateDatabase();
            return Ok();
        }
        public IActionResult GenerateSeedData()
        {
            _appService.GenerateSeedData();
            return Ok();
        }
        public IActionResult ClearSeedData()
        {
            _appService.ClearSeedData();
            return Ok();
        }
    }
}
