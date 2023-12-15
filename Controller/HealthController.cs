using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetAPI.DbContext;
using VirtualPetAPI.Entity;

namespace VirtualPetAPI.Controller;
[ApiController]
[Route("/api/v1/healths")]
public class HealthController: ControllerBase
{
    
    private readonly VirtualPetDbContext _virtualPetDbContext;

    public HealthController(VirtualPetDbContext virtualPetDbContext)
    {
        _virtualPetDbContext = virtualPetDbContext;
    }
    
    [HttpGet("{petId}")]
    public async Task<IActionResult> GetHealthByPetId(int petId)
    {
        try
        {
            var health = await _virtualPetDbContext.Healths.FirstOrDefaultAsync(h => h.PetId == petId);

            if (health == null)
                return NotFound($"Evcil hayvan ID'si {petId} olan sağlık durumu bulunamadı.");

            return Ok(health);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }

    [HttpPatch("{petId}")]
    public async Task<IActionResult> UpdateHealth(int petId, [FromBody] Health updatedHealth)
    {
        try
        {
            var health = await _virtualPetDbContext.Healths.FirstOrDefaultAsync(h => h.PetId == petId);

            if (health == null)
                return NotFound($"Evcil hayvan ID'si {petId} olan sağlık durumu bulunamadı.");

            health.Situation = updatedHealth.Situation;
            health.PetId = updatedHealth.PetId;

            await _virtualPetDbContext.SaveChangesAsync();

            return Ok(health);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }
}