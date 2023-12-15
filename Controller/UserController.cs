using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetAPI.DbContext;
using VirtualPetAPI.Entity;

namespace VirtualPetAPI.Controller;
[ApiController]
[Route("/api/v1/users")]
public class UserController: ControllerBase
{
    private readonly VirtualPetDbContext _virtualPetDbContext;

    public UserController(VirtualPetDbContext virtualPetDbContext)
    {
        _virtualPetDbContext = virtualPetDbContext;
    }
    

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        try
        {
            _virtualPetDbContext.Users.Add(user);
            await _virtualPetDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserById), new { kullaniciId = user.Id }, user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserById(int userId)
    {
        try
        {
            var user = await _virtualPetDbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                return NotFound($"Kullanıcı ID'si {userId} olan kullanıcı bulunamadı.");

            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }
}