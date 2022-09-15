using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;
using API.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugController : BaseApiController
    {
           private readonly DataContext _context;
        public BugController(DataContext context)
        {
            _context = context;
        }
    [Authorize]//user need to authorize or send an authorization header
    [HttpGet("auth")]// to access api/bug/auth
    //The Controller Action methods(Action REsult) are expected to return the results to the Client. 
    //The Client may expect simple results like string & integers or 
    //complex results like Json formatted data, HTML views or a file to download etc.
     public ActionResult<String> GetResult(){
        
        return "this is the secret";
     }
     [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound(){
        
        var thing = _context.Users.Find(-1);// we know there no user with -1 id
        if(thing == null) return NotFound();//butit in microsoft notfound method
        return Ok(thing);

     }
       [HttpGet("server-error")]
    public ActionResult<String> GetServerError(){
          var thing = _context.Users.Find(-1);// we know there no user with -1 id
          var thingToReturn = thing.ToString();//thing return null , so if we convert the null to string we get null reference exception
        
        return thingToReturn;
     }
         [HttpGet("bad-request")]
         public ActionResult<String> GetBadRequest(){
        
        return BadRequest("This is a Bad Request");
     }


    }
}