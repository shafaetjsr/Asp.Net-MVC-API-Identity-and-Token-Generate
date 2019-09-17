using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApiTokenAuthorization.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class DateController : ApiController
    {

        [AllowAnonymous]
        [HttpGet]
        [Route("api/data/forall")]
        public IHttpActionResult get()
        {
            return Ok("Now Server Time is : " + DateTime.Now.ToString());
        }


        [Authorize]
        [HttpGet]
        [Route("api/data/authenticate")]
        public IHttpActionResult getForAuthrization()
        {
            var identity = (ClaimsIdentity)User.Identity;
            return Ok("Hello " + identity.Name);
        }

        [Authorize(Roles ="admin")]
        [HttpGet]
        [Route("api/data/authorize")]
        public IHttpActionResult getForAdmin()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var roles = identity.Claims
                .Where(x => x.Type == ClaimTypes.Role)
                .Select(x => x.Value);
            return Ok("Hello " + identity.Name + " Role " + roles);
        }
    }
}
