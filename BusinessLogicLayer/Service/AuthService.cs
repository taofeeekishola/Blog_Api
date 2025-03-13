using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BusinessLogicLayer.IServices;
using DomainLayer.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateJwtToken(User user)
    {
        // Retrieve JWT settings from configuration
        var jwtKey = _configuration["Jwt:Key"];
        var jwtIssuer = _configuration["Jwt:Issuer"];
        var jwtAudience = _configuration["Jwt:Audience"];
        var jwtExpiryInMinutes = Convert.ToInt32(_configuration["Jwt:ExpiryInMinutes"]);

        // Ensure the key is valid
        if (string.IsNullOrEmpty(jwtKey) || jwtKey.Length < 32)
        {
            throw new ArgumentException("JWT Key must be at least 32 characters long.");
        }

        // Create security key and signing credentials
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        // Create claims for the token
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // User ID
            new Claim(JwtRegisteredClaimNames.Email, user.Email),      // User email
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // Unique token ID
        };

        // Create the JWT token
        var token = new JwtSecurityToken(
            issuer: jwtIssuer,
            audience: jwtAudience,
            claims: claims,
            expires: DateTime.Now.AddMinutes(jwtExpiryInMinutes),
            signingCredentials: credentials
        );

        // Serialize the token to a string
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}