using Application.DTOs;
using Application.DTOs.User;
using Application.Enums;
using Application.Exceptions;
using Application.Interfaces;
using Application.Wrappers;
using Domain.Settings;
using Identity.Helpers;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings _jWTSettings;
        private readonly IDateTimeService _dateTimeService;
        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager, JWTSettings jWTSettings, IDateTimeService dateTimeService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _jWTSettings = jWTSettings;
            _dateTimeService = dateTimeService;
        }

        public async Task<GenericResponse<AuthenticationResponse>> AuthenticateAsync(AuthenticationRequest request, string ipAddress)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                throw new ApiException($"{request.Email} doesnt exist");
            }
            var result = _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);

            if (!result.IsCompletedSuccessfully)
            {
                throw new ApiException($"Invalid credentials");

            }
            JwtSecurityToken jwtSecurityToken = await GenerateJWTToken(user);
            var roles = await _userManager.GetRolesAsync(user).ConfigureAwait(false);

            var refreshToken =  GenerateRefreshJWTToken(ipAddress);
            AuthenticationResponse response = new AuthenticationResponse()
            {
                Id = user.Id,
                JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles.ToList(),
                IsVerfied = user.EmailConfirmed

            };
            response.RefreshToken = refreshToken.Token;

            return new GenericResponse<AuthenticationResponse>(response, $"Authenticate {user.UserName}");
        }

        public async Task<GenericResponse<string>> RegisterAsync(RegisterRequest request, string origin)
        {
            var userWithTheSameUserName = await _userManager.FindByNameAsync(request.UserName);

            if (userWithTheSameUserName != null)
            {
                throw new ApiException($"{request.UserName} is already registered");
            }

            var user = new ApplicationUser
            {
                Email = request.Email,
                Name = request.Name,
                LastName = request.LastName,
                UserName = request.UserName,
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
            };
            var userWithTheSameEmail = await _userManager.FindByEmailAsync(user.Email);
            if (userWithTheSameEmail != null)
                throw new ApiException($"The Email {request.Email} is already registered");
            else
            {
                var result = await _userManager.CreateAsync(user, request.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, Roles.basic.ToString());
                    return new GenericResponse<string>(user.Id,message: "Usuario Registrado exitosamente");
                }
                else
                {
                    throw new ApiException($"{result.Errors}");
                }
            }
        }


        private async Task<JwtSecurityToken> GenerateJWTToken(ApplicationUser user)
        {
            var  userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var rolesClaims = new List<Claim>();

            foreach (var rol in roles)
            {
                rolesClaims.Add(new Claim("roles", rol));
            }

            string ipAddress = IpHelper.GetIpAddress();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid",user.Id),
                new Claim("ip", ipAddress),
            }.Union(userClaims).Union(rolesClaims);

            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jWTSettings.Key));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(issuer:_jWTSettings.Issuer,
                audience:_jWTSettings.Audience,
                claims:claims,
                expires: DateTime.Now.AddMinutes(_jWTSettings.DuratinoInMinutes),
                signingCredentials:signingCredentials);


            return jwtSecurityToken;
        }

        private RefreshToken GenerateRefreshJWTToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now,
                CreatedByIp = ipAddress,
            };
        }
        private string RandomTokenString()
        {
            using var randomCryptoServiceProvider = new RNGCryptoServiceProvider();

            var randomBytes = new byte[40];

            randomCryptoServiceProvider.GetBytes(randomBytes); 

            return BitConverter.ToString(randomBytes).Replace("-","");
        }
    }
}
