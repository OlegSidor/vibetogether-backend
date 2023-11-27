using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VibeTogether.Authorization.JWT;
using VibeTogether.Authorization.Models;

namespace VibeTogether.Authorization.JWT;

public class JwtHelper : IJwtHelper
{
    private readonly string Key;
    private readonly IConfiguration configuration;

    public JwtHelper(IConfiguration config)
    {
        Key = config.GetSection("JwtKey").Value;
        configuration = config;
    }

    public string GenerateJwtToken(VibeUser user)
    {
        var userInfo = VibeUserToUserInfo(user);
        List<Claim> claims = null!;

        try
        {
            claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, user.Email),
                new(JwtRegisteredClaimNames.Email, user.Email),
                new("UserInfo", JsonConvert.SerializeObject(userInfo))
            };
        }
        catch (NullReferenceException e)
        {
            Debug.WriteLine("NullReferenceException in GenerateJwtToken\n" + e);

            if(userInfo != null)
                foreach (var prop in userInfo.GetType().GetProperties())
                    prop.SetValue(userInfo, "Invalid");
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: configuration.GetSection("JwtIssuer").Value,
            audience: configuration.GetSection("JwtAudience").Value,
            claims: claims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private  UserInfo VibeUserToUserInfo(VibeUser user)
    {
        var userInfo = new UserInfo
        {
            UserId = user.Id,
            Username = user.UserName,
            Email = user.Email,
            Avatar = user.Avatar
        };

        return userInfo;
    }
}

