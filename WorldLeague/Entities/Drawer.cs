namespace WorldLeague.Entities;

public class Drawer : BaseEntity
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public ICollection<Team> Teams { get; set; }
}