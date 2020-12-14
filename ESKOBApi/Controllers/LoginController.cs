using ESKOBApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web.Http;

namespace ESKOBApi.Controllers
{
    [System.Web.Http.Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : Controller
    {
            //This resource is For all types of role
            [System.Web.Http.Authorize(Roles = "SuperAdmin, Admin, User")]
            [System.Web.Http.HttpGet]
           
            public IHttpActionResult GetResource1()
            {
                var identity = (ClaimsIdentity)User.Identity;
                return (IHttpActionResult)Ok("Hello: " + identity.Name);
            }


        } } 