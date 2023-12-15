namespace VirtualPetAPI.Entity;

public class Health
{
    public int Id { get; set; }
    public string Situation { get; set; }
    public int PetId { get; set; }
    public Pet Pet { get; set; }
}