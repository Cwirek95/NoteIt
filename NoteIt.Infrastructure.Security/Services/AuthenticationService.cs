using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NoteIt.Application.Contracts.Repositories;
using NoteIt.Application.Security.Contracts;
using NoteIt.Application.Security.Exceptions;
using NoteIt.Application.Security.Models;
using NoteIt.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NoteIt.Infrastructure.Security.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly JSONWebTokensSettings _jwtSettings;
        private readonly IStorageRepository _storageRepository;

        public AuthenticationService(IOptions<JSONWebTokensSettings> jwtSettings, IStorageRepository storageRepository)
        {
            _jwtSettings = jwtSettings.Value;
            _storageRepository = storageRepository;
        }

        public async Task<AuthenticationResponse> AuthenticateAsync(string storageName, string password)
        {
            var storage = await _storageRepository.GetByNameAsync(storageName);

            if (storage == null || !BCrypt.Net.BCrypt.Verify(password, storage.Password))
            {
                throw new UnauthorizedException("Invalid credentials. Please try again.");
            }

            JwtSecurityToken jwtSecurityToken = await GenerateToken(storage);

            AuthenticationResponse response = new AuthenticationResponse
            {
                Id = storage.Id,
                StorageName = storage.Name,
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
            };

            return response;
        }

        private async Task<JwtSecurityToken> GenerateToken(Storage storage)
        {
            var claims = new List<Claim>()
            {
            new Claim(ClaimTypes.NameIdentifier, storage.Id.ToString()),
            new Claim(ClaimTypes.Name, storage.AddressName),
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }
    }
}
