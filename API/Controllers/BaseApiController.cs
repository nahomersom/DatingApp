using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]//ApiController attributes
    [Route("api/[controller]")]//inorder to route user need to navigate api/controller name
    public class BaseApiController : ControllerBase
    {
        
    }
}