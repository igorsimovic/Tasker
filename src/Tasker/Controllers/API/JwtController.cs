using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Tasker.Options;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Tasker.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Principal;
using DataLayer.DataModel.MongoData;
using DataLayer.Repositories;
using DomainModel.Repositories;
using DomainModel.Entities;

namespace Tasker.Controllers.API
{
    [Route("api/v1/[controller]")]
    public class JwtController : Controller
    {
        private readonly IUserRepository repo_;
        private readonly JwtIssuerOptions _jwtOptions;
        private readonly ILogger _logger;
        private readonly JsonSerializerSettings _serializerSettings;

        public JwtController(IOptions<JwtIssuerOptions> jwtOptions, ILoggerFactory loggerFactory , IUserRepository repo)
        {
            repo_ = repo;
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);

            _logger = loggerFactory.CreateLogger<JwtController>();

            _serializerSettings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromForm] ApplicationUser user)
        {
            var identity = await GetClaimsIdentity(user);
            if (identity == null)
            {
                _logger.LogInformation($"Invalid username ({user.UserName}) or password ({user.Password})");
                return BadRequest("Invalid credentials");
            }

            var claims = new[]{
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, await _jwtOptions.JtiGenerator()),
                new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64), identity.FindFirst("TaskerUser")
              };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            UserDTO u = repo_.getUserByCredentials(user.UserName, user.Password);

            // Serialize and return the response
            var response = new
            {
                access_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds,
                user_id = u.Id
            };

            var json = JsonConvert.SerializeObject(response, _serializerSettings);
            return new OkObjectResult(json);
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] ApplicationUser user)
        {
            //Ovde kreirati korisnika u bazi
            repo_.CreateUser(new UserDTO { FullName = user.FullName, UserName = user.UserName, NewPassword = user.Password, Bio = user.Bio });

            return new OkObjectResult(new { });
        }
        
        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
            {
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));
            }

            if (options.SigningCredentials == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));
            }

            if (options.JtiGenerator == null)
            {
                throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
            }
        }

        /// <returns>Date converted to seconds since Unix epoch (Jan 1, 1970, midnight UTC).</returns>
        private static long ToUnixEpochDate(DateTime date)
          => (long)Math.Round((date.ToUniversalTime() - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero)).TotalSeconds);
        
        /// <summary>
        /// Ovde treba provera kredencijala iz mongo_repo<ApplicationUser>
        /// </summary>
        private Task<ClaimsIdentity> GetClaimsIdentity(ApplicationUser user)
        {

            UserDTO u = repo_.getUserByCredentials(user.UserName, user.Password);
            
            if (u != null)
            {
                return Task.FromResult(new ClaimsIdentity(new GenericIdentity(user.UserName, "Token"),
                  new[] { new Claim("TaskerUser", "RegularUser")} ));
            }

            // Credentials are invalid, or account doesn't exist
            return Task.FromResult<ClaimsIdentity>(null);
        }
        
    }
}