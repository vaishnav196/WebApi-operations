using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //private readonly ApplicationDbContext db;

        //public AuthController(ApplicationDbContext db) 
        //{
        //this.db = db;
        //}

        //[HttpPost]
        //[Route("/SignUp")]
        //public IActionResult SignUp(User us)
        //{
        //    db.users.Add(us);
        //    db.SaveChanges();
        //    return Ok("Registered Successfully");

        //}


        //[HttpPost]
        //[Route("/sigIn")]
        //public IActionResult SignIn(Login log)
        //{
        //    var data = db.users.Where(x => x.Username.Equals(log.Username) && x.Password.Equals(log.Password)).SingleOrDefault();
        //    //if (data == null)
        //    //{
        //    //    return Ok("Invalid Credentials");
        //    //}
        //    if (data != null)
        //    {
        //        var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, data.Username) }, CookieAuthenticationDefaults.AuthenticationScheme);
        //        var principal = new ClaimsPrincipal(identity);
        //        HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        //        HttpContext.Session.SetString("Username", data.Username);
        //        return Ok("Login Success");

        //    }
           
        //    else
        //    {
        //        return Ok("Invalid  credentials");


        //    }
           
        }
    }

