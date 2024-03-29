﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeManagement.Application.Commands;
using EmployeeManagement.Application.Response;
using EmployeeManagement.Core.Caching;
using EmployeeManagement.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly ICacheProvider<Employee> cacheProvider;

        public EmployeeController(IMediator mediator, ICacheProvider<Employee> cacheProvider)
        {
            this.mediator = mediator;
            this.cacheProvider = cacheProvider;
        }
        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await cacheProvider.GetCachedResponse("Employees");
            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost("create-employee")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeResponse>> Post([FromBody]CreateEmployeeCommand createEmployeeCommand)
        {
            var result = await this.mediator.Send(createEmployeeCommand);
            return Ok(result);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

