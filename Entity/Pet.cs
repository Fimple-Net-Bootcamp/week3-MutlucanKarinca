namespace VirtualPetAPI.Entity;

public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }   
    public int UserId { get; set; }
    public User? User { get; set; }
    public List<Health>? Healths { get; set; }
    public List<Activity>? Activities { get; set; }
    public List<Food>? Foods { get; set; }
}