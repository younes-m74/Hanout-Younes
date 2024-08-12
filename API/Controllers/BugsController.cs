using System;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BugsController : BaseApiController
{
    [HttpGet("unauthorized")]
    public ActionResult GetUnauthorized()
    {
        return Unauthorized("You are not authorized to access this resource");
    }
    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest("Not a good request");
    }
    [HttpGet("notfound")]
    public ActionResult GetNotFound()
    {
        return NotFound("Resource not found");
    }
    [HttpGet("internalerror")]
    public ActionResult GetInternalError()
    {
        throw new Exception("This is a test exception");
    }
    [HttpPost("validationerror")]
    public ActionResult GetValidationError(CreateProductDTo product)
    {
        return Ok();
    }

}
