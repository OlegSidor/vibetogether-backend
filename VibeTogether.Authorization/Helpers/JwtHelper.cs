using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using VibeTogether.Authorization.Models;

namespace VibeTogether.Authorization.Helpers;

internal static class JwtHelper
{
    public const string Key = "E8C76A8136892F7588D398DB3F474"; // Хардкод наше все

    public static string GenerateJwtToken(VibeUser user)
    {
        var userInfo = VibeUserToUserInfo(user);
        List<Claim> claims = null!;

        try
        {
            claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Sub, user.Id),
                new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
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
            issuer: "http://vibetogether.xyz/",
            audience: "VibeTogether",
            claims: claims,
            expires: DateTime.Now.AddDays(3),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static UserInfo VibeUserToUserInfo(VibeUser user)
    {
        var userInfo = new UserInfo
        {
            Username = user.UserName,
            Email = user.Email,
            Avatar = user.Avatar
        };

        return userInfo;
    }
}

