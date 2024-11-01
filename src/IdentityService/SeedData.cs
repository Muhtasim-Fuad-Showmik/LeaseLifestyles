using System.Security.Claims;
using IdentityModel;
using IdentityService.Data;
using IdentityService.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace IdentityService;

public class SeedData
{
    public static void EnsureSeedData(WebApplication app)
    {
        // Create scope for database migration
        using var scope = app.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();

        // Use the scope to get the context for the database
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Attempt to migrate the database
        context.Database.Migrate();

        // Get the user manager service from the scope
        // provided by ASP .NET Core Identity
        var userMgr = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        // Check if the database already contains any users
        // which if it does, we won't execute the seed logic below
        if (userMgr.Users.Any()) return;

        // Check the database for a user name Alice in the database
        var alice = userMgr.FindByNameAsync("alice").Result;

        // Check if Alice does not exist
        if (alice == null)
        {
            // Create a new user object for Alice if she does not exist
            // and confirm her email by default (as we currently
            // are not adding an email service)
            alice = new ApplicationUser
            {
                UserName = "alice",
                Email = "AliceSmith@email.com",
                EmailConfirmed = true,
            };

            // Create the user Alice with a hardcoded password
            var result = userMgr.CreateAsync(alice, "Pass123$").Result;

            // Thrown an exception if the user creation failed
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            // Add claims for the user to define additional details about them
            // For now we only need their name to be added
            result = userMgr.AddClaimsAsync(alice, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        }).Result;

            // Thrown an exception if addition of claims fails
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            // Log a message indicating success
            Log.Debug("alice created");
        }
        else
        {
            // Log a message indicating that Alice already exists
            Log.Debug("alice already exists");
        }

        // Check the database for a user name Bob in the database
        var bob = userMgr.FindByNameAsync("bob").Result;

        // Check if Bob does not exist
        if (bob == null)
        {
            // Create a new user object for Bob if he does not exist
            // and confirm his email by default (as we currently
            // are not adding an email service)
            bob = new ApplicationUser
            {
                UserName = "bob",
                Email = "BobSmith@email.com",
                EmailConfirmed = true
            };

            // Create the user Bob with a hardcoded password
            var result = userMgr.CreateAsync(bob, "Pass123$").Result;

            // Thrown an exception if the user creation failed
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            // Add claims for the user to define additional details about them
            // For now we only need their name to be added
            result = userMgr.AddClaimsAsync(bob, new Claim[]{
                            new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        }).Result;

            // Thrown an exception if addition of claims fails     
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.First().Description);
            }

            // Log a message indicating success
            Log.Debug("bob created");
        }
        else
        {
            // Log a message indicating that Bob already exists
            Log.Debug("bob already exists");
        }
    }
}