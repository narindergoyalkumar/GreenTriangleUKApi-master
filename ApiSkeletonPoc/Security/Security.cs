using ApiSkeletonPoc.Core.Models;
using ApiSkeletonPoc.Helpers;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiSkeletonPoc.Core.Common
{
    public static class Security
    {
        public static TokenModel GenerateJwtToken(string email, UserModel user, TokenOption options, string role
            )
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.Sid, user.Id.ToString())
            };
            if (!string.IsNullOrEmpty(Convert.ToString(user.ClientId)))
            {
                claims.Add(new Claim(ClaimTypes.UserData, Convert.ToString(user.ClientId)));
            }
            if (!string.IsNullOrEmpty(Convert.ToString(user.ClientId)))
            {
                claims.Add(new Claim(ClaimTypes.GivenName, Convert.ToString(user.Client)));
            }
            DateTime TokenExpiry;
            TokenExpiry = DateTime.Now.AddMinutes(options.ExpireMinutes);

            var token = new JwtSecurityToken
             (
                 issuer: options.Issuer,
                 audience: options.Audience,
                 claims: claims,
                 expires: TokenExpiry,
                 notBefore: DateTime.UtcNow,
                 signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key)),
                         SecurityAlgorithms.HmacSha256)
             );
            var response = new TokenModel
            {
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expires = TokenExpiry,
                Role = role,
                Name = user.FirstName + " " + user.LastName,
                UserId = user.Id,
                SubscribedModules = user.SubscribedModules
            };
            return response;

        }
    }
}
