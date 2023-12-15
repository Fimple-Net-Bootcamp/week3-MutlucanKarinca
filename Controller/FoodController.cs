using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VirtualPetAPI.DbContext;
using VirtualPetAPI.Entity;

namespace VirtualPetAPI.Controller;

[ApiController]
[Route("/api/v1/foods")]
public class FoodController: ControllerBase
{
    private readonly VirtualPetDbContext _virtualPetDbContext;

    public FoodController(VirtualPetDbContext virtualPetDbContext)
    {
        _virtualPetDbContext = virtualPetDbContext;
    }


   [HttpGet]
   public async Task<IActionResult> GetAllFoods()
   {
       try
       {
           var foods = await _virtualPetDbContext.Foods.ToListAsync();

           if (foods == null || foods.Count == 0)
           {
               return NotFound("Hiç besin bulunamadı.");
           }

           return Ok(foods);
       }
       catch (Exception ex)
       {
         return StatusCode(500, "Internal Server Error: " + ex.Message);
       }
   }

   [HttpPost("{petId}")]
   public async Task<IActionResult> GiveFoodToPet(int petId, [FromBody] Food food)
   {
       try
       {
           var evcilHayvan = await _virtualPetDbContext.Pets
               .Include(p => p.Foods)
               .FirstOrDefaultAsync(e => e.Id == petId);

           if (evcilHayvan == null)
               return NotFound($"Evcil hayvan ID'si {petId} olan evcil hayvan bulunamadı.");

           evcilHayvan.Foods ??= new List<Food>();
           evcilHayvan.Foods.Add(food);

           await _virtualPetDbContext.SaveChangesAsync();

           return Ok($"Evcil hayvana {food.Name} adlı besin verildi.");
       }
       catch (Exception ex)
       {
           return StatusCode(500, "Hata: " + ex.Message);
       }
   }
}