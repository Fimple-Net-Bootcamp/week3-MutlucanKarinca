using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetAPI.DbContext;
using VirtualPetAPI.Entity;

namespace VirtualPetAPI.Controller;

[ApiController]
[Route("/api/v1/pets")]
public class PetController : ControllerBase
{
    private readonly VirtualPetDbContext _virtualPetDbContext;

    public PetController(VirtualPetDbContext virtualPetDbContext)
    {
        _virtualPetDbContext = virtualPetDbContext;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePet([FromBody] Pet pet)
    {
        try
        {
            _virtualPetDbContext.Pets.Add(pet);
            await _virtualPetDbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPetById), new { evcilHayvanId = pet.Id }, pet);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPets()
    {
        try
        {
            var pets = await _virtualPetDbContext.Pets.ToListAsync();
            return Ok(pets);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }

    [HttpGet("{petId}")]
    public async Task<IActionResult> GetPetById(int petId)
    {
        try
        {
            var pet = await _virtualPetDbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
                return NotFound($"Evcil hayvan ID'si {petId} olan evcil hayvan bulunamadı.");

            return Ok(pet);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }

    [HttpPut("{petId}")]
    public async Task<IActionResult> UpdatePet(int petId, [FromBody] Pet updatedPet)
    {
        try
        {
            var pet = await _virtualPetDbContext.Pets.FirstOrDefaultAsync(p => p.Id == petId);

            if (pet == null)
                return NotFound($"Evcil hayvan ID'si {petId} olan evcil hayvan bulunamadı.");

            pet.Name = updatedPet.Name;
            pet.Species = updatedPet.Species;
            pet.Breed = updatedPet.Breed;
            pet.UserId = updatedPet.UserId;
            pet.Activities = updatedPet.Activities;
            pet.Healths = updatedPet.Healths;
            await _virtualPetDbContext.SaveChangesAsync();

            return Ok(pet);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Hata: " + ex.Message);
        }
    }
}