using System.Security.Claims;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityService.Services;

public abstract class CustomProfileService(UserManager<ApplicationUser> userManager) : IProfileService
{
    /// <summary>
    /// This method is called whenever claims about the user are requested (e.g. during token creation)
    /// </summary>
    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var user = await userManager.GetUserAsync(context.Subject);
        if (user == null) return;
            
        var existingClaims = await userManager.GetClaimsAsync(user);

        // Add a custom claim called "username" that contains the user's username
        // this claim will be included in the access token
        var claims = new List<Claim>
        {
            new Claim("username", user.UserName ?? string.Empty)
        };
        context.IssuedClaims.AddRange(claims);
        
        // Add the user's full name from the "name" claim if it exists
        context.IssuedClaims.Add(existingClaims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name));
    }

    /// <summary>
    /// This method is called whenever an access token is validated.
    /// The method should return true if the user is active, false otherwise.
    /// </summary>
    public Task IsActiveAsync(IsActiveContext context)
    {
        return Task.CompletedTask;
    }
}