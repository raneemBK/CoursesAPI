using CoursesAPI.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CoursesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly coursesContext _coursesContext;
        public AuthenticationController(coursesContext coursesContext)
        {
            _coursesContext = coursesContext;
        }


        [HttpPost]
        [Route("Login")]

        public ActionResult Login(Login login)
        {
            var auth = _coursesContext.Logins.Where(l=> l.Username == login.Username && l.Password == login.Password).FirstOrDefault();
            if (auth == null)
            {
                return Unauthorized();
            }
            else
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sfcsafsafsfuperSecretKeygteg@3451111234"));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                var claims = new List<Claim>
                {
                    new(ClaimTypes.Name, auth.Username),
                    new Claim(ClaimTypes.Role, auth.Roleid.ToString()),
                    new Claim("password",auth.Password),
                };
                var tokeOptions = new JwtSecurityToken(
                claims: claims,
                expires:
                DateTime.Now.AddHours(24),
                signingCredentials: signinCredentials
);
                var tokenString = new JwtSecurityTokenHandler().WriteToken(tokeOptions);
                return Ok(tokenString);
            }
        }
    }
}
