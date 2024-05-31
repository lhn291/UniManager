using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using UniManager.Application.Interfaces.Authentication;
using UniManager.Application.Interfaces.Services;

namespace UniManager.Infrastructure.Authentication
{
    public class JwtTokenGenerator<TEntity> : IJwtTokenGenerator<TEntity> where TEntity : class
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IDatetimeProvider _datetimeProvider;
        private readonly Expression<Func<TEntity, string>> _idSelector;
        private readonly Expression<Func<TEntity, string>> _userNameSelector;
        private readonly Expression<Func<TEntity, string>> _roleSelector;

        public JwtTokenGenerator(
            IDatetimeProvider datetimeProvider,
            IOptions<JwtSettings> jwtOptions,
            Expression<Func<TEntity, string>> idSelector,
            Expression<Func<TEntity, string>> userNameSelector,
            Expression<Func<TEntity, string>> roleSelector)
        {
            _datetimeProvider = datetimeProvider;
            _jwtSettings = jwtOptions.Value;
            _idSelector = idSelector;
            _userNameSelector = userNameSelector;
            _roleSelector = roleSelector;
        }

        public string GenerateToken(TEntity entity)
        {
            var signingCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256
            );

            var idValue = _idSelector.Compile()(entity);
            var userNameValue = _userNameSelector.Compile()(entity);
            var roleValue = _roleSelector.Compile()(entity);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, idValue),
                new Claim(JwtRegisteredClaimNames.GivenName, userNameValue),
                new Claim("role", roleValue),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var securityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                expires: _datetimeProvider.utcNow.AddMinutes(_jwtSettings.ExpiryMinutes),
                claims: claims,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}
