using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetAPI.DbContext;
using VirtualPetAPI.Entity;

namespace VirtualPetAPI.Controller;
[ApiController]
[Route("/api/v1/activities")]
public class ActivityController: ControllerBase
{
    private readonly VirtualPetDbContext _virtualPetDbContext;

    public ActivityController(VirtualPetDbContext virtualPetDbContext)
    {
        _virtualPetDbContext = virtualPetDbContext;
    }
    
    [HttpPost]
    public async Task<IActionResult> AddActivity([FromBody] Activity activity)
    {
        try
        {
            _virtualPetDbContext.Activities.Add(activity);
            await _virtualPetDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetActivitiesByPetId), 
                new { evcilHayvanId = activity.PetId }, activity);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }

    [HttpGet("{petId}")]
    public async Task<IActionResult> GetActivitiesByPetId(int petId)
    {
        try
        {
            var activities = await _virtualPetDbContext.Activities
                .Where(a => a.PetId == petId)
                .ToListAsync();

            if (activities == null || activities.Count == 0)
                return NotFound($"Evcil hayvan ID'si {petId} olan aktivite bulunamadı.");

            return Ok(activities);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }
}