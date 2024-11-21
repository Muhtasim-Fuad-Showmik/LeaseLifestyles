using System.Security.Claims;
using IdentityModel;
using IdentityService.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace IdentityService.Pages.Account.Register;

[SecurityHeaders]
[AllowAnonymous]
public class Index : PageModel
{
    private readonly UserManager<ApplicationUser> _userManager;
    public Index(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }
    
    [BindProperty]
    public RegisterViewModel Input { get; set; }
    
    [BindProperty]
    public bool RegisterSuccess { get; set; }
    
    public IActionResult OnGet(string returnUrl)
    {
        Input = new RegisterViewModel
        {
            ReturnUrl = returnUrl
        };

        return Page();
    }

    public async Task<IActionResult> OnPost()
    {
        // Triggered from a cancel button therefore redirected to home page
        if (Input.Button != "register") return Redirect("~/");
        
        // Model state needs to be validated for frontend clients
        if (ModelState.IsValid)
        {
            // Create new Application User with defaults initialized
            var user = new ApplicationUser
            {
                UserName = Input.Username,
                Email = Input.Email,
                EmailConfirmed = true
            };
            
            // Create the user
            var result = await _userManager.CreateAsync(user, Input.Password);

            // Add full name from input to the user claims
            if (result.Succeeded)
            {
                await _userManager.AddClaimsAsync(user, new Claim[]
                {
                    new Claim(JwtClaimTypes.Name, Input.FullName)
                });

                RegisterSuccess = true;
            }

        }
        
        return Page();
    }
}